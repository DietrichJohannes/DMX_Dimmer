using MiniWebPanel;
using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class WebServer : Form
    {
        private readonly Server _server = new();

        private CanvasState _state; // kommt vom MainForm

        private bool _serverActive = false;
        private bool _protectedModeActive = false;

        private readonly string _configuredPassword;
        private readonly int _configuredPort;

        // Designer-freundlicher ctor (falls du das Form im Designer öffnest)
        public WebServer() : this(new CanvasState())
        {
        }

        // Haupt-ctor: State wird von außen übergeben
        public WebServer(CanvasState state)
        {
            InitializeComponent();

            _state = state ?? new CanvasState();

            _configuredPassword = ConfigurationManager.AppSettings["ServerPassword"] ?? "";
            if (!int.TryParse(ConfigurationManager.AppSettings["WebserverPort"], out _configuredPort))
                _configuredPort = 8080;

            // Jetzt, wo _state gesetzt ist, State in den Server schieben
            _server.SetState(_state);

            UpdateServerButton();
            UpdateProtectButton();

            if (linkAddress != null)
                linkAddress.Text = $"http://{GetLocalIp()}:{_configuredPort}/";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_serverActive)
            {
                try { _server.Stop(); } catch { }
                _serverActive = false;
            }
            else
            {
                _server.Password = _protectedModeActive && !string.IsNullOrEmpty(_configuredPassword)
                    ? _configuredPassword
                    : null;

                // sicherheitshalber den aktuellen State nochmal setzen
                _server.SetState(_state);

                // Hinweis: Für http://+ brauchst du ggf. URLACL-Rechte oder Admin
                _server.Start($"http://+:{_configuredPort}/");
                _serverActive = true;

                if (linkAddress != null)
                    linkAddress.Text = $"http://{GetLocalIp()}:{_configuredPort}/";
            }

            UpdateServerButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _protectedModeActive = !_protectedModeActive;

            _server.Password = _protectedModeActive && !string.IsNullOrEmpty(_configuredPassword)
                ? _configuredPassword
                : null;

            UpdateProtectButton();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { _server.Stop(); } catch { }
            base.OnFormClosing(e);
        }

        private void UpdateServerButton()
        {
            if (_serverActive)
            {
                button1.Text = "Stoppen";
                button1.BackColor = Color.Red;
            }
            else
            {
                button1.Text = "Starten";
                button1.BackColor = Color.FromArgb(0, 192, 0);
            }
        }

        private void UpdateProtectButton()
        {
            if (_protectedModeActive)
            {
                button3.Text = "Aktiv";
                button3.BackColor = Color.FromArgb(0, 192, 0);
            }
            else
            {
                button3.Text = "Inaktiv";
                button3.BackColor = Color.Red;
            }
        }

        private static string GetLocalIp()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList.First(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .ToString();
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        private void linkAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkAddress.LinkVisited = true;
                var url = linkAddress.Text;

                // Öffnet den Standardbrowser zuverlässig
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konnte die Adresse nicht öffnen:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
