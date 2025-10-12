using DmxRuntime; // für DMX_Engine, MergeMode etc.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace dmx_dimmer
{
    public partial class Devices : Form
    {
        private readonly DeviceStore _store;              // zentrale Datenhaltung (IDs, Kollisionen)
        private readonly DMX_Engine _engine;              // optional: für Live-Defaults
        private DeviceLibrary _lib;                       // lädt XML-Vorlagen
        private readonly string _templatesPath;           // Ordner der Geräte-XMLs

        string folder = ConfigurationManager.AppSettings["DeviceTemplatesPath"];

        public Devices(DeviceStore store, DMX_Engine engine = null, string templatesPath = null)
        {
            InitializeComponent();
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _engine = engine;
            _templatesPath = templatesPath
                ?? Path.Combine(folder);

            // bei Änderungen im Store Liste neu aufbauen
            _store.Changed += (_, __) => RefreshList();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitListView();

            Directory.CreateDirectory(_templatesPath);
            _lib = DeviceLibrary.LoadFromFolder(_templatesPath);

            BuildTree();
            RefreshList();

            // Events zuweisen (nur wenn nicht schon im Designer gesetzt)
            proj_devices.SelectedIndexChanged += proj_devices_SelectedIndexChanged;
            device_tree.AfterSelect += device_tree_AfterSelect;
        }

        private TreeNode FindFirstTemplateNodeRecursive(TreeNode node)
        {
            if (node?.Tag is DeviceTemplate) return node;
            foreach (TreeNode child in node.Nodes)
            {
                var f = FindFirstTemplateNodeRecursive(child);
                if (f != null) return f;
            }
            return null;
        }



        // ===================== UI: Tree & List =====================

        private void InitListView()
        {
            proj_devices.View = View.Details;
            proj_devices.FullRowSelect = true;
            proj_devices.GridLines = true;

            if (proj_devices.Columns.Count == 0)
            {
                proj_devices.Columns.Add("ID", 60);
                proj_devices.Columns.Add("Name", 160);
                proj_devices.Columns.Add("Typ", 160);
                proj_devices.Columns.Add("Universe", 80);
                proj_devices.Columns.Add("Start", 70);
                proj_devices.Columns.Add("Footprint", 80);
            }
        }

        private void BuildTree()
        {
            device_tree.BeginUpdate();
            device_tree.Nodes.Clear();

            var map = new Dictionary<string, TreeNode>();
            foreach (var (path, t) in _lib.GroupedByCategory())
            {
                TreeNode parent = null;
                string running = "";
                for (int i = 0; i < path.Length; i++)
                {
                    running = (i == 0) ? path[0] : $"{running}/{path[i]}";
                    if (!map.TryGetValue(running, out var node))
                    {
                        node = new TreeNode(path[i]);
                        if (i == 0) device_tree.Nodes.Add(node);
                        else parent.Nodes.Add(node);
                        map[running] = node;
                    }
                    parent = node;
                }
                var leaf = new TreeNode(t.Name) { Tag = t };
                parent.Nodes.Add(leaf);
            }

            device_tree.ExpandAll();
            device_tree.EndUpdate();
        }

        private void RefreshList()
        {
            var items = _store.GetAll();

            proj_devices.BeginUpdate();
            proj_devices.Items.Clear();

            foreach (var d in items)
            {
                var it = new ListViewItem(d.Id.ToString());
                it.SubItems.Add(d.Label);
                it.SubItems.Add(d.Template.Name);
                it.SubItems.Add(d.Universe.ToString());
                it.SubItems.Add(d.StartChannel.ToString());
                it.SubItems.Add(d.Footprint.ToString());
                it.Tag = d;
                proj_devices.Items.Add(it);
            }

            proj_devices.EndUpdate();
        }

        // ===================== Tree/Selection (optional) =====================

        private void device_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is DeviceTemplate t)
                ShowTemplateDescription(t);
        }

        private void proj_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proj_devices.SelectedItems.Count == 0) return;

            var it = proj_devices.SelectedItems[0];
            // Annahme: .Tag trägt eine DeviceInstance mit Eigenschaft .Template
            if (it.Tag is DeviceInstance inst && inst.Template != null)
                ShowTemplateDescription(inst.Template);
        }

        // Zeigt die Gerätebeschreibung in dem Label "device_info" an
        private void ShowTemplateDescription(DeviceTemplate t)
        {
            if (device_info == null) return; // falls im Designer noch nicht vorhanden
            device_info.Text = "";
            device_info.Text = (t != null && !string.IsNullOrWhiteSpace(t.Description))
                ? t.Description
                : "(Keine Beschreibung vorhanden.)";
        }

        // ===================== "+" Button =====================

        private void add_device_Click(object sender, EventArgs e)
        {
            if (device_tree.SelectedNode?.Tag is not DeviceTemplate t)
            {
                MessageBox.Show("Bitte links eine Gerätevorlage auswählen.",
                    "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Vorschlag anhand aktueller Belegung im Store
            const ushort defaultUniverse = 0;
            int suggestedStart = _store.SuggestStart(t, defaultUniverse);

            using var dlg = new AddDeviceDialog(t.Name, suggestedStart, defaultUniverse);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var inst = _store.Add(t, dlg.DeviceLabel, dlg.Universe, dlg.StartChannel, out string err);
            if (inst == null)
            {
                MessageBox.Show(err ?? "Unbekannter Fehler.",
                    "Konnte Gerät nicht hinzufügen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Optional: Defaults sofort senden
            if (_engine != null)
            {
                foreach (var ch in inst.Template.Channels)
                {
                    int absolute = inst.StartChannel + ch.Offset; // 1-basiert
                    _engine.SetChannel(absolute, ch.Default, 0, ch.MergeMode);
                }
            }

            // Liste aktualisiert sich auch via Store.Changed; hier zur Sicherheit:
            RefreshList();
        }

        // ===================== Hilfen =====================

        // Wenn du den Templates-Pfad zur Laufzeit umstellen möchtest:
        public void ReloadTemplatesFrom(string newPath)
        {
            if (string.IsNullOrWhiteSpace(newPath)) return;
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            _lib = DeviceLibrary.LoadFromFolder(newPath);
            BuildTree();
        }
    }

    // ===================== Adress-Dialog =====================

    internal sealed class AddDeviceDialog : Form
    {
        private readonly TextBox txtLabel = new() { Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
        private readonly NumericUpDown numUniverse = new() { Minimum = 0, Maximum = 32767 };
        private readonly NumericUpDown numStart = new() { Minimum = 1, Maximum = 512 };
        private readonly Button btnOk = new() { Text = "OK", DialogResult = DialogResult.OK };
        private readonly Button btnCancel = new() { Text = "Abbrechen", DialogResult = DialogResult.Cancel };

        public string DeviceLabel => txtLabel.Text.Trim();
        public ushort Universe => (ushort)numUniverse.Value;
        public int StartChannel => (int)numStart.Value;

        public AddDeviceDialog(string templateName, int suggestedStart, ushort suggestedUniverse)
        {
            Text = $"Gerät hinzufügen – {templateName}";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = MaximizeBox = false;
            AcceptButton = btnOk; CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(360, 160);

            var lbl1 = new Label { Text = "Name:", Left = 12, Top = 14, AutoSize = true };
            txtLabel.Left = 120; txtLabel.Top = 10; txtLabel.Width = 220; txtLabel.Text = templateName;

            var lbl2 = new Label { Text = "Universe:", Left = 12, Top = 48, AutoSize = true };
            numUniverse.Left = 120; numUniverse.Top = 44; numUniverse.Value = suggestedUniverse;

            var lbl3 = new Label { Text = "Startkanal:", Left = 12, Top = 82, AutoSize = true };
            numStart.Left = 120; numStart.Top = 78; numStart.Value = suggestedStart;

            btnOk.Width = 80; btnCancel.Width = 90;
            btnOk.Left = ClientSize.Width - 180; btnOk.Top = 115;
            btnCancel.Left = ClientSize.Width - 90; btnCancel.Top = 115;

            Controls.AddRange(new Control[] {
                lbl1, txtLabel, lbl2, numUniverse, lbl3, numStart, btnOk, btnCancel
            });
        }
    }
}
