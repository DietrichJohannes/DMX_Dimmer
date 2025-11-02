using dmx_dimmer.Properties;
using System;
using System.Configuration;
using System.Net;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class Settings : Form
    {
        private bool sendWhenDirty = false;
        private bool pwVisible = false;

        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Korrekte IPv4-Maske: 000.000.000.000
            ip_label.Mask = "000\\.000\\.0\\.000";

            // Werte laden (mit Default)
            ip_label.Text = ConfigurationManager.AppSettings["ArtNetIP"] ?? "192.168.0.10";

            if (bool.TryParse(ConfigurationManager.AppSettings["SendOnlyWhenDirty"], out var b))
                sendWhenDirty = b;

            // UI sync
            chkSendOnlyWhenDirty.Checked = sendWhenDirty;

            dmx_fps.Value = int.Parse(ConfigurationManager.AppSettings["DMXFPS"] ?? "30");

            password.Text = ConfigurationManager.AppSettings["Password"] ?? "";
            password.UseSystemPasswordChar = true;
        }

        private void SaveSettings()
        {
            // 1) IP validieren
            var ipText = (ip_label.Text ?? "").Trim();
            if (!IPAddress.TryParse(ipText, out _))
            {
                MessageBox.Show("Bitte eine gültige IPv4-Adresse eingeben.", "Ungültige IP",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Config öffnen (App-Config)
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // 3) Werte setzen/aktualisieren
            SetAppSetting(config, "ArtNetIP", ipText);
            SetAppSetting(config, "SendOnlyWhenDirty", chkSendOnlyWhenDirty.Checked.ToString().ToLowerInvariant());
            SetAppSetting(config, "DMXFPS", dmx_fps.Value.ToString());
            SetAppSetting(config, "Password", password.Text.ToString());

            // 4) Speichern & Refresh
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            // internen Cache updaten (optional)
            sendWhenDirty = chkSendOnlyWhenDirty.Checked;

            MessageBox.Show("Einstellungen erfolgreich gespeichert.", "Speichern",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SetAppSetting(Configuration config, string key, string value)
        {
            var settings = config.AppSettings.Settings;
            if (settings[key] == null)
                settings.Add(key, value);
            else
                settings[key].Value = value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void chkSendOnlyWhenDirty_CheckedChanged(object sender, EventArgs e)
        {
            sendWhenDirty = chkSendOnlyWhenDirty.Checked;
        }

        private void dmx_fps_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwVisible = !pwVisible;
            password.UseSystemPasswordChar = !pwVisible;

            if(pwVisible)
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
