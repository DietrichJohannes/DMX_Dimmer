using System;
using System.Windows.Forms;
using dmx_dimmer.Properties;
using DmxRuntime; // hier liegt DMX_Engine

namespace dmx_dimmer
{
    public partial class Form1 : Form
    {
        private DMX_Engine _engine;         // <<— gehört IN die Klasse

        public Form1()
        {
            InitializeComponent();
            placeWindow();
        }

        private void placeWindow()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.Width = Screen.PrimaryScreen.Bounds.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var effects = new Effects();
            effects.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var textBook = new TextBook();
            textBook.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (_engine == null)
            {
                MessageBox.Show("Engine ist noch nicht gestartet.");
                return;
            }

            // FaderPanel bekommt die Engine injiziert
            var faderPanel = new FaderPanel(_engine);
            faderPanel.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var devices = new Devices();
            devices.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Beispiel: Blackout auslösen
            _engine?.SetBlackout(true);
        }


        private void startStopSheduler_Click(object sender, EventArgs e)
        {
            if (_engine != null && _engine.IsRunning)
            {
                _engine.Dispose();
                _engine = null;
                btn_start_stop_sheduler.Image = Resources.play;
                btn_start_stop_sheduler.Text = "Sheduler Starten";
            }
            else
            {
                _engine = new DMX_Engine("192.168.2.193", universe: 0, fps: 30, sendOnlyWhenDirty: true);
                btn_start_stop_sheduler.Image = Resources.stop;
                btn_start_stop_sheduler.Text = "Sheduler Stoppen";
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _engine?.Dispose();
            base.OnFormClosing(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FaderPanel panel = new FaderPanel(_engine);
            panel.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Devices devices= new Devices();
            devices.Show();
        }
    }
}
