using DmxRuntime;
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
    public partial class Scene : Form
    {
        private readonly DMX_Engine _engine;

        public Scene(DMX_Engine engine)
        {
            InitializeComponent();
            _engine = engine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scene_forms.Scene_rgb scene_rgb = new scene_forms.Scene_rgb(_engine);
            scene_rgb.Show();
        }
    }
}
