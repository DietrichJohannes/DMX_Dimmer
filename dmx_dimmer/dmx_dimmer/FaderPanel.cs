using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DmxRuntime;

namespace dmx_dimmer
{
    public partial class FaderPanel : Form
    {
        private readonly DMX_Engine _engine;
        private const int MaxDmxChannel = 512;
        private const int FaderCount = 24;

        private int _baseChannel = 1;

        // Wenn dein Compiler C# 9 nicht nutzt, ersetze "new()" durch "new Dictionary<TrackBar, Label>()" usw.
        private readonly Dictionary<TrackBar, Label> _percentLabels = new();
        private readonly Dictionary<TrackBar, Label> _channelLabels = new();
        private readonly List<TrackBar> _faders = new();

        public FaderPanel(DMX_Engine engine)
        {
            InitializeComponent();
            _engine = engine;

            if (_engine == null)
            {
                MessageBox.Show(
                    "DMX Engine ist nicht gestartet.\nBitte starte die Engine, bevor du Werte sendest.",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                // Optional: Close(); return;  // wenn Fenster gar nicht erst nutzbar sein soll
            }

            // TrackBars registrieren – achte auf korrekte Label-Namen!
            RegisterFader(trackBar1, value1, ch1, 0);
            RegisterFader(trackBar2, value2, ch2, 1);
            RegisterFader(trackBar3, value3, ch3, 2);
            RegisterFader(trackBar4, value4, ch4, 3);
            RegisterFader(trackBar5, value5, ch5, 4);  // FIX: ch5 statt ch3
            RegisterFader(trackBar6, value6, ch6, 5);
            RegisterFader(trackBar7, value7, ch7, 6);
            RegisterFader(trackBar8, value8, ch8, 7);
            RegisterFader(trackBar9, value9, ch9, 8);  // FIX: ch9 statt ch8
            RegisterFader(trackBar10, value10, ch10, 9);
            RegisterFader(trackBar11, value11, ch11, 10);
            RegisterFader(trackBar12, value12, ch12, 11);
            RegisterFader(trackBar13, value13, ch13, 12);
            RegisterFader(trackBar14, value14, ch14, 13);
            RegisterFader(trackBar15, value15, ch15, 14);
            RegisterFader(trackBar16, value16, ch16, 15);
            RegisterFader(trackBar17, value17, ch17, 16);
            RegisterFader(trackBar18, value18, ch18, 17);
            RegisterFader(trackBar19, value19, ch19, 18);
            RegisterFader(trackBar20, value20, ch20, 19);
            RegisterFader(trackBar21, value21, ch21, 20);
            RegisterFader(trackBar22, value22, ch22, 21);
            RegisterFader(trackBar23, value23, ch23, 22);
            RegisterFader(trackBar24, value24, ch24, 23);

            // NumericUpDown
            NUDchannel.Minimum = 1;
            NUDchannel.Maximum = MaxDmxChannel - (FaderCount - 1); // 512 - 23 = 489
            NUDchannel.Value = _baseChannel;
            NUDchannel.ValueChanged += NUDchannel_ValueChanged;

            UpdateAllPercentLabels();
            UpdateAllChannelCaptions();
        }

        private void RegisterFader(TrackBar tb, Label percentLabel, Label channelCaption, int offset)
        {
            tb.Minimum = 0;
            tb.Maximum = 255;
            tb.Tag = offset;
            tb.Scroll += TrackBar_Scroll;   // UI-Update
            tb.MouseUp += TrackBar_Commit;  // beim Loslassen senden
            tb.KeyUp += TrackBar_Commit;

            _percentLabels[tb] = percentLabel;
            if (channelCaption != null) _channelLabels[tb] = channelCaption;

            _faders.Add(tb);
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            var tb = (TrackBar)sender;
            UpdatePercentLabel(tb);
            // Falls live-senden gewünscht:
            // TrySendToEngine(GetChannelFor(tb), (byte)tb.Value);
        }

        private void TrackBar_Commit(object sender, EventArgs e)
        {
            var tb = (TrackBar)sender;
            TrySendToEngine(GetChannelFor(tb), (byte)tb.Value);
        }

        private void NUDchannel_ValueChanged(object sender, EventArgs e)
        {
            _baseChannel = (int)NUDchannel.Value;
            UpdateAllChannelCaptions();

            // Optional: alle aktuellen Werte auf neue Kanäle senden
            foreach (var tb in _faders)
                TrySendToEngine(GetChannelFor(tb), (byte)tb.Value);
        }

        private int GetChannelFor(TrackBar tb)
        {
            int offset = (int)(tb.Tag ?? 0);
            int ch = _baseChannel + offset;
            if (ch < 1) ch = 1;
            if (ch > MaxDmxChannel) ch = MaxDmxChannel;
            return ch;
        }

        private void UpdatePercentLabel(TrackBar tb)
        {
            if (_percentLabels.TryGetValue(tb, out var lbl))
            {
                double pct = tb.Value / 255.0 * 100.0;
                lbl.Text = $"{pct:0}%";
            }
        }

        private void UpdateAllPercentLabels()
        {
            foreach (var tb in _faders)
                UpdatePercentLabel(tb);
        }

        private void UpdateAllChannelCaptions()
        {
            foreach (var tb in _faders)
                if (_channelLabels.TryGetValue(tb, out var lbl))
                    lbl.Text = $"{GetChannelFor(tb)}";
        }

        // Sichere Engine-Kapselung
        private bool TrySendToEngine(int channel, byte value)
        {
            if (_engine == null)
            {
                MessageBox.Show("DMX Engine ist nicht gestartet.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                _engine.SetChannel(channel, value);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Senden an Kanal {channel}:\n{ex.Message}",
                    "DMX-Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            // Seite zurück
            _baseChannel -= FaderCount;

            if (_baseChannel < 1)
                _baseChannel = 1;

            UpdateAfterScroll();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            // Seite vor
            _baseChannel += FaderCount;

            // Begrenzen, damit letzte Seite nicht über 512 hinausragt
            if (_baseChannel > MaxDmxChannel - (FaderCount - 1))
                _baseChannel = MaxDmxChannel - (FaderCount - 1);

            UpdateAfterScroll();
        }

        private void UpdateAfterScroll()
        {
            // NumericUpDown synchron halten (falls vorhanden)
            if (NUDchannel.Value != _baseChannel)
                NUDchannel.Value = _baseChannel;

            // Kanalbeschriftungen aktualisieren
            UpdateAllChannelCaptions();

            // Buttons korrekt aktivieren/deaktivieren
            buttonPrev.Enabled = _baseChannel > 1;
            buttonNext.Enabled = _baseChannel < MaxDmxChannel - (FaderCount - 1);

            // Optional: aktuelle Werte erneut an Engine senden
            foreach (var tb in _faders)
                TrySendToEngine(GetChannelFor(tb), (byte)tb.Value);
        }
    }
}
