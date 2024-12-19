using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using OpenIris;
using OpenIris.ImageGrabbing;
using System.Runtime.InteropServices;

#nullable enable

namespace VarjoOpenIrisPlugin
{
    [Export(typeof(EyeTrackingSystemBase)), PluginDescriptionEyeTrackingSystem("Varjo", typeof(EyeTrackingSystemSettings))]
    public class EyeTrackingSystemVarjo : EyeTrackingSystemBase
    {

        private EyeCollection<CameraEye>? cameras;
        private ImageEyeTimestamp firstTimeStamp = ImageEyeTimestamp.Empty;
        private long numberFramesGrabbed;

        public EyeCollection<CameraEye?>? StartVarjo()
        {
            return this.CreateAndStartCameras();
        }
        protected override EyeCollection<CameraEye?>? CreateAndStartCameras()
        {
            cameras = new EyeCollection<CameraEye> { new CameraEyeVarjo(), new CameraEyeVarjo() };
            // Create a delegate instance
            CallbackDelegate callback = new CallbackDelegate(this.VarjoCallbackFunction);

            // Register the callback with the C++ DLL
            Console.WriteLine("Callback registered.");

            Task.Run(()=>VarjoDLL.VarjoStartCameras(callback));
             

            return cameras;
        }

        public override IHeadDataSource? CreateHeadDataSourceWithCameras()
        {
            return null;
        }

        public override IHeadDataSource? CreateHeadDataSourceWithVideos()
        {
            return null;
        }


        public override EyeCollection<ImageEye?> PreProcessImages(EyeCollection<ImageEye?> images)
        {
            return images;
        }

        public override EyeTrackerImagesAndData PostProcessImagesAndData(EyeTrackerImagesAndData procesedImages)
        {
            return procesedImages;
        }



        // Callback function to pass to C++
        public bool VarjoCallbackFunction(IntPtr frameDataPtr, int size, FrameInfo frameInfo)
        {
            // IntPtr frameDataPtr = IntPtr.Zero;
            // int size = 0;
            // FrameInfo frameInfo = new FrameInfo();

            // Call the C++ function to get the last frame
            //GetLastFrame(out frameDataPtr, out size, ref frameInfo);

            if (frameDataPtr != IntPtr.Zero && size > 0)
            {
                // Marshal the frame data to a managed byte array
                //byte[] frameData = new byte[size];
                //Marshal.Copy(frameDataPtr, frameData, 0, size);

                // Print the frame metadata
                //Console.WriteLine("Frame Index: " + frameInfo.FrameIndex);
                //Console.WriteLine("Timestamp: " + frameInfo.Timestamp);

                //Console.WriteLine("Frame Data: " + string.Join(", ", frameData));

                numberFramesGrabbed++;

                var timestamp = new ImageEyeTimestamp
                (
                    seconds: (double)frameInfo.Timestamp / 1000000.0,
                    frameNumber: (ulong)frameInfo.FrameIndex - firstTimeStamp.FrameNumberRaw,
                    frameNumberRaw: (ulong)frameInfo.FrameIndex
                );
                // If it is the first frame save some info
                if (numberFramesGrabbed == 1)
                {
                    timestamp.FrameNumber = 0;
                    firstTimeStamp = timestamp;
                }

                // format 15, type 1, byteSize 307200, rowstride 640, width 640, height 480

                int cols = 640;
                int rows = 480;
                int stride = 640;
                var WhichEye = (frameInfo.ChannelIndex==1) ? Eye.Left : Eye.Right;
                var newImage = new ImageEye(cols, rows, stride, frameDataPtr, timestamp)
                {
                    WhichEye = WhichEye,
                    ImageSourceData = frameDataPtr
                };

                (cameras?[WhichEye] as CameraEyeVarjo)?.cameraBuffer.Add(newImage);


                // Free the memory allocated by C++
                //VarjoDLL.FreeFrameData(frameDataPtr);
            }
            else
            {
                Console.WriteLine("Failed to retrieve frame data.");
            }

            Console.WriteLine("Callback received value: " + 0);
            return true;
        }
    }
}
