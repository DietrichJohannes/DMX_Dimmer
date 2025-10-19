using dmx_dimmer.Properties;
using DmxRuntime;
using System.Configuration;


namespace dmx_dimmer
{
    public partial class Form1 : Form
    {
        private DMX_Engine _engine;
        DeviceStore _deviceStore = new DeviceStore();
        PowerStatus pwr = SystemInformation.PowerStatus;

        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            placeWindow();
            initDmxEngine();
            InitializeBatteryMonitor();
        }

        private void placeWindow()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.Width = Screen.PrimaryScreen.Bounds.Width;
        }


        private void InitializeBatteryMonitor()
        {
            // Timer erstellen (alle 5 Sekunden aktualisieren)
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10000; // 10 Sekunden
            timer.Tick += UpdateBatteryStatus;
            timer.Start();

            // Initiales Update
            UpdateBatteryStatus(null, null);
        }

        private void UpdateBatteryStatus(object sender, EventArgs e)
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            if (pwr.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
            {
                lblBattery.Text = "Kein Akku vorhanden";
                BatteryBar.Value = 0;
            }
            else
            {
                BatteryBar.Style = ProgressBarStyle.Continuous;

                int percent = (int)(pwr.BatteryLifePercent * 100);
                BatteryBar.Value = percent;
                lblBattery.Text = $"Akkustand: {percent}%";

                if (pwr.PowerLineStatus == PowerLineStatus.Online)
                {
                    lblBattery.Text += " (Netzbetrieb)";
                }
                else
                {
                    lblBattery.Text += " (Akkubetrieb)";
                }
            }
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

        private void initDmxEngine()
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
                string ip = ConfigurationManager.AppSettings["ArtNetIP"];
                bool whenDirty = false;
                bool.TryParse(ConfigurationManager.AppSettings["SendOnlyWhenDirty"], out whenDirty);
                int fps = int.Parse(ConfigurationManager.AppSettings["DMXFPS"]);

                _engine = new DMX_Engine(ip, universe: 0, fps, whenDirty);


                btn_start_stop_sheduler.Image = Resources.stop;
                btn_start_stop_sheduler.Text = "Sheduler Stoppen";
            }
        }

        private void startStopSheduler_Click(object sender, EventArgs e)
        {
            initDmxEngine();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FaderPanel panel = new FaderPanel(_engine);
            panel.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (_engine != null)
            {
                _engine.SetBlackout();
            }
            else
            {
                MessageBox.Show("DMX Engine ist gesoppt!");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var devicesWindow = new Devices(_deviceStore, _engine);
            devicesWindow.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (_engine != null)
            {
                _engine.SetBlackout();
            }
            else
            {
                MessageBox.Show("DMX Engine ist gesoppt!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Scene scene = new Scene(_engine);
            scene.Show();
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
            else
            {
                if (_engine != null)
                {
                    _engine.Dispose();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GraphicStageView graphicStageView = new GraphicStageView();
            graphicStageView.Show();
        }

        private void überDenEntwicklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }
    }
}
