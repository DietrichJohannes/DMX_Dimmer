using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            ip_label.Mask = "000\\.000\\.0\\.000";
            ip_label.Text = ConfigurationManager.AppSettings["ArtNetIP"];
        }

        private void SaveSettings()
        {
            // Aktuelle Konfiguration laden
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Wert aktualisieren oder hinzufügen
            if (config.AppSettings.Settings["ArtNetIP"] == null)
            {
                config.AppSettings.Settings.Add("ArtNetIP", ip_label.Text);
            }
            else
            {
                config.AppSettings.Settings["ArtNetIP"].Value = ip_label.Text;
            }

            // Änderungen speichern
            config.Save(ConfigurationSaveMode.Modified);

            // Konfiguration neu laden, damit andere Stellen im Programm den neuen Wert sehen
            ConfigurationManager.RefreshSection("appSettings");

            MessageBox.Show("Einstellungen wurden gespeichert.", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}