using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace dmx_dimmer
{
    /// <summary>
    /// Speichert/Lädt CanvasState in/aus .dmxproj (ZIP mit 'canvas.xml').
    /// </summary>
    public static class ProjectManager
    {
        private const string XmlEntryName = "canvas.xml";
        private static readonly object _ioLock = new();

        /// <summary>Zuletzt verwendeter Projektpfad (optional für "Schnell speichern").</summary>
        public static string? CurrentPath { get; private set; }

        /// <summary>
        /// Speichert den angegebenen CanvasState in ein .dmxproj (ZIP-Archiv mit 'canvas.xml').
        /// Wenn path null ist, wird CurrentPath verwendet (falls vorhanden), sonst Exception.
        /// </summary>
        public static void ProjectSave(CanvasState state, string? path = null)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            var targetPath = NormalizePath(path ?? CurrentPath)
                ?? throw new ArgumentException("Kein Zielpfad angegeben und CurrentPath ist leer.");

            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);

            lock (_ioLock)
            {
                using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Create);

                // Optionales Mini-Manifest
                var manifest = $"dmxproj/1.0\nsaved:{DateTime.UtcNow:O}\nformat:XML\n";
                AddStringEntry(zip, "manifest.txt", manifest);

                // Nutzdaten (XML)
                AddStringEntry(zip, XmlEntryName, SerializeXml(state));
            }

            CurrentPath = targetPath;
        }

        /// <summary>
        /// Lädt einen CanvasState aus einem .dmxproj (ZIP-Archiv mit 'canvas.xml').
        /// </summary>
        public static CanvasState ProjectLoad(string path)
        {
            var sourcePath = NormalizePath(path)
                ?? throw new ArgumentException("Ungültiger Pfad.", nameof(path));

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Projektdatei nicht gefunden.", sourcePath);

            lock (_ioLock)
            {
                using var fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Read);

                var xmlEntry = zip.GetEntry(XmlEntryName)
                    ?? throw new InvalidDataException("Ungültiges .dmxproj: 'canvas.xml' fehlt.");

                using var s = xmlEntry.Open();
                var state = DeserializeXml<CanvasState>(s) ?? new CanvasState();
                CurrentPath = sourcePath;
                return state;
            }
        }

        // ---------- Helper ----------

        private static string? NormalizePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;
            var p = path!;
            if (!Path.GetExtension(p).Equals(".dmxproj", StringComparison.OrdinalIgnoreCase))
                p = Path.ChangeExtension(p, ".dmxproj");
            return Path.GetFullPath(p);
        }

        private static void AddStringEntry(ZipArchive zip, string entryName, string content)
        {
            var entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);
            using var s = new StreamWriter(entry.Open(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            s.Write(content);
        }

        private static string SerializeXml<T>(T obj)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty); // keine xmlns
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            var settings = new System.Xml.XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                Encoding = new UTF8Encoding(false)
            };
            using (var xw = System.Xml.XmlWriter.Create(ms, settings))
                serializer.Serialize(xw, obj, ns);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        private static T? DeserializeXml<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T?)serializer.Deserialize(stream);
        }
    }
}
