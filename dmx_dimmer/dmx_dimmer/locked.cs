using dmx_dimmer.Properties;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class locked : Form
    {
        private string _password;
        private bool _unlocked = false;
        private bool pwVisible = false;

        public locked()
        {
            InitializeComponent();
            GetPassword();
            hint.Visible = false;

            this.ControlBox = true; // auf false setzen, wenn das X nicht erlaubt sein soll
            entered_password.UseSystemPasswordChar = true;
            this.AcceptButton = button1;
        }

        private void GetPassword()
        {
            _password = ConfigurationManager.AppSettings["Password"] ?? "";

            if(string.Equals(_password, "")
            {
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string entered = entered_password.Text;

            if (string.Equals(_password, entered))
            {
                _unlocked = true;
                this.Close(); // erlaubt das Schließen
            }
            else
            {
                hint.Text = "Falsches Kennwort!";
                hint.Visible = true;
            }
        }

        private void locked_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_unlocked && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                hint.Text = "Bitte korrektes Passwort eingeben.";
                hint.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwVisible = !pwVisible;
            entered_password.UseSystemPasswordChar = !pwVisible;

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
