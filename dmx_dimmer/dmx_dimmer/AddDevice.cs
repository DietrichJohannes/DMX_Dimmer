using System;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class AddDevice : Form
    {
        public string DeviceName { get; private set; } = "";
        public int Universe { get; private set; } = 0;
        public int StartChannel { get; private set; } = 0;

        public AddDevice(string deviceName, int universe, int chanal)
        {
            InitializeComponent();

            device_name.Text = deviceName;
            device_universe.Value = universe;
            device_chanal.Value = chanal;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            // Einfache Validierung
            if (string.IsNullOrWhiteSpace(device_name.Text))
            {
                MessageBox.Show("Bitte einen Gerätenamen eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Werte übernehmen
            DeviceName = device_name.Text.Trim();
            Universe = (int)device_universe.Value;
            StartChannel = (int)device_chanal.Value;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
