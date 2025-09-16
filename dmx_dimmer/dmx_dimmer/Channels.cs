using System;
using System.Windows.Forms;


namespace dmx_dimmer
{
    public partial class Channels : Form
    {
        private readonly byte[] dmx = new byte[512];
        private int channel = 1;                 // 1-basiert
        private readonly System.Windows.Forms.Timer effectTimer = new System.Windows.Forms.Timer();
        private bool _updatingUI = false;

        public Channels()
        {
            InitializeComponent();
            startDMX_Sender();

            // --- Effekte initialisieren ---
            NativeEffects.effects_init();

            // --- Effekt-Timer (~40 FPS) ---
            effectTimer.Interval = 25;
            effectTimer.Tick += (s, e) => ApplyEffectsAndSend();
            effectTimer.Start();

            initChanels();
            UpdateUIForChannel(channel);
        }

        private void initChanels()
        {
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 512;
            numericUpDown1.Value = channel;

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 255;
            trackBar1.Value = dmx[channel - 1];
            label1.Text = trackBar1.Value.ToString();
            label2.Text = "0%";
        }

        private void startDMX_Sender()
        {
            
            int rc = Native.start_sender("192.168.2.128", 0, 40);
            if (rc != 0)
            {
                MessageBox.Show($"Start fehlgeschlagen: {rc}");
            }
        }

        
        private void ApplyEffectsAndSend()
        {
            int changed = NativeEffects.effects_apply(dmx, dmx.Length);
            if (changed > 0)
            {
                Native.update_dmx(dmx, dmx.Length);
                UpdateUIForChannel(channel);
            }
        }

        private void UpdateUIForChannel(int ch1)
        {
            int idx = ch1 - 1;
            if (idx < 0 || idx >= dmx.Length) return;

            _updatingUI = true;
            try
            {
                trackBar1.Value = dmx[idx];
                label1.Text = dmx[idx].ToString();
                label2.Text = ((int)Math.Round(dmx[idx] / 255.0 * 100)).ToString() + "%";
            }
            finally
            {
                _updatingUI = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_updatingUI) return; 

            int idx = channel - 1;

            if (NativeEffects.effects_is_active(channel) != 0)
                NativeEffects.effects_cancel(channel);

            dmx[idx] = (byte)trackBar1.Value;
            Native.update_dmx(dmx, dmx.Length);
            UpdateUIForChannel(channel);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            channel = (int)numericUpDown1.Value; 
            UpdateUIForChannel(channel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (channel < 512)
            {
                channel++;
                numericUpDown1.Value = channel;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (channel > 1)
            {
                channel--;
                numericUpDown1.Value = channel;
            }
        }

        // Einfache Helper-Methode für Fades
        private void FadeTo(byte target, int durationMs)
        {
            int idx = channel - 1;
            byte current = dmx[idx];
            NativeEffects.effects_start_fade(channel, current, target, durationMs);
            // Weitere Updates macht der Timer automatisch
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Fade -> 0%
            FadeTo(0, 2000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Fade -> 50%
            FadeTo(128, 2000);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Fade -> 100%
            FadeTo(255, 2000);
        }

        private void Channels_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnFormClosing(e);
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Timer anhalten
            effectTimer.Stop();
            effectTimer.Dispose();

            // Effekte stoppen (optional)
            NativeEffects.effects_cancel_all();

            // UI deaktivieren (optional)
            this.Enabled = false;

            // Sender sauber stoppen (im Hintergrund)
            await System.Threading.Tasks.Task.Run(() => Native.stop_sender());

            base.OnFormClosing(e);
        }
    }
}
