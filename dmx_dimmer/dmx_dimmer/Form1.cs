using System.Threading.Channels;

namespace dmx_dimmer
{
    public partial class Form1 : Form
    {

        private readonly byte[] dmx = new byte[512];

        public Form1()
        {
            InitializeComponent();
            placeWindow();
        }

        private void placeWindow()
        {
            this.StartPosition = FormStartPosition.Manual; // wichtig!
            this.Location = new System.Drawing.Point(0, 0);

            this.Width = Screen.PrimaryScreen.Bounds.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Effects effects = new Effects();
            effects.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextBook textBook = new TextBook();
            textBook.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Channels channels = new Channels();
            channels.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Devices devices = new Devices();
            devices.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Blackout DMX Bus AUS!", "DMX_Dimmer");
        }
    }
}
