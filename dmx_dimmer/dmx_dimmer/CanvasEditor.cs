using MiniWebPanel;
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
    public partial class CanvasEditor : Form
    {
        public CanvasState State { get; private set; } = new();
        public event Action<CanvasState>? StateChanged;

        private Point _dragOffset;
        private Control? _dragControl;

        // ✅ Für den Designer / falls du später per LoadState lädst
        public CanvasEditor()
        {
            InitializeComponent();
            InitUi();
        }

        // ✅ Konstruktor mit State + chaining
        public CanvasEditor(CanvasState canvas) : this()
        {
            LoadState(canvas); // <— Wichtig!
        }

        private void InitUi()
        {
            var cm = new ContextMenuStrip();
            cm.Items.Add("Button hinzufügen", null, (_, __) => AddButtonAt(MousePositionToCanvas()));
            canvasPanel.ContextMenuStrip = cm;

            canvasPanel.AllowDrop = false;
        }

        public void LoadState(CanvasState state)
        {
            State = state ?? new CanvasState();
            Redraw();
        }

        private void Redraw()
        {
            canvasPanel.Controls.Clear();
            foreach (var w in State.Widgets)
            {
                var b = MakeButton(w);
                canvasPanel.Controls.Add(b);
            }
        }

        private Button MakeButton(Widget w)
        {
            var b = new Button
            {
                Text = w.Text,
                Left = w.X,
                Top = w.Y,
                Width = w.Width,
                Height = w.Height,
                Tag = w.Id
            };
            b.MouseDown += StartDrag;
            b.MouseMove += DoDrag;
            b.MouseUp += EndDrag;
            b.DoubleClick += (_, __) =>
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox("Text:", "Beschriftung", w.Text);
                if (!string.IsNullOrWhiteSpace(input))
                {
                    w.Text = input;
                    b.Text = input;
                    Publish();
                }
            };
            return b;
        }

        private void AddButtonAt(Point p)
        {
            var w = new Widget { X = p.X, Y = p.Y };
            State.Widgets.Add(w);
            var b = MakeButton(w);
            canvasPanel.Controls.Add(b);
            Publish();
        }

        private Point MousePositionToCanvas()
        {
            var p = canvasPanel.PointToClient(Cursor.Position);
            if (p.X < 0) p.X = 0; if (p.Y < 0) p.Y = 0;
            return p;
        }

        private void StartDrag(object? sender, MouseEventArgs e)
        {
            _dragControl = sender as Control;
            if (_dragControl == null) return;
            _dragOffset = new Point(e.X, e.Y);
        }

        private void DoDrag(object? sender, MouseEventArgs e)
        {
            if (_dragControl == null) return;
            if (e.Button != MouseButtons.Left) return;

            var pos = canvasPanel.PointToClient(Cursor.Position);
            _dragControl.Left = Math.Max(0, pos.X - _dragOffset.X);
            _dragControl.Top = Math.Max(0, pos.Y - _dragOffset.Y);
        }

        private void EndDrag(object? sender, MouseEventArgs e)
        {
            if (_dragControl == null) return;
            var id = (string)_dragControl.Tag;
            var w = State.Widgets.First(x => x.Id == id);
            w.X = _dragControl.Left;
            w.Y = _dragControl.Top;
            Publish();
            _dragControl = null;
        }

        private void Publish() => StateChanged?.Invoke(State);
    }
}
