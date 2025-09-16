// dmx_effects.c  —  compile: cl /LD dmx_effects.c
#include <stdint.h>
#include <string.h>
#include <windows.h>

#define DMX_SLOTS 512

typedef struct {
    uint8_t  active;        // 0 = aus, 1 = Fade läuft
    uint8_t  start;         // Startwert
    uint8_t  end;           // Zielwert
    uint64_t t0_ms;         // Startzeit (ms)
    uint32_t dur_ms;        // Dauer
} FadeState;

static FadeState g_fades[DMX_SLOTS];

static uint64_t now_ms(void) {
    return (uint64_t)GetTickCount64();
}

static uint8_t clamp_u8(int v) {
    if (v < 0)   return 0;
    if (v > 255) return 255;
    return (uint8_t)v;
}

__declspec(dllexport) void __cdecl effects_init(void) {
    memset(g_fades, 0, sizeof(g_fades));
}

__declspec(dllexport) void __cdecl effects_cancel(int channel1based) {
    if (channel1based < 1 || channel1based > DMX_SLOTS) return;
    g_fades[channel1based - 1].active = 0;
}

__declspec(dllexport) void __cdecl effects_cancel_all(void) {
    for (int i = 0; i < DMX_SLOTS; ++i) g_fades[i].active = 0;
}

// Startet einen linearen Fade auf "channel1based" von "current" nach "target" in "duration_ms".
__declspec(dllexport) void __cdecl effects_start_fade(
    int channel1based, uint8_t current, uint8_t target, int duration_ms)
{
    if (channel1based < 1 || channel1based > DMX_SLOTS) return;
    if (duration_ms < 0) duration_ms = 0;

    FadeState* st = &g_fades[channel1based - 1];
    st->start = current;
    st->end = target;
    st->t0_ms = now_ms();
    st->dur_ms = (uint32_t)(duration_ms == 0 ? 1 : duration_ms);
    st->active = 1;
}

// Wendet alle aktiven Fades auf das DMX-Array an.
// dmx: [In,Out] 0..(len-1). Gibt Anzahl geänderter Kanäle zurück.
__declspec(dllexport) int __cdecl effects_apply(uint8_t* dmx, int len) {
    if (!dmx || len <= 0) return 0;
    if (len > DMX_SLOTS) len = DMX_SLOTS;

    uint64_t t = now_ms();
    int changed = 0;

    for (int i = 0; i < len; ++i) {
        FadeState* st = &g_fades[i];
        if (!st->active) continue;

        uint64_t elapsed = (t > st->t0_ms) ? (t - st->t0_ms) : 0;
        if (elapsed >= st->dur_ms) {
            dmx[i] = st->end;
            st->active = 0;
            ++changed;
            continue;
        }

        // Linearer Fortschritt 0..1
        double p = (double)elapsed / (double)st->dur_ms;
        int val = (int)((1.0 - p) * st->start + p * st->end + 0.5); // rundung
        uint8_t v = clamp_u8(val);

        if (dmx[i] != v) {
            dmx[i] = v;
            ++changed;
        }
    }
    return changed;
}

// Optional: Abfragen, ob auf Kanal noch ein Fade läuft
__declspec(dllexport) int __cdecl effects_is_active(int channel1based) {
    if (channel1based < 1 || channel1based > DMX_SLOTS) return 0;
    return g_fades[channel1based - 1].active ? 1 : 0;
}
