using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.WpfViewers
{
    /// <summary>
    /// Interaction logic for SilhouetteViewer.xaml
    /// </summary>
    public partial class SilhouetteViewer : ImageViewer
    {
        private byte[] silhouetteData;
        private DepthImageFormat lastImageFormat;
        private short[] pixelData;
        private WriteableBitmap outputBitmap;

        private static readonly int Bgr32BytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        public SilhouetteViewer()
        {
            InitializeComponent();
        }

        protected override void OnKinectChanged(KinectSensor oldKinectSensor, KinectSensor newKinectSensor)
        {
            if (oldKinectSensor != null)
            {
                oldKinectSensor.DepthFrameReady -= this.DepthImageReady;
                kinectDepthImage.Source = null;
                this.lastImageFormat = DepthImageFormat.Undefined;
            }

            if (newKinectSensor != null && newKinectSensor.Status == KinectStatus.Connected)
            {
                ResetFrameRateCounters();

                newKinectSensor.DepthFrameReady += this.DepthImageReady;
            }
        }

        private void DepthImageReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame imageFrame = e.OpenDepthImageFrame())
            {
                if (imageFrame != null)
                {
                    // We need to detect if the format has changed.
                    bool haveNewFormat = this.lastImageFormat != imageFrame.Format;

                    if (haveNewFormat)
                    {
                        // short array to copy to frame.
                        this.pixelData = new short[imageFrame.PixelDataLength];
                        // this.depthFrame32 = new byte[imageFrame.Width * imageFrame.Height * Bgr32BytesPerPixel];
                    }

                    imageFrame.CopyPixelDataTo(this.pixelData);

                    silhouetteData = new byte[4*pixelData.Length];

                    for (int i = 0; i < pixelData.Length; i++)
                    {
                        if ((pixelData[i] & 0x07) != 0)
                        {
                            silhouetteData[4 * i + 3] = 200;
                        }
                    }

                    // byte[] convertedDepthBits = this.ConvertDepthFrame(this.pixelData, ((KinectSensor)sender).DepthStream);

                    if (haveNewFormat)
                    {
                        this.outputBitmap = new WriteableBitmap(
                            imageFrame.Width,
                            imageFrame.Height,
                            96,  // DpiX
                            96,  // DpiY
                            PixelFormats.Bgra32,
                            null);

                        this.kinectDepthImage.Source = this.outputBitmap;
                    }

                    this.outputBitmap.WritePixels(
                        new Int32Rect(0, 0, imageFrame.Width, imageFrame.Height),
                        silhouetteData,
                        imageFrame.Width * Bgr32BytesPerPixel,
                        0);

                    this.lastImageFormat = imageFrame.Format;

                    UpdateFrameRate();
                }
            }
        }
    }
}
