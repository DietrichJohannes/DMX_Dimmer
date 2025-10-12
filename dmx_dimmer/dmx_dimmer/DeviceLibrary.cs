using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;


namespace dmx_dimmer
{
    /// <summary>
    /// Lädt alle Gerätevorlagen (.xml) aus einem Ordner
    /// und stellt sie als DeviceTemplate-Objekte bereit.
    /// </summary>
    public sealed class DeviceLibrary
    {
        private readonly List<DeviceTemplate> _templates = new();
        public IReadOnlyList<DeviceTemplate> Templates => _templates;

        private DeviceLibrary() { }

        /// <summary>
        /// Lädt alle .xml-Dateien im angegebenen Ordner (inkl. Unterordner)
        /// </summary>
        public static DeviceLibrary LoadFromFolder(string folder)
        {
            var lib = new DeviceLibrary();

            if (!Directory.Exists(folder))
                return lib;

            foreach (var file in Directory.EnumerateFiles(folder, "*.xml", SearchOption.AllDirectories))
            {
                try
                {
                    var t = DeviceTemplate.FromXml(file);
                    lib._templates.Add(t);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"[DeviceLibrary] Fehler beim Laden {file}: {ex.Message}");
                }
            }

            return lib;
        }

        /// <summary>
        /// Gruppiert alle Templates nach ihrer Kategorie (z. B. "Licht/RGB")
        /// für den TreeView-Aufbau.
        /// </summary>
        public IEnumerable<(string[] path, DeviceTemplate t)> GroupedByCategory()
        {
            foreach (var t in _templates.OrderBy(t => t.Category).ThenBy(t => t.Name))
            {
                string cat = string.IsNullOrWhiteSpace(t.Category) ? "Allgemein" : t.Category;
                yield return (cat.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries), t);
            }
        }
    }

    /// <summary>
    /// Beschreibt eine Gerätevorlage (aus XML geladen)
    /// </summary>
    public sealed class DeviceTemplate
    {
        public string Name { get; init; } = "";
        public string Category { get; init; } = "";
        public string Description { get; init; } = "";
        public List<ChannelDef> Channels { get; } = new();

        public sealed class ChannelDef
        {
            public string Name { get; init; } = "";
            public int Offset { get; init; }          // 0-basiert
            public byte Default { get; init; } = 0;
            public DmxRuntime.MergeMode MergeMode { get; init; } = DmxRuntime.MergeMode.HTP;
        }

        /// <summary>
        /// Lädt eine einzelne .xml-Vorlage von Datei
        /// </summary>
        public static DeviceTemplate FromXml(string path)
        {
            var x = XDocument.Load(path).Root ?? throw new InvalidDataException("Ungültige XML-Struktur");
            var t = new DeviceTemplate
            {
                Name = (string)x.Attribute("name") ?? Path.GetFileNameWithoutExtension(path),
                Category = (string)x.Attribute("category") ?? "",
                Description = (string?)x.Element("Description") ?? ""
            };

            foreach (var c in x.Element("Channels")!.Elements("Channel"))
            {
                var def = new ChannelDef
                {
                    Name = (string)c.Attribute("name")!,
                    Offset = (int)c.Attribute("offset")!,
                    Default = (byte)((int?)c.Attribute("default") ?? 0),
                    MergeMode = Enum.TryParse((string?)c.Attribute("mergeMode"), true, out DmxRuntime.MergeMode m)
                        ? m : DmxRuntime.MergeMode.HTP
                };
                t.Channels.Add(def);
            }

            return t;
        }
    }
}
