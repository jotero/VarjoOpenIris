using System.Runtime.InteropServices;
using static VarjoOpenIrisPlugin.Class1;

namespace VarjoOpenIrisPlugin
{
    public class NewFrameEventArgs : EventArgs
    {
       public long frameNumber;

        public NewFrameEventArgs(long frameN)
        {
            this.frameNumber = frameN;
        }
    }

    public class Class1
    {
        public delegate void NewFrameEventHandler(object sender, NewFrameEventArgs e);

        // Step 2: Declare the event
        public event NewFrameEventHandler NewFrame;

        // Step 3: Raise the event
        protected virtual void OnNewFrame(NewFrameEventArgs e)
        {
            NewFrame?.Invoke(this, e);
        }


        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "MyFunction2")]
        public static extern int MyFunction2();


        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "VarjoStartCameras")]
        public static extern int VarjoStartCameras(CallbackDelegate callback);

       
        // Define the struct to match the C++ FrameInfo
        [StructLayout(LayoutKind.Sequential)]
        public struct FrameInfo
        {
            public long FrameIndex; // Matches C++'s `long` (8 bytes on most platforms)
            public long Timestamp;  // Matches C++'s `long`
        }


        // Import the C++ GetLastFrame function
        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLastFrame(out IntPtr frameData, out int size, ref FrameInfo frameInfo);

        // Import the C++ FreeFrameData function
        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeFrameData(IntPtr frameData);




        // Callback function to pass to C++
        public bool MyCallbackFunction(IntPtr frameDataPtr, int size, FrameInfo frameInfo)
        {
           // IntPtr frameDataPtr = IntPtr.Zero;
           // int size = 0;
           // FrameInfo frameInfo = new FrameInfo();

            // Call the C++ function to get the last frame
            //GetLastFrame(out frameDataPtr, out size, ref frameInfo);

            if (frameDataPtr != IntPtr.Zero && size > 0)
            {
                // Marshal the frame data to a managed byte array
                byte[] frameData = new byte[size];
                Marshal.Copy(frameDataPtr, frameData, 0, size);

                // Print the frame metadata
                Console.WriteLine("Frame Index: " + frameInfo.FrameIndex);
                Console.WriteLine("Timestamp: " + frameInfo.Timestamp);

                Console.WriteLine("Frame Data: " + string.Join(", ", frameData));

                OnNewFrame(new NewFrameEventArgs(frameInfo.FrameIndex));

                // Free the memory allocated by C++
                FreeFrameData(frameDataPtr);
            }
            else
            {
                Console.WriteLine("Failed to retrieve frame data.");
            }

            Console.WriteLine("Callback received value: " + 0);
            return true;
        }
    }

    //struct Frame
    //{
    //    struct Metadata
    //    {
    //        varjo_StreamFrame streamFrame;        //!< Stream frame information
    //        long channelIndex;     //!< Channel index
    //        long timestamp;         //!< Frame timestamp
    //        double[] extrinsics = new double[16];              //!< Camera extrinsics (if available)
    //        //varjo_CameraIntrinsics intrinsics { };    //!< Camera frame intrinsics (if available)
    //        //varjo_BufferMetadata bufferMetadata { };  //!< Buffer metadata
    //    };
    //    Metadata metadata;        //!< Frame metadata
    //    std::vector<uint8_t> data;  //!< Buffer data
    //};

//    struct varjo_StreamFrame
//    {
//        long type;                     //!< Type of the stream.
//        long id;                         //!< Id of the stream.
//        long frameNumber;                       //!< Monotonically increasing frame number.
//        ulong channels;                //!< Channels that this frame contains.
//        ulong dataFlags;                  //!< Data that this frame contains.
//        double[] hmdPose = new double[16];               //!< HMD world pose. Invert the pose, if you need the HMD center view matrix.
//   // union varjo_StreamFrameMetadata metadata;  //!< Frame data. Use 'type' to determine which element to access.
//};
}