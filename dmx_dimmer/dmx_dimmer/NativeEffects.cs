using System.Runtime.InteropServices;

internal static class NativeEffects
{
    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void effects_init();

    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void effects_cancel(int channel1based);

    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void effects_cancel_all();

    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void effects_start_fade(int channel1based, byte current, byte target, int duration_ms);

    // Wichtig: [In, Out], damit Änderungen in der DLL zurückkommen
    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int effects_apply([In, Out] byte[] dmx, int len);

    [DllImport("effect_engine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int effects_is_active(int channel1based);
}
