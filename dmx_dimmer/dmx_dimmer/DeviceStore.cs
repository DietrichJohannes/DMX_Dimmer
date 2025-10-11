using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dmx_dimmer
{
    public sealed class DeviceStore
    {
        private readonly List<DeviceInstance> _devices = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        // Änderungsevent (UI kann drauf reagieren)
        public event EventHandler? Changed;

        // Optional: Singleton
        public static DeviceStore Shared { get; } = new DeviceStore();

        public IReadOnlyList<DeviceInstance> GetAll()
        {
            lock (_lock) return _devices.ToList();
        }

        public DeviceInstance? GetById(int id)
        {
            lock (_lock) return _devices.FirstOrDefault(d => d.Id == id);
        }

        public bool Remove(int id)
        {
            lock (_lock)
            {
                var idx = _devices.FindIndex(d => d.Id == id);
                if (idx < 0) return false;
                _devices.RemoveAt(idx);
            }
            OnChanged();
            return true;
        }

        public bool UpdateAddress(int id, ushort universe, int start, out string? error)
        {
            lock (_lock)
            {
                var d = _devices.FirstOrDefault(x => x.Id == id);
                if (d == null) { error = "Gerät nicht gefunden."; return false; }

                int fp = d.Footprint;
                if (start < 1 || start + fp - 1 > 512) { error = "Adresse außerhalb 1..512."; return false; }
                if (OverlapsAny_NoLock(universe, start, fp, exceptId: id)) { error = "Adresskollision."; return false; }

                d.Universe = universe;
                d.StartChannel = start;
            }
            error = null; OnChanged(); return true;
        }

        public DeviceInstance Add(DeviceTemplate t, string label, ushort universe, int start, out string? error)
        {
            int fp = t.Channels.Max(c => c.Offset) + 1;
            lock (_lock)
            {
                if (start < 1 || start + fp - 1 > 512)
                {
                    error = "Startkanal + Footprint überschreitet 512."; return null!;
                }
                if (OverlapsAny_NoLock(universe, start, fp, exceptId: null))
                {
                    error = "Adresse kollidiert mit bestehendem Gerät."; return null!;
                }

                var inst = new DeviceInstance(_nextId++, t, string.IsNullOrWhiteSpace(label) ? t.Name : label, start, universe);
                _devices.Add(inst);
                error = null;
                OnChanged();
                return inst;
            }
        }

        public int SuggestStart(DeviceTemplate t, ushort universe)
        {
            int fp = t.Channels.Max(c => c.Offset) + 1;
            lock (_lock)
            {
                for (int s = 1; s + fp - 1 <= 512; s++)
                    if (!OverlapsAny_NoLock(universe, s, fp, exceptId: null)) return s;
                return Math.Max(1, 512 - fp + 1);
            }
        }

        private bool OverlapsAny_NoLock(ushort universe, int start, int footprint, int? exceptId)
        {
            int min = start;
            int max = start + footprint - 1;
            foreach (var d in _devices)
            {
                if (exceptId.HasValue && d.Id == exceptId.Value) continue;
                if (d.Universe != universe) continue;
                int dmin = d.StartChannel;
                int dmax = d.StartChannel + d.Footprint - 1;
                if (max >= dmin && dmax >= min) return true;
            }
            return false;
        }

        private void OnChanged() => Volatile.Read(ref Changed)?.Invoke(this, EventArgs.Empty);

        // ---- Platzhalter für spätere Persistenz ----
        public void Save(string path) { /* später implementieren */ }
        public void Load(string path) { /* später implementieren; danach OnChanged(); */ }
    }
}
