// dmx_sender.c  —  compile as: cl /LD dmx_sender.c ws2_32.lib
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>
#include <windows.h>
#include <stdint.h>
#include <stdbool.h>
#include <string.h>
#include <stdio.h>

#pragma comment(lib, "ws2_32.lib")

#define ARTNET_PORT 6454
#define DMX_SLOTS   512
#define ARTDMX_HDR  18
#define MAX_PACKET  (ARTDMX_HDR + DMX_SLOTS)

// --- State ---
static SOCKET             g_sock = INVALID_SOCKET;
static struct sockaddr_in g_dest;
static HANDLE             g_thread = NULL;
static volatile LONG      g_running = 0;
static uint8_t            g_bufA[DMX_SLOTS], g_bufB[DMX_SLOTS];
static volatile LONG      g_front = 0;     // 0 -> A sichtbar, 1 -> B sichtbar
static uint8_t            g_packet[MAX_PACKET];
static int                g_packet_len = 0;
static int                g_fps = 40;
static uint8_t            g_sequence = 1;

// --- Helpers ---
static void build_artnet_header(uint8_t* pkt, int abs_universe, int dlen)
{
    // ID "Art-Net" + 0x00
    const char id[8] = { 'A','r','t','-','N','e','t',0 };
    memcpy(pkt, id, 8);

    // OpCode = ArtDMX (0x5000, little-endian on wire)
    pkt[8] = 0x00;
    pkt[9] = 0x50;

    // ProtVer = 14 (0x000E)
    pkt[10] = 0x00;
    pkt[11] = 0x0E;

    // Sequence (0 = "ignore" bei manchen Nodes, deshalb !=0 sinnvoll)
    pkt[12] = g_sequence;

    // Physical = 0 (optional, hier ungenutzt)
    pkt[13] = 0x00;

    // --- Universe-Packing ---
    // Art-Net: Net(7) : SubSwitch(4) : Universe(4)
    uint8_t uni = (uint8_t)(abs_universe & 0x0F);        // 0..15
    uint8_t subSwitch = (uint8_t)((abs_universe >> 4) & 0x0F); // 0..15
    uint8_t net = (uint8_t)((abs_universe >> 8) & 0x7F); // 0..127

    pkt[14] = (uint8_t)((subSwitch << 4) | uni);  // Low: SubSwitch<<4 | Universe
    pkt[15] = net;                                // High: Net

    // Data length: clamp + even
    if (dlen < 0) dlen = 0;
    if (dlen > DMX_SLOTS) dlen = DMX_SLOTS;
    if (dlen & 1) dlen++;  // even length required

    pkt[16] = (uint8_t)((dlen >> 8) & 0xFF);
    pkt[17] = (uint8_t)(dlen & 0xFF);
}

static DWORD WINAPI send_thread(LPVOID lpParam)
{
    const int universe = (int)(intptr_t)lpParam;
    const int interval_ms = (g_fps > 0) ? (1000 / g_fps) : 25;

    while (InterlockedCompareExchange(&g_running, 0, 0)) {
        DWORD tick = GetTickCount();

        // Frontbuffer wählen
        uint8_t* src = (InterlockedCompareExchange(&g_front, 0, 0) == 0) ? g_bufA : g_bufB;

        // Header + Payload bauen
        g_sequence = (uint8_t)(g_sequence + 1); // rollt natürlich über
        build_artnet_header(g_packet, universe, DMX_SLOTS);
        memcpy(g_packet + ARTDMX_HDR, src, DMX_SLOTS);
        g_packet_len = ARTDMX_HDR + DMX_SLOTS;

        // Senden
        int sent = sendto(g_sock, (const char*)g_packet, g_packet_len, 0,
            (struct sockaddr*)&g_dest, sizeof(g_dest));
        (void)sent; // optional: für Debug prüfen

        // Taktung
        DWORD next_tick = tick + interval_ms;
        DWORD now = GetTickCount();
        if (now < next_tick) {
            Sleep(next_tick - now);
        }
        else {
            Sleep(1);
        }
    }
    return 0;
}

// Utility: kleine Helfer zum Aufräumen in Fehlerfällen
static void cleanup_socket_and_wsa(void)
{
    if (g_sock != INVALID_SOCKET) {
        closesocket(g_sock);
        g_sock = INVALID_SOCKET;
    }
    WSACleanup();
}

// --- API ---
__declspec(dllexport) int __cdecl start_sender(const char* ip, int universe, int fps)
{
    if (InterlockedCompareExchange(&g_running, 0, 0)) return 1; // bereits aktiv?

    // Winsock
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) return -1;

    // Socket
    g_sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (g_sock == INVALID_SOCKET) { WSACleanup(); return -2; }

    // Socket-Optionen
    // 1) Broadcast erlauben (unschädlich für Unicast)
    {
        BOOL on = TRUE;
        setsockopt(g_sock, SOL_SOCKET, SO_BROADCAST, (char*)&on, sizeof(on));
    }
    // 2) Sende-Timeout, damit sendto() nicht lange hängt
    {
        DWORD to = 200; // ms
        setsockopt(g_sock, SOL_SOCKET, SO_SNDTIMEO, (char*)&to, sizeof(to));
    }

    // Zieladresse
    memset(&g_dest, 0, sizeof(g_dest));
    g_dest.sin_family = AF_INET;
    g_dest.sin_port = htons(ARTNET_PORT);

    // inet_addr Besonderheit: INADDR_NONE ist auch 255.255.255.255.
    // Wir behandeln exakt "255.255.255.255" als Broadcast.
    uint32_t addr = inet_addr(ip);
    if (addr == INADDR_NONE) {
        if (strcmp(ip, "255.255.255.255") == 0) {
            g_dest.sin_addr.s_addr = INADDR_BROADCAST;
        }
        else {
            cleanup_socket_and_wsa();
            return -4; // ungültige IP
        }
    }
    else {
        g_dest.sin_addr.s_addr = addr;
    }

    // Buffer init
    memset(g_bufA, 0, DMX_SLOTS);
    memset(g_bufB, 0, DMX_SLOTS);
    InterlockedExchange(&g_front, 0);

    // Sender-Parameter
    g_fps = (fps > 0 && fps <= 44) ? fps : 40;
    g_sequence = 1;

    // Start
    InterlockedExchange(&g_running, 1);
    g_thread = CreateThread(NULL, 0, send_thread, (LPVOID)(intptr_t)universe, 0, NULL);
    if (!g_thread) {
        InterlockedExchange(&g_running, 0);
        cleanup_socket_and_wsa();
        return -3; // Thread-Start fehlgeschlagen
    }
    return 0;
}

__declspec(dllexport) void __cdecl update_dmx(const uint8_t* data, int len)
{
    if (!InterlockedCompareExchange(&g_running, 0, 0)) return;
    if (!data || len <= 0) return;
    if (len > DMX_SLOTS) len = DMX_SLOTS;

    // Backbuffer aktualisieren, Front/Back toggle
    if (InterlockedCompareExchange(&g_front, 0, 0) == 0) {
        memcpy(g_bufB, g_bufA, DMX_SLOTS);
        memcpy(g_bufB, data, len);
        InterlockedExchange(&g_front, 1);
    }
    else {
        memcpy(g_bufA, g_bufB, DMX_SLOTS);
        memcpy(g_bufA, data, len);
        InterlockedExchange(&g_front, 0);
    }
}

__declspec(dllexport) void __cdecl stop_sender(void)
{
    if (!InterlockedCompareExchange(&g_running, 0, 0)) return;

    // Stop-Flag setzen
    InterlockedExchange(&g_running, 0);

    // WICHTIG: Socket zuerst schließen ? bricht sendto() sofort ab
    if (g_sock != INVALID_SOCKET) {
        shutdown(g_sock, SD_BOTH);   // optional
        closesocket(g_sock);
        g_sock = INVALID_SOCKET;
    }

    // Jetzt auf Thread warten (sollte sehr schnell gehen)
    if (g_thread) {
        WaitForSingleObject(g_thread, 2000);
        CloseHandle(g_thread);
        g_thread = NULL;
    }

    WSACleanup();
}