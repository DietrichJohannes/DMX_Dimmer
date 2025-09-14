using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dmx_dimmer
{
    internal static class Native
    {
        [DllImport("dmx_sender.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int start_sender([MarshalAs(UnmanagedType.LPStr)] string ip, int universe, int fps);

        [DllImport("dmx_sender.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void update_dmx(byte[] data, int len);

        [DllImport("dmx_sender.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void stop_sender();
    }
}
