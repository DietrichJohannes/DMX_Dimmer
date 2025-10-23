using dmx_dimmer.Properties;
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
    public partial class GraphicStageView : Form
    {
        public GraphicStageView()
        {
            InitializeComponent();

            // Beispiel: 3 Scheinwerfer erstellen
            CreateFixture("Spot 1", 50, 50);
            CreateFixture("Spot 2", 50, 150);
            CreateFixture("Spot 3", 50, 250);
        }

        private void CreateFixture(string name, int x, int y)
        {
            PictureBox fixture = new PictureBox();
            fixture.Width = 40;
            fixture.Height = 40;
            fixture.Image = Resources.Scheinwerfer;
            fixture.BorderStyle = BorderStyle.None;
            fixture.Tag = name;

            // Position setzen
            fixture.Left = x;
            fixture.Top = y;

            // Drag-Ereignisse registrieren
            fixture.MouseDown += Fixture_MouseDown;
            fixture.MouseMove += Fixture_MouseMove;
            fixture.MouseUp += Fixture_MouseUp;

            panelStage.Controls.Add(fixture);
        }

        private bool isDragging = false;
        private Point dragStart;

        private void Fixture_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStart = e.Location;
            ((PictureBox)sender).BringToFront(); // Immer oben
        }

        private void Fixture_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                PictureBox fixture = (PictureBox)sender;
                fixture.Left += e.X - dragStart.X;
                fixture.Top += e.Y - dragStart.Y;
            }
        }

        private void Fixture_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            // Optional: Position speichern
            var fixture = (PictureBox)sender;
            Console.WriteLine($"{fixture.Tag} -> X:{fixture.Left}, Y:{fixture.Top}");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}