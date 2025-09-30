using System;
using System.Windows.Forms;
using DmxRuntime;

namespace dmx_dimmer
{
    public partial class FaderPanel : Form
    {
        private readonly DMX_Engine _engine;

        // Variante A: Engine wird von außen übergeben (empfohlen)
        public FaderPanel(DMX_Engine engine)
        {
            InitializeComponent();
            _engine = engine;

            // UI-Grundsetup (falls noch nicht im Designer)
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 512;
            numericUpDown1.Value = 1;

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 255;

            // Events
            trackBar1.ValueChanged += trackBar1_ValueChanged;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            int channel = Convert.ToInt32(numericUpDown1.Value); // 1..512
            byte value = (byte)trackBar1.Value;                  // 0..255
            _engine.SetChannel(channel, value);                    // Command an Engine
        }

        // Wenn du WIRKLICH willst, dass das Panel Besitzer der Engine ist:
        // überschreibe lieber OnFormClosing statt Event-Handler:
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Falls das Panel die Engine erstellt hat: _engine?.Dispose();
            // Ansonsten NICHT disposen, wenn Engine app-weit verwendet wird.
        }
    }
}
