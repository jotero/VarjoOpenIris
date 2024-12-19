
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
    internal class CameraEyeVarjo : CameraEye
    {
        public BlockingCollection<ImageEye> cameraBuffer = new BlockingCollection<ImageEye>(100);
        private CancellationTokenSource cancellation = new CancellationTokenSource();

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
                return cameraBuffer.Take(cancellation.Token);
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
