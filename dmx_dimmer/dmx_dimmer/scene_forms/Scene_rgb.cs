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

namespace dmx_dimmer.scene_forms
{
    public partial class Scene_rgb : Form
    {
        private Color selectedColor = Color.Black;
        private readonly DMX_Engine _engine;
        private int _rgbBaseCh = 5;

        public Scene_rgb(DMX_Engine engine)
        {
            InitializeComponent();
            _engine = engine;
            this.Shown += (s, e) => OpenColorDialog();
        }

        private void OpenColorDialog()
        {
            colorPicker.FullOpen = true; // optional
            if (colorPicker.ShowDialog(this) == DialogResult.OK)
            {
                selectedColor = colorPicker.Color;
                panelColorPreview.BackColor = selectedColor;
                ApplyColorToDevice();
            }
        }

        private void ApplyColorToDevice()
        {
            if (_engine == null || !_engine.IsRunning) return;
            if (selectedColor == null) return; // falls dein Code das zulässt

            // Optional: Gamma-Korrektur (wirkt natürlicher auf LEDs)
            byte r = Gamma8(selectedColor.R);
            byte g = Gamma8(selectedColor.G);
            byte b = Gamma8(selectedColor.B);

            var values = new Dictionary<int, byte>
    {
        { _rgbBaseCh + 0, r }, // R
        { _rgbBaseCh + 1, g }, // G
        { _rgbBaseCh + 2, b }  // B
    };

            
            _engine.SetMany(values, priority: 0, mode: MergeMode.LTP);
        }

        // einfache Gamma-Kurve
        private static byte Gamma8(byte v)
        {
            // 0..255 -> 0..255
            double x = v / 255.0;
            double y = Math.Pow(x, 2.2);
            return (byte)Math.Round(y * 255.0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenColorDialog();
        }
    }

}
