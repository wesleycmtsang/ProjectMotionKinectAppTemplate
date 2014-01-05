// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

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
using Coding4Fun.Kinect.Wpf;
using System.Media;
//using System.Windows.Media.MediaPlayer;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
//using System.Drawing.Drawing2D.Matrix;
//using System.Drawing.Image;
//using System.Drawing.Bitmap;
//using System.Drawing.Color;
//using System.Drawing.Imaging;
//using System.Drawing.Graphics;
//using System.Drawing.Drawing2D.GraphicsPath;
//using System.Drawing.RectangleF;


namespace SkeletalTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool closing = false;
        const int skeletonCount = 6;

        bool leftOverlap = false;
        bool rightOverlap = false;

        bool leftFootOverlap = false;
        bool rightFootOverlap = false;

        bool image1Touched = false;
        bool image2Touched = false;
        bool image3Touched = false;
        bool image4Touched = false;
        bool image5Touched = false;
        bool image6Touched = false;

        int leftOverlapWith = 0;
        int rightOverlapWith = 0;

        int leftFootOverlapWith = 0;
        int rightFootOverlapWith = 0;

        string[] vortexList = new string[360];

        Dictionary<System.Windows.Controls.Image, WMPLib.WindowsMediaPlayer> imageToSoundDict = new Dictionary<System.Windows.Controls.Image, WMPLib.WindowsMediaPlayer>();
        Dictionary<int, bool> touchedDict = new Dictionary<int, bool>();

        Dictionary<int, System.Windows.Controls.Image> imageDict = new Dictionary<int, System.Windows.Controls.Image>();
        Dictionary<int, WMPLib.WindowsMediaPlayer> soundDict = new Dictionary<int, WMPLib.WindowsMediaPlayer>();

        Skeleton[] allSkeletons = new Skeleton[skeletonCount];

        bool started = false;
        bool stopped = false;

        bool changeLock = false;

        int trackNum = 0;
        int interval = 0;

        string[] imageList = new string[9];

        List<string> trackList = new List<string>();
        List<List<Boolean>> holds = new List<List<bool>>();
        List<List<TimeSpan>> changeTimes = new List<List<TimeSpan>>();
        List<List<List<string>>> soundSets = new List<List<List<string>>>();
        List<List<List<int>>> Volumes = new List<List<List<int>>>();

        WMPLib.WindowsMediaPlayer baseTrack = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound1 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound2 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound3 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound4 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound5 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer sound6 = new WMPLib.WindowsMediaPlayer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);

            imageToSoundDict.Add(image1, sound1);
            imageToSoundDict.Add(image2, sound2);
            imageToSoundDict.Add(image3, sound3);
            imageToSoundDict.Add(image4, sound4);
            imageToSoundDict.Add(image5, sound5);
            imageToSoundDict.Add(image6, sound6);

            touchedDict.Add(1, image1Touched);
            touchedDict.Add(2, image2Touched);
            touchedDict.Add(3, image3Touched);
            touchedDict.Add(4, image4Touched);
            touchedDict.Add(5, image5Touched);
            touchedDict.Add(6, image6Touched);

            imageDict.Add(0, image7);
            imageDict.Add(1, image1);
            imageDict.Add(2, image2);
            imageDict.Add(3, image3);
            imageDict.Add(4, image4);
            imageDict.Add(5, image5);
            imageDict.Add(6, image6);

            soundDict.Add(1, sound1);
            soundDict.Add(2, sound2);
            soundDict.Add(3, sound3);
            soundDict.Add(4, sound4);
            soundDict.Add(5, sound5);
            soundDict.Add(6, sound6);

            for (int i = 0; i < 360; i++)
            {
                vortexList[i] = @"C:\Users\Wesley\Desktop\Spiral\" + (i + 1).ToString() + ".png";
            }
            
            baseTrack.settings.autoStart = false;
            sound1.settings.autoStart = false;
            sound2.settings.autoStart = false;
            sound3.settings.autoStart = false;
            sound4.settings.autoStart = false;
            sound5.settings.autoStart = false;
            sound6.settings.autoStart = false;

            baseTrack.settings.volume = 30;
            sound1.settings.volume = 80;
            sound2.settings.volume = 80;
            sound3.settings.volume = 80;
            sound4.settings.volume = 80;
            sound5.settings.volume = 80;
            sound6.settings.volume = 80;

            imageList[0] = @"C:\Users\Wesley\Desktop\lightsV2\light1.png";
            imageList[1] = @"C:\Users\Wesley\Desktop\lightsV2\light2.png";
            imageList[2] = @"C:\Users\Wesley\Desktop\lightsV2\light3.png";
            imageList[3] = @"C:\Users\Wesley\Desktop\lightsV2\light4.png";
            imageList[4] = @"C:\Users\Wesley\Desktop\lightsV2\light5.png";
            imageList[5] = @"C:\Users\Wesley\Desktop\lightsV2\light6.png";
            imageList[6] = @"C:\Users\Wesley\Desktop\lightsV2\light7.png";
            imageList[7] = @"C:\Users\Wesley\Desktop\lightsV2\light8.png";
            imageList[8] = @"C:\Users\Wesley\Desktop\lightsV2\light9.png";

            trackList.Add(@"C:\Users\Wesley\Desktop\Beats\Track1\Background\Background.mp3");
            trackList.Add(@"C:\Users\Wesley\Desktop\Beats\Track2\Background\Background.mp3");
            trackList.Add(@"C:\Users\Wesley\Desktop\Beats\Track3\Background\Background.mp3");
            trackList.Add(@"C:\Users\Wesley\Desktop\Beats\Track4\Background\Background.mp3");

            changeTimes.Add(new List<TimeSpan> { new TimeSpan(0, 0, 0, 27, 400                                                    ), 
                new TimeSpan(0, 0, 0, 27, 400), 
                new TimeSpan(0, 0, 0, 13, 684), 
                new TimeSpan(0, 0, 0, 27, 400), 
                new TimeSpan(0, 0, 0, 30, 0) });

            //track2 changeTimes
            changeTimes.Add(new List<TimeSpan> { new TimeSpan(0, 0, 0, 44, 0), 
                new TimeSpan(0, 0, 0, 28, 0), 
                new TimeSpan(0, 0, 0, 42, 0) });

            //track3 changeTimes
            changeTimes.Add(new List<TimeSpan> { new TimeSpan(0, 0, 0, 31, 970), 
                new TimeSpan(0, 0, 0, 31, 970), 
                new TimeSpan(0, 0, 0, 31, 970), 
                new TimeSpan(0, 0, 0, 31, 970), 
                new TimeSpan(0, 0, 0, 31, 970) });

            //track4 changeTimes
            changeTimes.Add(new List<TimeSpan> { new TimeSpan(0, 0, 0, 34, 0), 
                new TimeSpan(0, 0, 0, 27, 0), 
                new TimeSpan(0, 0, 0, 27, 300), 
                new TimeSpan(0, 0, 0, 27, 100), 
                new TimeSpan(0, 0, 0, 27, 0) });

            Volumes.Add(new List<List<int>> { });
            Volumes.Add(new List<List<int>> { });
            Volumes.Add(new List<List<int>> { });
            Volumes.Add(new List<List<int>> { });

            Volumes[0].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[0].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[0].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[0].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[0].Add(new List<int> { 80, 80, 80, 80, 80, 80 });

            Volumes[1].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[1].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[1].Add(new List<int> { 80, 80, 80, 80, 80, 80 });

            Volumes[2].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[2].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[2].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[2].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[2].Add(new List<int> { 80, 80, 80, 80, 80, 80 });

            Volumes[3].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[3].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[3].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[3].Add(new List<int> { 80, 80, 80, 80, 80, 80 });
            Volumes[3].Add(new List<int> { 80, 80, 80, 80, 80, 80 });

            //Repeat this for as many tracks as there are in the final product
            soundSets.Add(new List<List<string>> { });
            soundSets.Add(new List<List<string>> { });
            soundSets.Add(new List<List<string>> { });
            soundSets.Add(new List<List<string>> { });

            // Track1
            soundSets[0].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track1\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\6.mp3"});

            soundSets[0].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track1\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\6.mp3"});

            soundSets[0].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track1\Group3\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group3\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group3\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group3\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group3\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group3\6.mp3"});

            soundSets[0].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track1\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group2\6.mp3"});

            soundSets[0].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track1\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track1\Group1\6.mp3"});

            // Track2
            soundSets[1].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track2\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\6.mp3"});

            soundSets[1].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track2\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group2\6.mp3"});

            soundSets[1].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track2\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track2\Group1\6.mp3"});

            // Track3
            soundSets[2].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track3\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\6.mp3"});

            soundSets[2].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track3\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\6.mp3"});

            soundSets[2].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track3\Group3\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group3\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group3\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group3\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group3\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group3\6.mp3"});

            soundSets[2].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track3\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group2\6.mp3"});

            soundSets[2].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track3\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track3\Group1\6.mp3"});

            // Track4
            soundSets[3].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track4\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\6.mp3"});

            soundSets[3].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track4\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\6.mp3"});

            soundSets[3].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track4\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\6.mp3"});

            soundSets[3].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track4\Group2\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group2\6.mp3"});

            soundSets[3].Add(new List<string> {@"C:\Users\Wesley\Desktop\Beats\Track4\Group1\1.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\2.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\3.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\4.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\5.mp3",
                @"C:\Users\Wesley\Desktop\Beats\Track4\Group1\6.mp3"});

            holds.Add(new List<bool> { false, true, false, true, false });
            holds.Add(new List<bool> { false, true, false });
            holds.Add(new List<bool> { true, true, true, true, true });
            holds.Add(new List<bool> { false, true, false, true, false });

            Thread t1 = new Thread(PlayMusic);
            t1.Start();

            Thread t2 = new Thread(() => VortexSpiral(1));
            t2.Start();

            Thread t3 = new Thread(() => HoverCounterClockwise(image1, 1, 1, 240, 410, 650, 820, image1Touched));
            t3.Start();

            Thread t4 = new Thread(() => HoverClockwise(image2, 1, 1, 250, 420, 450, 620, image2Touched));
            t4.Start();

            Thread t5 = new Thread(() => HoverCounterClockwise(image3, 1, 1, 330, 500, 175, 345, image3Touched));
            t5.Start();

            Thread t6 = new Thread(() => HoverClockwise(image4, 1, 1, 940, 1110, 175, 345, image4Touched));
            t6.Start();

            Thread t7 = new Thread(() => HoverCounterClockwise(image5, 1, 1, 1030, 1200, 450, 620, image5Touched));
            t7.Start();

            Thread t8 = new Thread(() => HoverClockwise(image6, 1, 1, 1070, 1240, 650, 820, image6Touched));
            t8.Start();
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor old = (KinectSensor)e.OldValue;

            StopKinect(old);

            KinectSensor sensor = (KinectSensor)e.NewValue;

            if (sensor == null)
            {
                return;
            }

            // Parameters for smooth response
            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            // sensor.SkeletonStream.Enable(parameters);

            sensor.SkeletonStream.Enable();

            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30); 
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            try
            {
                sensor.Start();
            }
            catch (System.IO.IOException)
            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (closing)
            {
                return;
            }

            //Get a skeleton
            Skeleton first =  GetFirstSkeleton(e);

            if (first == null)
            {
                return; 
            }

            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            {
                if (depthImageFrame != null)
                {
                    System.Windows.Point HeadPoint = GetPosition2DLocation(depthImageFrame, first.Joints[JointType.Head].Position);
                    System.Windows.Point LeftHandPoint = GetPosition2DLocation(depthImageFrame, first.Joints[JointType.HandLeft].Position);
                    System.Windows.Point RightHandPoint = GetPosition2DLocation(depthImageFrame, first.Joints[JointType.HandRight].Position);
                    System.Windows.Point LeftFootPoint = GetPosition2DLocation(depthImageFrame, first.Joints[JointType.FootLeft].Position);
                    System.Windows.Point RightFootPoint = GetPosition2DLocation(depthImageFrame, first.Joints[JointType.FootRight].Position);

                    Canvas.SetLeft(headImage, HeadPoint.X - headImage.Width / 2);
                    Canvas.SetTop(headImage, HeadPoint.Y - headImage.Height / 2);

                    Canvas.SetLeft(leftEllipse, LeftHandPoint.X - leftEllipse.Width / 2 + 75);
                    Canvas.SetTop(leftEllipse, LeftHandPoint.Y - leftEllipse.Height / 2);

                    Canvas.SetLeft(rightEllipse, RightHandPoint.X - rightEllipse.Width / 2 - 75);
                    Canvas.SetTop(rightEllipse, RightHandPoint.Y - rightEllipse.Height / 2);

                    Canvas.SetLeft(leftFootEllipse, LeftFootPoint.X - leftFootEllipse.Width / 2 + 25);
                    Canvas.SetTop(leftFootEllipse, LeftFootPoint.Y - leftFootEllipse.Height / 2);

                    Canvas.SetLeft(rightFootEllipse, RightFootPoint.X - rightFootEllipse.Width / 2 - 25);
                    Canvas.SetTop(rightFootEllipse, RightFootPoint.Y - rightFootEllipse.Height / 2);

                    double leftX = Canvas.GetLeft(leftEllipse);
                    double leftY = Canvas.GetTop(leftEllipse);
                    if (leftY > 620)
                    {
                        Canvas.SetLeft(leftEllipse, leftX - 50);
                        Canvas.SetTop(leftEllipse, leftY + 50);
                    }

                    double rightX = Canvas.GetLeft(rightEllipse);
                    double rightY = Canvas.GetTop(rightEllipse);
                    if (rightX < 950 && rightY > 550)
                    {
                        Canvas.SetLeft(rightEllipse, rightX + 75);
                        Canvas.SetTop(rightEllipse, rightY + 75);
                    }
                }
            }

            // GetCameraPoint(first, e);

            //set scaled position
            //ScalePosition(headImage, first.Joints[JointType.Head]);
            //ScalePosition(leftEllipse, first.Joints[JointType.HandLeft]);
            //ScalePosition(rightEllipse, first.Joints[JointType.HandRight]);

            if (changeLock == false)
            {
                if (started == false)
                {
                    DetectStart();
                }
                else if (started == true && stopped == false)
                {
                    if (!leftOverlap)
                    {
                        leftOverlapWith = CheckNewOverlap(leftEllipse);
                        if (leftOverlapWith != 0)
                        { 
                            leftOverlap = !leftOverlap;
                            //if (leftOverlapWith != rightOverlapWith)
                            //{
                            //    rectDict[leftOverlapWith].Fill = new SolidColorBrush(Colors.Red);
                            //}
                        }
                    }
                    else
                    {
                        if (!DetectOverlap(leftEllipse, imageDict[leftOverlapWith]))
                        {
                            // if hold is true, then stop the corresponding mediaplayer from playing.
                            if ((interval < holds[trackNum].Count) && (holds[trackNum][interval]))
                            {
                                soundDict[leftOverlapWith].controls.stop();
                            }
                            touchedDict[leftOverlapWith] = false;
                            leftOverlap = !leftOverlap;
                            //if (leftOverlapWith != rightOverlapWith)
                            //{
                            //    rectDict[leftOverlapWith].Fill = new SolidColorBrush(Colors.Lime);
                            //}
                            leftOverlapWith = 0;
                        }
                    }

                    if (!rightOverlap)
                    {
                        rightOverlapWith = CheckNewOverlap(rightEllipse);
                        if (rightOverlapWith != 0)
                        {
                            rightOverlap = !rightOverlap;
                            //if (rightOverlapWith != leftOverlapWith)
                            //{
                            //    rectDict[rightOverlapWith].Fill = new SolidColorBrush(Colors.Red);
                            //}
                        }
                    }
                    else
                    {
                        if (!DetectOverlap(rightEllipse, imageDict[rightOverlapWith]))
                        {
                            // if hold is true, then stop the corresponding mediaplayer from playing. 
                            if ((interval < holds[trackNum].Count) && (holds[trackNum][interval]))
                            {
                                soundDict[rightOverlapWith].controls.stop();
                            }
                            touchedDict[rightOverlapWith] = false;
                            rightOverlap = !rightOverlap;
                            //if (rightOverlapWith != leftOverlapWith)
                            //{
                            //    rectDict[rightOverlapWith].Fill = new SolidColorBrush(Colors.Lime);
                            //}
                            rightOverlapWith = 0;
                        }
                    }

                    //if (!leftFootOverlap)
                    //{
                    //    leftFootOverlapWith = CheckNewOverlap(leftFootEllipse);
                    //    if (leftFootOverlapWith != 0)
                    //    {
                    //        leftFootOverlap = !leftFootOverlap;
                    //        //if (leftFootOverlapWith != rightFootOverlapWith)
                    //        //{
                    //        //    rectDict[leftFootOverlapWith].Fill = new SolidColorBrush(Colors.Red);
                    //        //}
                    //    }
                    //}
                    //else
                    //{
                    //    if (!DetectOverlap(leftFootEllipse, imageDict[leftFootOverlapWith]))
                    //    {
                    //        // if hold is true, then stop the corresponding mediaplayer from playing.
                    //        if ((interval < holds[trackNum].Count) && (holds[trackNum][interval]))
                    //        {
                    //            soundDict[leftFootOverlapWith].controls.stop();
                    //        }
                    //        touchedDict[leftFootOverlapWith] = false;
                    //        leftFootOverlap = !leftFootOverlap;
                    //        //if (leftFootOverlapWith != rightFootOverlapWith)
                    //        //{
                    //        //    rectDict[leftFootOverlapWith].Fill = new SolidColorBrush(Colors.Lime);
                    //        //}
                    //        leftFootOverlapWith = 0;
                    //    }
                    //}

                    //if (!rightFootOverlap)
                    //{
                    //    rightFootOverlapWith = CheckNewOverlap(rightFootEllipse);
                    //    if (rightFootOverlapWith != 0)
                    //    {
                    //        rightFootOverlap = !rightFootOverlap;
                    //        //if (rightFootOverlapWith != leftFootOverlapWith)
                    //        //{
                    //        //    rectDict[rightFootOverlapWith].Fill = new SolidColorBrush(Colors.Red);
                    //        //}
                    //    }
                    //}
                    //else
                    //{
                    //    if (!DetectOverlap(rightFootEllipse, imageDict[rightFootOverlapWith]))
                    //    {
                    //        // if hold is true, then stop the corresponding mediaplayer from playing. 
                    //        if ((interval < holds[trackNum].Count) && (holds[trackNum][interval]))
                    //        {
                    //            soundDict[rightFootOverlapWith].controls.stop();
                    //        }

                    //        touchedDict[rightFootOverlapWith] = false;
                    //        rightFootOverlap = !rightFootOverlap;
                    //        //if (rightFootOverlapWith != leftFootOverlapWith)
                    //        //{
                    //        //    rectDict[rightFootOverlapWith].Fill = new SolidColorBrush(Colors.Lime);
                    //        //}
                    //        rightFootOverlapWith = 0;
                    //    }
                    //}
                }
                else if (started == true && stopped == true)
                {
                    //trackNum = 0;

                    //if (trackNum < 3)
                    //{
                    //    trackNum++;
                    //}
                    //else
                    //{
                    //    trackNum = 0;
                    //}

                    started = false;
                    stopped = false;
                }
            }
        }

        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null; 
                }

                
                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //get the first tracked skeleton
                Skeleton first = (from s in allSkeletons
                                         where s.TrackingState == SkeletonTrackingState.Tracked
                                         select s).FirstOrDefault();

                return first;

            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();

                    //stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }


                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true; 
            StopKinect(kinectSensorChooser1.Kinect); 
        }

        int CheckNewOverlap(Ellipse element)
        {
            if (DetectOverlap(element, image1))
            {
                image1Touched = true;
                //if (sound1.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound1.controls.stop();
                sound1.controls.play();
                //}
                return 1;
            }
            else if (DetectOverlap(element, image2))
            {
                image2Touched = true;
                //if (sound2.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound2.controls.stop();
                sound2.controls.play();
                //}
                return 2;
            }
            else if (DetectOverlap(element, image3))
            {
                image3Touched = true;
                //if (sound3.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound3.controls.stop();
                sound3.controls.play();
                //}
                return 3;
            }
            else if (DetectOverlap(element, image4))
            {
                image4Touched = true;
                //if (sound4.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound4.controls.stop();
                sound4.controls.play();
                //}
                return 4;
            }
            else if (DetectOverlap(element, image5))
            {
                image5Touched = true;
                //if (sound5.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound5.controls.stop();
                sound5.controls.play();
                //}
                return 5;
            }
            else if (DetectOverlap(element, image6))
            {
                image6Touched = true;
                //if (sound6.playState == WMPLib.WMPPlayState.wmppsReady)
                //{
                //sound6.controls.stop();
                sound6.controls.play();
                //}
                return 6;
            }
            return 0;
        }

        private bool DetectOverlap(Ellipse elli, System.Windows.Controls.Image image)
        {
            double elliX1 = Canvas.GetLeft(elli);
            double elliX2 = elliX1 + 75;
            double elliY1 = Canvas.GetTop(elli);
            double elliY2 = elliY1 + 75;

            double imageX1 = Canvas.GetLeft(image);
            double imageX2 = imageX1 + 98;
            double imageY1 = Canvas.GetTop(image);
            double imageY2 = imageY1 + 99;

            return !(elliX1 > imageX2 || imageX1 > elliX2 || elliY1 > imageY2 || imageY1 > elliY2);
        }

        private void DetectStart()
        {
            double leftX1 = Canvas.GetLeft(leftEllipse);
            double leftX2 = leftX1 + 75;
            double leftY1 = Canvas.GetTop(leftEllipse);
            double leftY2 = leftY1 + 75;

            double rightX1 = Canvas.GetLeft(rightEllipse);
            double rightX2 = rightX1 + 75;
            double rightY1 = Canvas.GetTop(rightEllipse);
            double rightY2 = rightY1 + 75;

            double startX1 = Canvas.GetLeft(rectangle7);
            double startX2 = startX1 + 200;
            double startY1 = Canvas.GetTop(rectangle7);
            double startY2 = startY1 + 100;

            if (!(leftX1 > startX2 || startX1 > leftX2 || leftY1 > startY2 || startY1 > leftY2) 
                && !(rightX1 > startX2 || startX1 > rightX2 || rightY1 > startY2 || startY1 > rightY2))
            {
                started = true;
            }
        }

        void PlayMusic()
        {
            Action leftHidden = delegate
            {
                imageHandLeft.Visibility = System.Windows.Visibility.Hidden;
            };

            Action rightHidden = delegate
            {
                imageHandRight.Visibility = System.Windows.Visibility.Hidden;
            };

            Action leftVisible = delegate
            {
                imageHandLeft.Visibility = System.Windows.Visibility.Visible;
            };

            Action rightVisible = delegate
            {
                imageHandRight.Visibility = System.Windows.Visibility.Visible;
            };

            while (true)
            {
                if (started == true && stopped == false)
                {
                    imageHandLeft.Dispatcher.Invoke(leftHidden);
                    imageHandRight.Dispatcher.Invoke(rightHidden);

                    changeLock = true;
                    baseTrack.URL = trackList[trackNum];
                    sound1.URL = soundSets[trackNum][interval][0];
                    sound2.URL = soundSets[trackNum][interval][1];
                    sound3.URL = soundSets[trackNum][interval][2];
                    sound4.URL = soundSets[trackNum][interval][3];
                    sound5.URL = soundSets[trackNum][interval][4];
                    sound6.URL = soundSets[trackNum][interval][5];

                    sound1.settings.volume = Volumes[trackNum][interval][0];
                    sound2.settings.volume = Volumes[trackNum][interval][1];
                    sound3.settings.volume = Volumes[trackNum][interval][2];
                    sound4.settings.volume = Volumes[trackNum][interval][3];
                    sound5.settings.volume = Volumes[trackNum][interval][4];
                    sound6.settings.volume = Volumes[trackNum][interval][5];
                    changeLock = false;
                    baseTrack.controls.play();
                    Thread.Sleep(changeTimes[trackNum][interval]);
                    interval++;

                    while (interval < changeTimes[trackNum].Count)
                    {
                        changeLock = true;
                        sound1.URL = soundSets[trackNum][interval][0];
                        sound2.URL = soundSets[trackNum][interval][1];
                        sound3.URL = soundSets[trackNum][interval][2];
                        sound4.URL = soundSets[trackNum][interval][3];
                        sound5.URL = soundSets[trackNum][interval][4];
                        sound6.URL = soundSets[trackNum][interval][5];

                        sound1.settings.volume = Volumes[trackNum][interval][0];
                        sound2.settings.volume = Volumes[trackNum][interval][1];
                        sound3.settings.volume = Volumes[trackNum][interval][2];
                        sound4.settings.volume = Volumes[trackNum][interval][3];
                        sound5.settings.volume = Volumes[trackNum][interval][4];
                        sound6.settings.volume = Volumes[trackNum][interval][5];
                        changeLock = false;
                        Thread.Sleep(changeTimes[trackNum][interval]);
                        interval++;
                    }

                    //while (baseTrack.playState != WMPLib.WMPPlayState.wmppsStopped)
                    //{
                    //}
                    stopped = true;

                    if (trackNum < 3)
                    {
                        trackNum++;
                    }
                    else
                    {
                        trackNum = 0;
                    }
                    interval = 0;

                    imageHandLeft.Dispatcher.Invoke(leftVisible);
                    imageHandRight.Dispatcher.Invoke(rightVisible);
                }
            }
        }

        void HoverClockwise(System.Windows.Controls.Image image, int xSpeed, int ySpeed, int xLeft, int xRight, int yTop, int yBottom, bool imageTouched)
        {
            double currentLeft = 100;
            double currentTop = 100;
            int xDirection = 1;
            int yDirection = 1;

            Action get = delegate
            {
                currentLeft = Canvas.GetLeft(image);
                currentTop = Canvas.GetTop(image);
            };

            Action set = delegate
            {
                Canvas.SetLeft(image, currentLeft + (xDirection * xSpeed));
                Canvas.SetTop(image, currentTop + (yDirection * ySpeed));
            };

            Action original = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[0], UriKind.Absolute));
            };

            Action animate1 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[1], UriKind.Absolute));
            };

            Action animate2 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[2], UriKind.Absolute));
            };

            Action animate3 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[3], UriKind.Absolute));
            };

            Action animate4 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[4], UriKind.Absolute));
            };

            Action animate5 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[5], UriKind.Absolute));
            };

            Action animate6 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[6], UriKind.Absolute));
            };

            Action animate7 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[7], UriKind.Absolute));
            };

            Action animate8 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[8], UriKind.Absolute));
            };

            while (true) {
                if (!imageTouched)
                {
                    image.Dispatcher.Invoke(get);

                    if (xDirection == 1 && currentLeft + 100 >= xRight)
                    {
                        xDirection = -1;
                    }
                    else if (xDirection == -1 && currentLeft <= xLeft)
                    {
                        xDirection = 1;
                    }

                    if (yDirection == 1 && currentTop + 100 >= yBottom)
                    {
                        yDirection = -1;
                    }
                    else if (yDirection == -1 && currentTop <= yTop)
                    {
                        yDirection = 1;
                    }

                    image.Dispatcher.Invoke(set);

                    Thread.Sleep(80);
                }
                else
                {
                    if (!holds[trackNum][interval])
                    {
                        image.Dispatcher.Invoke(animate1);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate2);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate3);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate4);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate5);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate6);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate7);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate8);
                        Thread.Sleep(100);
                    }
                    while (imageToSoundDict[image].playState == WMPLib.WMPPlayState.wmppsPlaying) { }
                    image.Dispatcher.Invoke(original);
                }

                //if (imageTouched)
                //{
                //    this.Dispatcher.Invoke(animate1);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate2);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate3);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate4);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate5);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate6);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate7);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate8);
                //    Thread.Sleep(100);

                //    while (imageToSoundDict[image].playState == WMPLib.WMPPlayState.wmppsPlaying) { }
                //    this.Dispatcher.Invoke(original);
                //}
            }
        }

        void HoverCounterClockwise(System.Windows.Controls.Image image, int xSpeed, int ySpeed, int xLeft, int xRight, int yTop, int yBottom, bool imageTouched)
        {
            double currentLeft = 100;
            double currentTop = 100;
            int xDirection = 1;
            int yDirection = -1;

            Action get = delegate
            {
                currentLeft = Canvas.GetLeft(image);
                currentTop = Canvas.GetTop(image);
            };

            Action set = delegate
            {
                Canvas.SetLeft(image, currentLeft + (xDirection * xSpeed));
                Canvas.SetTop(image, currentTop + (yDirection * ySpeed));
            };

            Action original = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[0], UriKind.Absolute));
            };

            Action animate1 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[1], UriKind.Absolute));
            };

            Action animate2 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[2], UriKind.Absolute));
            };

            Action animate3 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[3], UriKind.Absolute));
            };

            Action animate4 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[4], UriKind.Absolute));
            };

            Action animate5 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[5], UriKind.Absolute));
            };

            Action animate6 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[6], UriKind.Absolute));
            };

            Action animate7 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[7], UriKind.Absolute));
            };

            Action animate8 = delegate
            {
                image.Source = new BitmapImage(new Uri(imageList[8], UriKind.Absolute));
            };

            while (true)
            {
                if (!imageTouched)
                {
                    image.Dispatcher.Invoke(get);

                    if (xDirection == 1 && currentLeft + 100 >= xRight)
                    {
                        xDirection = -1;
                    }
                    else if (xDirection == -1 && currentLeft <= xLeft)
                    {
                        xDirection = 1;
                    }

                    if (yDirection == 1 && currentTop + 100 >= yBottom)
                    {
                        yDirection = -1;
                    }
                    else if (yDirection == -1 && currentTop <= yTop)
                    {
                        yDirection = 1;
                    }

                    image.Dispatcher.Invoke(set);

                    Thread.Sleep(80);
                }
                else
                {
                    if (!holds[trackNum][interval])
                    {
                        image.Dispatcher.Invoke(animate1);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate2);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate3);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate4);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate5);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate6);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate7);
                        Thread.Sleep(100);
                        image.Dispatcher.Invoke(animate8);
                        Thread.Sleep(100);
                    }
                    while (imageToSoundDict[image].playState == WMPLib.WMPPlayState.wmppsPlaying) { }
                    image.Dispatcher.Invoke(original);
                }

                //if (imageTouched)
                //{
                //    this.Dispatcher.Invoke(animate1);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate2);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate3);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate4);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate5);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate6);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate7);
                //    Thread.Sleep(100);
                //    this.Dispatcher.Invoke(animate8);
                //    Thread.Sleep(100);

                //    while (imageToSoundDict[image].playState == WMPLib.WMPPlayState.wmppsPlaying) { }
                //    this.Dispatcher.Invoke(original);
                //}
            }
        }

        //void OrbAnimation(bool imageTouched, System.Windows.Controls.Image image, int count)
        //{
        //    Action action = delegate { image.Source = new BitmapImage(new Uri(imageList[count], UriKind.Absolute)); };

        //    while (true)
        //    {
        //        if (imageTouched)
        //        {
        //            if (!holds[trackNum][interval])
        //            {
        //                count++;
        //                while (count < 9)
        //                {
        //                    image.Dispatcher.Invoke(action);
        //                    Thread.Sleep(80);
        //                    count++;
        //                }
        //                while (imageToSoundDict[image].playState == WMPLib.WMPPlayState.wmppsPlaying) {}
        //                count = 0;
        //                image.Dispatcher.Invoke(action);
        //            }
        //        }
        //    }
        //}

        void VortexSpiral(int count)
        {
            Action rotate = delegate
            {
                vortex.Source = new BitmapImage(new Uri(vortexList[count], UriKind.Absolute));
            };

            while (true)
            {
                if (count == 360)
                {
                    count = 0;
                }
                
                vortex.Dispatcher.Invoke(rotate);
                count++;
                Thread.Sleep(50);
            }
        }

        private System.Windows.Point GetPosition2DLocation(DepthImageFrame depthFrame, SkeletonPoint skeletonPoint)
        {
            DepthImagePoint depthPoint = depthFrame.MapFromSkeletonPoint(skeletonPoint);

            return new System.Windows.Point(
                (int)(1600 * depthPoint.X / depthFrame.Width), 
                (int)(900 * depthPoint.Y / depthFrame.Height));
        }
    }
}
