   M S B A M L   `   `   `  ���� ^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers   SkeletalTracking!   SkeletalTracking.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  
MainWindow�� � WindowState$ 	Maximized=� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ CanResizeWithGrip=�$��Stretch=�$��Stretch=�.���� -   $��
MainCanvas��$	��900��$
��1600��.+��� -   $��
background�� �� Left$ 0��
 �� Top$ 0��$	��900��$
��Fill=�$/��)/SkeletalTracking;component/Images/bg.png��$
��1600���� -   $��vortex��$ 0��$ 0��$	��900��$
��Fill=�$
��1600��$5��//SkeletalTracking;component/Images/nbb20001.png��$y�Visible=�;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   $��kinectColorViewer1��$ 0��$ 0��$	��640��$	��480��$y�Hidden=�5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�A� -   $��leftEllipse��$ 0��$ 0��$��75��$��75�����	#FF4D298D	 J� Opacity$	 1Q�
�������$y�Visible=�A� -   $��rightEllipse��$	 100��$ 0�����	#FF2CACE3$��75��$��75��$	 1Q�
�������$y�Visible=�A� -   $��leftFootEllipse��$	 200��$ 0��
��� ���$��75��$��75��$	 1Q�
�������$y�Visible=�A� -	   $��rightFootEllipse��$	 300��$ 0��
���  ��$��75��$��75��$	 1Q�
�������$y�Visible=�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  -
   $��kinectSensorChooser1��$
 1040��$ 47��$	��328���� -   $��	headImage��$ 66��$ 90��$��87��$
��Fill=�$��84��$/��)/SkeletalTracking;component/c4f-color.png��$y�Visible=��� -   $��
rectangle7��$	 622��$ 77��$	��100��
���   �$	��200��
��� � �$y�Hidden=��� -   $��image1��$	 270��$	 690��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��image2��$	 360��$	 461��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��image3��$	 492��$	 275��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��image4��$	 850��$	 275��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��image5��$
 1000��$	 461��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��image6��$
 1100��$	 690��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png���� -   $��imageHandLeft��$	 622��$ 77��$	��100��$
��Fill=�$	��100��$6��0/SkeletalTracking;component/Images/hand_left.png���� -   $��imageHandRight��$	 722��$ 77��$	��100��$
��Fill=�$	��100��$7��1/SkeletalTracking;component/Images/hand_right.png���� -   $��image7��$
 1373��$ 47��$��99��$
��Fill=�$��98��$3��-/SkeletalTracking;component/Images/light1.png��$y�Hidden=�:   4Microsoft.Samples.Kinect.WpfViewers.SilhouetteViewer  -   $��SilhouetteViewer1��$ 0��$ 0��$
��1600��$	��900��$y�Visible=� �� $ kinectSensorChooser1��$ Kinect�