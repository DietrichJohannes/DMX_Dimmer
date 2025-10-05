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
            _engine.SetBlackout();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Devices devices = new Devices();
            devices.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _engine.SetBlackout();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            Effects effects = new Effects();
            effects.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                                  "Möchten Sie DMX_DIMMER wirklich beenden?",
                                                  "DMX_DIMMER beenden",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question
                                                  );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }


    }
}
