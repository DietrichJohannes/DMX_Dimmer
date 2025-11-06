# DMX_DIMMER

**DMX_DIMMER** ist ein modular aufgebautes Lichtsteuerungs-System für DMX512 über Art-Net.  
Es besteht aus einer C# Windows Forms Anwendung zur Geräte- und Szenensteuerung sowie einer C-basierten Library, die das Senden von DMX-Daten über Art-Net übernimmt.

---

## 📌 Projektübersicht

| Komponente     | Beschreibung |
|----------------|--------------|
| **dmx_dimmer** | C# Windows Forms Applikation. Dient als grafische Bedienoberfläche zur Konfiguration, Steuerung und Live-Ausgabe von DMX-Werten. |
| **dmx_sender** | Native C-Bibliothek zur Kommunikation mit einem Art-Net Node. Stellt Funktionen bereit, um DMX-Daten effizient über UDP zu senden. |
| **files / devices** | Enthält `.xml`-Dateien zur Definition von DMX-Geräten (z. B. Kanalanzahl, Parameter, Farbmapping etc.). |

---

## 🖥️ dmx_dimmer (C# Windows Forms)

- Geräte hinzufügen / bearbeiten
- Szenen erstellen
- Live-Fader Panel
- Bühnengrafik (Widget-basiert, frei positionierbare Geräte)
- Speichern & Laden von Projekten (`.dmxproj` Format)
- Übergibt sämtliche Kanaländerungen an die `dmx_sender` Bibliothek

---

## 🔧 dmx_sender (C Bibliothek)

- Implementiert Art-Net Protokoll (ArtDMX Pakete)
- Senden von Universen über UDP
- Optimiert für niedrige Latenz
- Wird per P/Invoke aus C# angesprochen

---

## 🗂️ Geräte-Konfiguration (`/files/devices/`)

Geräte werden in **XML** definiert.