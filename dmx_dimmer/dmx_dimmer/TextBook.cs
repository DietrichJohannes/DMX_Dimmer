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
    public partial class TextBook : Form
    {
        public TextBook()
        {
            InitializeComponent();
            // Wichtig: sonst werden Tastendrücke an die Controls weitergereicht
            this.KeyPreview = true;

            // Event abonnieren
            this.KeyDown += TextBook_KeyDown;
        }

        private void TextBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Message();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Message();
        }

        private void Message()
        {
            MessageBox.Show("Effekt gestartet!");
        }
    }
}
