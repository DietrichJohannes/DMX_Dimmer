using dmx_dimmer.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class GraphicStageView : Form
    {
        private readonly GraphicStageViewState _state;
        private readonly Dictionary<PictureBox, FixtureState> _map = new();

        private bool _isDragging = false;
        private Point _dragStart;

        // Optional: bestehenden State reinreichen, sonst neuer
        public GraphicStageView(GraphicStageViewState state)
        {
            InitializeComponent();
            _state = state;

            // Fixtures aus State laden
            foreach (var fx in _state.Fixtures)
                CreateFixtureControl(fx);

            // Falls noch keine Fixtures existieren --> Beispiel anlegen
            if (_state.Fixtures.Count == 0)
            {
                AddFixtureToStateAndUI(new FixtureState { Name = "Spot 1", X = 50, Y = 50 });
                AddFixtureToStateAndUI(new FixtureState { Name = "Spot 2", X = 50, Y = 150});
                AddFixtureToStateAndUI(new FixtureState { Name = "Spot 3", X = 50, Y = 250});
                AddFixtureToStateAndUI(new FixtureState { Name = "Spot 4", X = 50, Y = 350});
            }
        }

        // Zugriff nach außen, um den (geänderten) State abzuholen
        public GraphicStageViewState GetState() => _state;

        // --- UI-Erzeugung ---

        private void AddFixtureToStateAndUI(FixtureState fixtureState)
        {
            _state.Fixtures.Add(fixtureState);
            CreateFixtureControl(fixtureState);
        }

        private void CreateFixtureControl(FixtureState fx)
        {
            var fixture = new PictureBox
            {
                Width = fx.Width,
                Height = fx.Height,
                Image = Resources.Scheinwerfer,
                BorderStyle = BorderStyle.None,
                Tag = fx.Name,
                Left = fx.X,
                Top = fx.Y,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.SizeAll
            };

            // Drag-Events
            fixture.MouseDown += Fixture_MouseDown;
            fixture.MouseMove += Fixture_MouseMove;
            fixture.MouseUp += Fixture_MouseUp;

            panelStage.Controls.Add(fixture);
            _map[fixture] = fx;
        }

        // --- Dragging ---

        private void Fixture_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb)
            {
                _isDragging = true;
                _dragStart = e.Location;
                pb.BringToFront();
            }
        }

        private void Fixture_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging && sender is PictureBox pb)
            {
                pb.Left += e.X - _dragStart.X;
                pb.Top += e.Y - _dragStart.Y;

            }
        }

        private void Fixture_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;

            if (sender is PictureBox pb && _map.TryGetValue(pb, out var fx))
            {
                // neue Position zurück in den State
                fx.X = pb.Left;
                fx.Y = pb.Top;
                fx.Width = pb.Width;   // falls später resizable
                fx.Height = pb.Height;

                // Debug-Ausgabe
                Console.WriteLine($"{fx.Name} -> X:{fx.X}, Y:{fx.Y}");
            }
        }

        private static void ClampInsidePanel(Control c, Panel container)
        {
            int minX = 0, minY = 0;
            int maxX = Math.Max(0, container.ClientSize.Width - c.Width);
            int maxY = Math.Max(0, container.ClientSize.Height - c.Height);

            c.Left = Math.Min(Math.Max(c.Left, minX), maxX);
            c.Top = Math.Min(Math.Max(c.Top, minY), maxY);
        }

        // Beispiel: neuen Fixture per Button hinzufügen
        private void buttonAddFixture_Click(object sender, EventArgs e)
        {
            var name = $"Spot {_state.Fixtures.Count + 1}";
            AddFixtureToStateAndUI(new FixtureState { Name = name, X = 100, Y = 100 });
        }
    }
}
