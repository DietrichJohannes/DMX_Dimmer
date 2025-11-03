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
        private CanvasState _state = new();

        private bool _serverActive = false;
        private bool _protectedModeActive = false;

        private readonly string _configuredPassword;
        private readonly int _configuredPort;

        public WebServer()
        {
            InitializeComponent();

            _configuredPassword = ConfigurationManager.AppSettings["ServerPassword"] ?? "";
            if (!int.TryParse(ConfigurationManager.AppSettings["WebserverPort"], out _configuredPort))
                _configuredPort = 8080;

            _server.SetState(_state);

            UpdateServerButton();
            UpdateProtectButton();

            linkAddress.Text = $"http://{GetLocalIp()}:{_configuredPort}/";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_serverActive)
            {
                _server.Stop();
                _serverActive = false;
            }
            else
            {
                _server.Password = _protectedModeActive && !string.IsNullOrEmpty(_configuredPassword)
                    ? _configuredPassword
                    : null;

                _server.SetState(_state);
                _server.Start($"http://+:{_configuredPort}/");
                _serverActive = true;

                if (linkAddress != null) linkAddress.Text = $"http://{GetLocalIp()}:{_configuredPort}/";
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

        private void button2_Click(object sender, EventArgs e)
        {
            using var editor = new CanvasEditor();
            editor.LoadState(_state);
            editor.StateChanged += st =>
            {
                _state = st;
                _server.SetState(_state);
            };
            editor.ShowDialog(this);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { _server.Stop(); } catch { }
            base.OnFormClosing(e);
        }

        private void UpdateServerButton()
        {
            if (_serverActive) { button1.Text = "Stoppen"; button1.BackColor = Color.Red; }
            else { button1.Text = "Starten"; button1.BackColor = Color.FromArgb(0, 192, 0); }
        }

        private void UpdateProtectButton()
        {
            if (_protectedModeActive) { button3.Text = "Aktiv"; button3.BackColor = Color.FromArgb(0, 192, 0); }
            else { button3.Text = "Inaktiv"; button3.BackColor = Color.Red; }
        }

        private static string GetLocalIp()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList.First(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .ToString();
            }
            catch { return "127.0.0.1"; }
        }

        private void linkAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkAddress.LinkVisited = true;

                var url = linkAddress.Text;

                System.Diagnostics.Process.Start("explorer", url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konnte die Adresse nicht öffnen:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
