using System;
using System.Collections.Generic;
using System.Linq;

namespace dmx_dimmer
{
    /// <summary>
    /// Konkrete Instanz eines Geräts im Projekt (mit Adresse, ID, Label).
    /// </summary>
    public sealed class DeviceInstance
    {
        /// <summary>Eindeutige laufende ID (vom DeviceStore vergeben).</summary>
        public int Id { get; }

        /// <summary>Vorlage aus der DeviceLibrary (liefert Kanäle, Defaults, MergeMode etc.).</summary>
        public DeviceTemplate Template { get; }

        /// <summary>Anzeigename im UI (frei wählbar).</summary>
        public string Label { get; set; }

        /// <summary>Universe des Geräts.</summary>
        public ushort Universe { get; set; }

        /// <summary>Startkanal (1..512) des Geräts im Universe.</summary>
        public int StartChannel { get; set; }

        /// <summary>Belegte DMX-Slots (aus Template berechnet): max Offset + 1.</summary>
        public int Footprint { get; }

        public DeviceInstance(int id, DeviceTemplate template, string label, int startChannel, ushort universe)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
            if (template.Channels == null || template.Channels.Count == 0)
                throw new ArgumentException("Template hat keine Kanäle.", nameof(template));

            Id = id;
            Label = string.IsNullOrWhiteSpace(label) ? template.Name : label;
            StartChannel = startChannel;
            Universe = universe;

            Footprint = template.Channels.Max(c => c.Offset) + 1;
        }

        /// <summary>
        /// Liefert die absolute DMX-Kanalnummer (1..512) für einen benannten Kanal der Vorlage.
        /// </summary>
        public int ChannelNumber(string channelName)
        {
            if (string.IsNullOrWhiteSpace(channelName))
                throw new ArgumentException("Channel-Name darf nicht leer sein.", nameof(channelName));

            var cd = Template.Channels.FirstOrDefault(c => c.Name == channelName)
                     ?? throw new KeyNotFoundException($"Kanal '{channelName}' existiert nicht im Template '{Template.Name}'.");
            return StartChannel + cd.Offset; // 1-basiert
        }

        /// <summary>
        /// Alle absoluten Kanäle (1..512), die diese Instanz belegt.
        /// </summary>
        public IEnumerable<int> AbsoluteChannels()
        {
            foreach (var c in Template.Channels)
                yield return StartChannel + c.Offset; // 1-basiert
        }

        /// <summary>
        /// Komfort: vollständige Kanal-Map inkl. Namen, MergeMode und Default.
        /// </summary>
        public IReadOnlyList<(string Name, int Absolute, DmxRuntime.MergeMode Mode, byte Default)> ChannelMap()
        {
            return Template.Channels
                .Select(c => (c.Name, Absolute: StartChannel + c.Offset, c.MergeMode, c.Default))
                .ToList();
        }

        public override string ToString()
            => $"{Id}: {Label} [{Template.Name}] U{Universe} @ {StartChannel} (+{Footprint - 1})";
    }
}
