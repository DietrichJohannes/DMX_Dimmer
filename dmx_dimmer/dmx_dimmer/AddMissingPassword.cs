using dmx_dimmer.Properties;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class AddMissingPassword : Form
    {
        public string ResultPassword { get; private set; }
        bool pwVisible;

        public AddMissingPassword()
        {
            InitializeComponent();
            this.AcceptButton = btn_ok;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var entered = password.Text?.Trim();

            if (string.IsNullOrEmpty(entered))
            {
                MessageBox.Show("Bitte ein Passwort eingeben.");
                this.DialogResult = DialogResult.None;
                return;
            }

            ResultPassword = entered;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SetAppSetting(config, "Password", entered);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private static void SetAppSetting(Configuration config, string key, string value)
        {
            var settings = config.AppSettings.Settings;
            if (settings[key] == null)
                settings.Add(key, value);
            else
                settings[key].Value = value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwVisible = !pwVisible;
            password.UseSystemPasswordChar = !pwVisible;

            if (pwVisible)
            {
                button2.Image = Resources.eye_closed;
            }
            else
            {
                button2.Image = Resources.eye_outline;
            }
        }
    }
}
