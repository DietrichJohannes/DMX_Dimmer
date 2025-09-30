// DMX_Engine.cs
// Achtung: Passe das using unten auf den Namespace deiner DLL an.
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
// <- Namespace deiner C-DLL:
using dmx_dimmer;  // enthält ArtNet.start_sender(...), ArtNet.update_dmx(...), ArtNet.stop_sender()

namespace DmxRuntime
{
    public enum MergeMode { HTP, LTP }

    public readonly record struct SetChannelCmd(int Channel, byte Value, int Priority = 0, MergeMode Mode = MergeMode.HTP);
    public readonly record struct SetManyCmd(IReadOnlyDictionary<int, byte> Values, int Priority = 0, MergeMode Mode = MergeMode.HTP);
    public readonly record struct BlackoutCmd(bool Enabled);
    public readonly record struct GrandMasterCmd(byte Value); // 0..255

    /// <summary>
    /// Zentraler DMX-Manager: nimmt Befehle entgegen, merged sie und sendet in fixer Framerate an die ArtNet-DLL.
    /// Diese Minimalversion arbeitet mit EINEM Universe (512 Kanäle). Mehr-Universen ist unten leicht erweiterbar.
    /// </summary>
    public sealed class DMX_Engine : IDisposable
    {
        // --- Config ---
        private readonly double _fps;
        private readonly ushort _universe;
        private readonly string _nodeIp;
        private readonly bool _sendOnlyWhenDirty;

        // --- Concurrency & Lifetime ---
        private readonly ConcurrentQueue<object> _queue = new();
        private readonly CancellationTokenSource _cts = new();
        private Task? _loopTask;

        // --- State (logische Kanäle) ---
        // HTP: höchster Wert gewinnt (typ. Dimmer)
        private readonly Dictionary<int, (byte val, int prio)> _htp = new();
        // LTP: letzter (mit >= Prio) gewinnt (typ. Farbe, Pan/Tilt, Gobo)
        private readonly Dictionary<int, (byte val, int prio)> _ltp = new();

        // Gemergter Ergebnis-State (logisch 1..512)
        private readonly byte[] _logical = new byte[512];

        // Transport-Puffer (physisches Universe)
        private readonly byte[] _frameBuf = new byte[512];
        private readonly byte[] _lastSent = new byte[512];

        // Master/Safety
        private volatile bool _blackout;
        private volatile byte _grandMaster = 255; // 0..255

        // --- Public API ---
        public DMX_Engine(string nodeIp, ushort universe, double fps = 30.0, bool sendOnlyWhenDirty = true)
        {
            _nodeIp = nodeIp;
            _universe = universe;
            _fps = Math.Max(1.0, fps);
            _sendOnlyWhenDirty = sendOnlyWhenDirty;

            // Init der C-DLL
            ArtNet.start_sender(_nodeIp, _universe, (int)_fps);

            // Loop starten
            _loopTask = Task.Run(Loop, _cts.Token);
        }

        /// <summary>Bequemes API für UI/Automationen</summary>
        public void SetChannel(int channel, byte value, int priority = 0, MergeMode mode = MergeMode.HTP)
            => _queue.Enqueue(new SetChannelCmd(channel, value, priority, mode));

        public void SetMany(IReadOnlyDictionary<int, byte> values, int priority = 0, MergeMode mode = MergeMode.HTP)
            => _queue.Enqueue(new SetManyCmd(values, priority, mode));

        public void SetBlackout(bool enabled) => _queue.Enqueue(new BlackoutCmd(enabled));
        public void SetGrandMaster(byte value) => _queue.Enqueue(new GrandMasterCmd(value));

        // --- Main Loop ---
        private async Task Loop()
        {
            var frameTime = TimeSpan.FromMilliseconds(1000.0 / _fps);
            var sw = new Stopwatch();

            while (!_cts.IsCancellationRequested)
            {
                sw.Restart();
                try
                {
                    DrainCommands();
                    BuildLogicalState();
                    BuildAndSendUniverse();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DMX_Engine] Loop error: {ex}");
                }

                var sleep = frameTime - sw.Elapsed;
                if (sleep > TimeSpan.Zero)
                    await Task.Delay(sleep, _cts.Token).ConfigureAwait(false);
            }
        }

        // --- Command Handling ---
        private void DrainCommands()
        {
            while (_queue.TryDequeue(out var cmd))
            {
                switch (cmd)
                {
                    case SetChannelCmd s:
                        ApplySet(s.Channel, s.Value, s.Priority, s.Mode);
                        break;

                    case SetManyCmd m:
                        foreach (var kv in m.Values)
                            ApplySet(kv.Key, kv.Value, m.Priority, m.Mode);
                        break;

                    case BlackoutCmd b:
                        _blackout = b.Enabled;
                        break;

                    case GrandMasterCmd g:
                        _grandMaster = g.Value;
                        break;
                }
            }
        }

        private void ApplySet(int channel, byte value, int priority, MergeMode mode)
        {
            if (channel is < 1 or > 512) return;

            if (mode == MergeMode.HTP)
            {
                if (!_htp.TryGetValue(channel, out var cur))
                    _htp[channel] = (value, priority);
                else
                {
                    // HTP-Kriterium: höhere Prio gewinnt immer; bei gleicher/geringerer Prio gewinnt der höhere Value
                    if (priority > cur.prio || value > cur.val)
                        _htp[channel] = (value, priority);
                }
            }
            else
            {
                // LTP: letzter mit >= Prio überschreibt
                if (!_ltp.TryGetValue(channel, out var cur) || priority >= cur.prio)
                    _ltp[channel] = (value, priority);
            }
        }

        // --- Merge (HTP + LTP -> logical) ---
        private void BuildLogicalState()
        {
            // Start: alles auf 0
            Array.Clear(_logical, 0, _logical.Length);

            // HTP anwenden (Maximum)
            foreach (var (ch, t) in _htp)
            {
                int i = ch - 1;
                if ((uint)i < 512)
                    _logical[i] = Math.Max(_logical[i], t.val);
            }

            // LTP anwenden (überschreibt)
            foreach (var (ch, t) in _ltp)
            {
                int i = ch - 1;
                if ((uint)i < 512)
                    _logical[i] = t.val;
            }

            // Grand Master / Blackout
            if (_blackout || _grandMaster < 255)
            {
                for (int i = 0; i < 512; i++)
                {
                    var v = _logical[i];
                    if (_blackout) v = 0;
                    else v = (byte)((v * _grandMaster) / 255);
                    _logical[i] = v;
                }
            }
        }

        // --- Universe bauen & senden ---
        private void BuildAndSendUniverse()
        {
            // Hier 1:1 auf physische Slots (später Patch einbauen, wenn du logische Kanäle mappen willst)
            Buffer.BlockCopy(_logical, 0, _frameBuf, 0, 512);

            if (_sendOnlyWhenDirty && _frameBuf.SequenceEqual(_lastSent))
                return;

            ArtNet.update_dmx(_frameBuf, _frameBuf.Length);
            Buffer.BlockCopy(_frameBuf, 0, _lastSent, 0, 512);
        }

        // --- Lifetime ---
        public void Dispose()
        {
            try { _cts.Cancel(); } catch { /* ignore */ }
            try { _loopTask?.Wait(200); } catch { /* ignore */ }
            try { ArtNet.stop_sender(); } catch { /* ignore */ }
            _cts.Dispose();
        }

        // --- (Optional) Helper: Klarer Reset aller Kanäle ---
        public void ClearAll()
        {
            _htp.Clear();
            _ltp.Clear();
            Array.Clear(_logical, 0, _logical.Length);
            Array.Clear(_frameBuf, 0, _frameBuf.Length);
            Array.Clear(_lastSent, 0, _lastSent.Length);
        }
    }
}
