using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace dmx_dimmer
{
    /// <summary>
    /// Speichert/Lädt beliebige Objekte als XML in/aus .dmxproj (ZIP).
    /// Du gibst beim Speichern nur (Dateiname, Instanz) an.
    /// </summary>
    public static class ProjectManager
    {
        private static readonly object _ioLock = new();

        /// <summary>Zuletzt verwendeter Projektpfad (für "Schnell speichern").</summary>
        public static string? CurrentPath { get; private set; }

        /// <summary>
        /// Beschreibt einen zu speichernden Teil: Dateiname + Objekt.
        /// Optional kannst du den Typ explizit setzen (z.B. bei Interfaces/Basistypen).
        /// </summary>
        public sealed record Part(string EntryName, object Data, Type? ExplicitType = null);

        // -------------------- SAVE --------------------

        /// <summary>
        /// Speichert beliebige Parts (Dateiname + Instanz) in ein .dmxproj-Archiv.
        /// Beispiel:
        /// ProjectSave("C:\\Projekte\\Show1.dmxproj",
        ///     new Part("canvas.xml", canvasState),
        ///     new Part("stageview.xml", stageViewState));
        /// </summary>
        public static void ProjectSave(string? path, params Part[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentException("Es wurden keine Parts zum Speichern übergeben.", nameof(parts));

            var targetPath = NormalizePath(path ?? CurrentPath)
                ?? throw new ArgumentException("Kein Zielpfad angegeben und CurrentPath ist leer.", nameof(path));

            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);

            lock (_ioLock)
            {
                using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Create);

                // Manifest mit Liste aller Einträge + Typinfos
                var sb = new StringBuilder();
                sb.AppendLine("dmxproj/2.0");
                sb.AppendLine($"saved:{DateTime.UtcNow:O}");
                sb.AppendLine("format:XML");
                sb.AppendLine("entries:");
                foreach (var p in parts)
                {
                    var type = (p.ExplicitType ?? p.Data?.GetType())?.AssemblyQualifiedName ?? "null";
                    sb.AppendLine($"{EnsureXmlName(p.EntryName)}|{type}");
                }
                AddStringEntry(zip, "manifest.txt", sb.ToString());

                // Alle Parts als XML ins Archiv
                foreach (var p in parts)
                {
                    if (p.Data == null)
                        throw new ArgumentNullException(nameof(p.Data), $"Part '{p.EntryName}' hat null-Daten.");

                    var entryName = EnsureXmlName(p.EntryName);
                    var type = p.ExplicitType ?? p.Data.GetType();
                    AddStringEntry(zip, entryName, SerializeXml(p.Data, type));
                }
            }

            CurrentPath = targetPath;
        }

        /// <summary>
        /// Bequemer Overload für "Schnell speichern" (gleicher Pfad wie letztes Projekt).
        /// </summary>
        public static void ProjectSave(params Part[] parts) => ProjectSave(CurrentPath, parts);

        // -------------------- LOAD --------------------

        /// <summary>
        /// Lädt EIN Objekt vom angegebenen entryName aus dem .dmxproj.
        /// </summary>
        public static T ProjectLoad<T>(string path, string entryName)
        {
            var sourcePath = ValidateExistingPath(path);
            var normalized = EnsureXmlName(entryName);

            lock (_ioLock)
            {
                using var fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Read);

                var e = zip.GetEntry(normalized)
                    ?? throw new InvalidDataException($"Eintrag '{normalized}' wurde im Projekt nicht gefunden.");

                using var s = e.Open();
                var obj = DeserializeXml<T>(s);
                CurrentPath = sourcePath;
                return obj!;
            }
        }

        /// <summary>
        /// Lädt MEHRERE Objekte auf einmal. Du gibst (EntryName, Zieltyp) an.
        /// Rückgabe ist ein Dictionary: entryName -> Objekt.
        /// </summary>
        public static Dictionary<string, object?> ProjectLoadMany(string path, params (string entryName, Type type)[] requests)
        {
            var sourcePath = ValidateExistingPath(path);
            var result = new Dictionary<string, object?>();

            lock (_ioLock)
            {
                using var fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var zip = new ZipArchive(fs, ZipArchiveMode.Read);

                foreach (var (entryName, type) in requests)
                {
                    var normalized = EnsureXmlName(entryName);
                    var e = zip.GetEntry(normalized)
                        ?? throw new InvalidDataException($"Eintrag '{normalized}' wurde im Projekt nicht gefunden.");
                    using var s = e.Open();
                    result[normalized] = DeserializeXml(s, type);
                }
                CurrentPath = sourcePath;
            }

            return result;
        }

        // -------------------- Helpers --------------------

        private static string? NormalizePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;
            var p = path!;
            if (!Path.GetExtension(p).Equals(".dmxproj", StringComparison.OrdinalIgnoreCase))
                p = Path.ChangeExtension(p, ".dmxproj");
            return Path.GetFullPath(p);
        }

        private static string ValidateExistingPath(string path)
        {
            var sourcePath = NormalizePath(path)
                ?? throw new ArgumentException("Ungültiger Pfad.", nameof(path));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Projektdatei nicht gefunden.", sourcePath);
            return sourcePath;
        }

        private static string EnsureXmlName(string entryName)
        {
            if (string.IsNullOrWhiteSpace(entryName))
                throw new ArgumentException("EntryName darf nicht leer sein.", nameof(entryName));

            return Path.GetExtension(entryName).Equals(".xml", StringComparison.OrdinalIgnoreCase)
                ? entryName.Replace('\\', '/')
                : (entryName + ".xml").Replace('\\', '/');
        }

        private static void AddStringEntry(ZipArchive zip, string entryName, string content)
        {
            var entry = zip.CreateEntry(entryName.Replace('\\', '/'), CompressionLevel.Optimal);
            using var s = new StreamWriter(entry.Open(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            s.Write(content);
        }

        private static string SerializeXml(object obj, Type type)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty); // keine xmlns
            var serializer = new XmlSerializer(type);
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

        private static object? DeserializeXml(Stream stream, Type type)
        {
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(stream);
        }
    }
}
