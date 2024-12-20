
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Collections.Concurrent;
using OpenIris.ImageGrabbing;
using OpenIris;
using System.Threading;

namespace VarjoOpenIrisPlugin
{
    internal class ImageEyeVarjo : ImageEye
    {
        public ImageEyeVarjo(int width, int height, int stride, IntPtr data, ImageEyeTimestamp timeStamp)
            : base( width, height, stride, data, timeStamp)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (this.ImageSourceData != null)
            {
                VarjoDLL.FreeFrameData((IntPtr)this.ImageSourceData);
            }

            base.Dispose(disposing);
        }
    }

    internal class CameraEyeVarjo : CameraEye
    {
        public BlockingCollection<ImageEye> cameraBuffer = new BlockingCollection<ImageEye>(100);
        private CancellationTokenSource cancellation = new CancellationTokenSource();

        public CameraEyeVarjo()
        {
            FrameRate = 100;
        }

        public override void Start()
        {
        }

        public  override void Stop()
        {
            cameraBuffer.CompleteAdding();
            cancellation.Cancel();
        }

        protected override ImageEye GrabImageFromCamera()
        {
            try
            {
                var image =  cameraBuffer.Take(cancellation.Token);
                return image;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            cameraBuffer?.Dispose();
            cancellation?.Dispose();
        }
    }
}
