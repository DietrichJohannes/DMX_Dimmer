using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class Channels : Form
    {
        private readonly byte[] dmx = new byte[512];
        int channel = 1;

        public Channels()
        {
            InitializeComponent();
            startDMX_Sender();
            initChanels();
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
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int idx = channel - 1;            // 0-basiert
            dmx[idx] = (byte)trackBar1.Value; // Wert schreiben
            Native.update_dmx(dmx, dmx.Length);
            updateViews(idx);
        }

        private void startDMX_Sender()
        {
            int rc = Native.start_sender("192.168.2.128", 0, 1); // Node-IP, Universe 0, 40 FPS
            if (rc != 0)                                         // TODO: Framerate anpassen! (40 fps)
            {
                MessageBox.Show($"Start fehlgeschlagen: {rc}");
            }
        }

        private void Channels_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnFormClosing(e);
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // UI nicht mehr bedienbar machen (optional)
            this.Enabled = false;

            // stop in Hintergrund-Thread ausführen, UI bleibt responsiv
            await Task.Run(() => Native.stop_sender());

            base.OnFormClosing(e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            channel = (int)numericUpDown1.Value; // 1-basiert merken
            int idx = channel - 1;               // 0-basiert berechnen
            updateViews(idx);
        }

        private void updateViews(int idx)
        {
            trackBar1.Value = dmx[idx];
            label1.Text = dmx[idx].ToString();
            label2.Text = ((int)Math.Round(dmx[idx] / 255.0 * 100)).ToString() + "%";
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
    }
}
