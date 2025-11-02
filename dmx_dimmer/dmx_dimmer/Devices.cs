using DmxRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace dmx_dimmer
{
    public partial class Devices : Form
    {
        private readonly DMX_Engine _engine;
        private readonly string _templatesPath;
        private readonly List<DeviceInstance> _addedDevices = new(); // lokale Geräteliste

        private readonly string folder = ConfigurationManager.AppSettings["DeviceTemplatesPath"];

        private int _nextId = 1;                    // fortlaufende Idx

        private sealed class DeviceInstance
        {
            public int Idx { get; set; } 
            public string Name { get; set; }
            public string TemplateName { get; set; }
            public int Universe { get; set; }
            public int StartChannel { get; set; }
            public int Footprint { get; set; }
        }


        // Template-Modell
        private sealed class DeviceTemplate
        {
            public string Name { get; init; }
            public string Category { get; init; }
            public string Description { get; init; }
            public string FilePath { get; init; }
            public int Footprint { get; init; }
        }


        public Devices(DMX_Engine engine = null, string templatesPath = null)
        {
            InitializeComponent();

            _engine = engine;
            _templatesPath = templatesPath ?? folder;

            InitDeviceListView();
            HookTreeEvents();
            LoadDeviceTemplatesIntoTree();
        }

        // ===================== UI: Geräteliste =====================
        private void InitDeviceListView()
        {
            if (proj_device == null) return;

            proj_device.View = View.Details;
            proj_device.FullRowSelect = true;
            proj_device.GridLines = true;

            proj_device.Columns.Clear();
            proj_device.Columns.Add("Idx", 60);         
            proj_device.Columns.Add("Name", 160);
            proj_device.Columns.Add("Template", 120);
            proj_device.Columns.Add("Universe", 80);
            proj_device.Columns.Add("Startkanal", 80);
            proj_device.Columns.Add("Footprint", 80);
        }


        // ===================== Tree =====================
        private void HookTreeEvents()
        {
            device_tree.AfterSelect -= device_tree_AfterSelect;
            device_tree.AfterSelect += device_tree_AfterSelect;
        }

        private void LoadDeviceTemplatesIntoTree()
        {
            device_tree.BeginUpdate();
            device_tree.Nodes.Clear();

            var rootNode = device_tree.Nodes.Add("Gerätebibliothek");
            var categories = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(_templatesPath) || !Directory.Exists(_templatesPath))
            {
                rootNode.Nodes.Add("(Kein gültiger Vorlagen-Pfad)");
                device_tree.EndUpdate();
                rootNode.Expand();
                return;
            }

            foreach (var file in Directory.EnumerateFiles(_templatesPath, "*.xml", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var dev = ParseDeviceTemplate(file);
                    if (dev == null)
                    {
                        errors.Add(Path.GetFileName(file) + " (unerwartete Struktur)");
                        continue;
                    }

                    if (!categories.TryGetValue(dev.Category, out var catNode))
                    {
                        catNode = rootNode.Nodes.Add(dev.Category);
                        categories[dev.Category] = catNode;
                    }

                    var node = new TreeNode(dev.Name) { Tag = dev };
                    catNode.Nodes.Add(node);
                }
                catch (Exception ex)
                {
                    errors.Add(Path.GetFileName(file) + " (" + ex.GetType().Name + ")");
                }
            }

            device_tree.EndUpdate();
            rootNode.Expand();

            // Hilfreiche Mini-Zusammenfassung zeigen
            if (errors.Count > 0)
            {
                MessageBox.Show(
                    "Einige Vorlagen konnten nicht geladen werden:\n- " +
                    string.Join("\n- ", errors.Take(10)) +
                    (errors.Count > 10 ? $"\n…(+{errors.Count - 10} weitere)" : ""),
                    "Vorlagen prüfen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }


        private static DeviceTemplate ParseDeviceTemplate(string filePath)
        {
            // Whitespace/Kommentare ignorieren, damit „kommentarlastige“ Dateien sicher geladen werden
            var settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
                DtdProcessing = DtdProcessing.Ignore
            };

            using var reader = XmlReader.Create(filePath, settings);
            var xdoc = XDocument.Load(reader, LoadOptions.None);

            var root = xdoc.Root;
            if (root == null) return null;

            // Root-Name case-insensitive prüfen
            if (!string.Equals(root.Name.LocalName, "DeviceTemplate", StringComparison.OrdinalIgnoreCase))
                return null;

            // Name: Attribut oder Fallback auf Dateiname
            string name = (string)root.Attribute("name")
                         ?? (string)root.Element("Name")
                         ?? Path.GetFileNameWithoutExtension(filePath);

            // Kategorie: Attribut oder Element, Fallback
            string category = (string)root.Attribute("category")
                             ?? (string)root.Element("Category")
                             ?? "Ohne Kategorie";

            // Beschreibung optional
            string description = (string)root.Element("Description") ?? "";

            // Footprint zählen (Channels kann direkt unter Root ODER in ControllType liegen)
            int footprint = 0;
            var channelsRoot =
                root.Element("Channels") ??
                root.Element("ControllType")?.Element("Channels");
            if (channelsRoot != null)
                footprint = channelsRoot.Elements("Channel").Count();

            return new DeviceTemplate
            {
                Name = name,
                Category = category,
                Description = description,
                FilePath = filePath,
                Footprint = footprint
            };
        }

        // ===================== Tree-Event =====================
        private void device_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not DeviceTemplate dev)
            {
                if (device_info != null) device_info.Text = "";
                return;
            }

            if (device_info != null)
                device_info.Text = $"{dev.Name} ({dev.Category}){Environment.NewLine}{Environment.NewLine}{dev.Description}";
        }

        // ===================== Button: Neues Gerät =====================
        private void add_device_Click(object sender, EventArgs e)
        {
            if (device_tree.SelectedNode?.Tag is not DeviceTemplate selectedTemplate)
            {
                MessageBox.Show("Bitte zuerst ein Gerätetemplate in der linken Liste auswählen.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (AddDevice dlg = new AddDevice(selectedTemplate.Name, 0, 1))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var newDev = new DeviceInstance
                    {
                        Idx = _nextId++,                    
                        Name = dlg.DeviceName,
                        TemplateName = selectedTemplate.Name,
                        Universe = dlg.Universe,
                        StartChannel = dlg.StartChannel,
                        Footprint = 3 // ggf. aus Template ermitteln
                    };

                    _addedDevices.Add(newDev);
                    UpdateDeviceListView();
                }
            }

        }

        private void UpdateDeviceListView()
        {
            proj_device.BeginUpdate();
            proj_device.Items.Clear();

            foreach (var dev in _addedDevices)
            {
                var item = new ListViewItem(dev.Idx.ToString());  
                item.SubItems.Add(dev.Name);
                item.SubItems.Add(dev.TemplateName);
                item.SubItems.Add(dev.Universe.ToString());
                item.SubItems.Add(dev.StartChannel.ToString());
                item.SubItems.Add(dev.Footprint.ToString());
                item.Tag = dev;
                proj_device.Items.Add(item);
            }

            proj_device.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            proj_device.EndUpdate();
        }

    }
}
