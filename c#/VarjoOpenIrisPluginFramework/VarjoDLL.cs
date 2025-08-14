using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VarjoOpenIrisPlugin
{

    // Define the struct to match the C++ FrameInfo
    [StructLayout(LayoutKind.Sequential)]
    public struct FrameInfo
    {
        public long FrameIndex; // Matches C++'s `long` (8 bytes on most platforms)
        public long Timestamp;  // Matches C++'s `long`
        public long ChannelIndex;
    }

    // Define a delegate that matches the C++ callback signature
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool CallbackDelegate(IntPtr frameData, int size, FrameInfo frameInfo);

    public class VarjoDLL
    {

        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "VarjoStartCameras")]
        public static extern int VarjoStartCameras(CallbackDelegate callback);

        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "VarjoStop")]
        public static extern int VarjoStop();


        // Import the C++ FreeFrameData function
        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeFrameData(IntPtr frameData);



    }
}
