   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    SkeletalTracking!   SkeletalTracking.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   K   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   -   5          � Title$  
MainWindow��5   	    � WindowState$ 	Maximized=�5   "    � SizeToContent$ WidthAndHeight=�6�    � 
ResizeMode$ CanResizeWithGrip=�6  $��Stretch=�67  $��Stretch=�6U  .��5      �� -   5        $��
MainCanvas��5      $	��900��6   $
��1600��6,   .+�5   
   �� -   5        $��
background��5   <    �� Left$ 0��6   
 �� Top$ 0��6    $	��900��6/   $
��Fill=�6N   $/��)/SkeletalTracking;component/Images/bg.png��6]   $
��1600��6�   �� 5	   
   -   5        $��vortex��5	   <   $ 0��6   $ 0��6    $	��900��6/   $
��Fill=�6J   $
��1600��6Y   $5��//SkeletalTracking;component/Images/nbb20001.png��6f   $y�Visible=�6�   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  5
   
   -   5        $��kinectColorViewer1��5
   W   $ 0��6   $ 0��6/   $	��640��6>   $	��480��6J   $y�Hidden=�5   `   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6   ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�6`   A� 5   
   -   5        $��leftEllipse��5   =   $ 0��6   $ 0��6"   $��75��61   $��75��6Q   ���	#FF4D298D	 J� Opacity$	 1Q�6m   
�������$y�Visible=�6�   A� 5   
   -   5        $��rightEllipse��5   P   $	 100��6   $ 0��6$   ���	#FF2CACE3$��75��6D   $��75��6d   $	 1Q�6o   
�������$y�Visible=�6�   A� 5   
   -   5        $��leftFootEllipse��5   M   $	 200��6   $ 0��6$   
��� ���$��75��6A   $��75��6d   $	 1Q�6o   
�������$y�Hidden=�6�   A� 5   
   -	   5        $��rightFootEllipse��5   J   $	 300��6   $ 0��6$   
���  ��$��75��6>   $��75��6b   $	 1Q�6m   
�������$y�Hidden=�6�   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5   
   -
   5        $��kinectSensorChooser1��5   D   $
 1040��6!   $ 47��64   $	��328��6`   �� 5   
   -   5        $��	headImage��5   =   $ 66��6   $ 90��6!   $��87��61   $
��Fill=�6N   $��84��6]   $/��)/SkeletalTracking;component/c4f-color.png��6h   $y�Hidden=�6�   �� 5   
   -   5        $��
rectangle7��5   C   $	 622��6   $ 77��6&   $	��100��66   
���   �$	��200��6d   
��� � �$y�Hidden=�6|   �� 5   
   -   5        $��image1��5   ?   $	 270��6   $	 690��6"   $��99��63   $
��Fill=�6M   $��98��6\   $3��-/SkeletalTracking;component/Images/light1.png��6g   �� 5   
   -   5        $��image2��5   ?   $	 300��6   $	 461��6"   $��99��63   $
��Fill=�6M   $��98��6\   $3��-/SkeletalTracking;component/Images/light1.png��6g   �� 5   
   -   5        $��image3��5   ?   $	 370��6   $	 200��6"   $��99��63   $
��Fill=�6M   $��98��6\   $3��-/SkeletalTracking;component/Images/light1.png��6g   �� 5   
   -   5        $��image4��5   ?   $	 970��6   $	 200��6"   $��99��63   $
��Fill=�6M   $��98��6\   $3��-/SkeletalTracking;component/Images/light1.png��6g   �� 5   
   -   5        $��image5��5   @   $
 1060��6   $	 461��6#   $��99��64   $
��Fill=�6N   $��98��6]   $3��-/SkeletalTracking;component/Images/light1.png��6h   �� 5   
   -   5        $��image6��5   @   $
 1100��6   $	 690��6#   $��99��64   $
��Fill=�6N   $��98��6]   $3��-/SkeletalTracking;component/Images/light1.png��6h   �� 5   
   -   5        $��imageHandLeft��5   ?   $	 622��6   $ 77��6"   $	��100��62   $
��Fill=�6T   $	��100��6c   $6��0/SkeletalTracking;component/Images/hand_left.png��6o   �� 5   
   -   5        $��imageHandRight��5   ?   $	 722��6   $ 77��6"   $	��100��62   $
��Fill=�6U   $	��100��6d   $7��1/SkeletalTracking;component/Images/hand_right.png��6p   �� 5   
   -   5        $��image7��5   ?   $
 1373��6   $ 47��6#   $��99��63   $
��Fill=�6M   $��98��6\   $3��-/SkeletalTracking;component/Images/light1.png��6g   $y�Hidden=�6�   A� 5   
   -   5        $��ellipse1��5   B   $	 993��6   $	 583��6$   $	��100��65   
���   �$	��100��6a   $y�Hidden=�6m   
���   �6�   :   4Microsoft.Samples.Kinect.WpfViewers.SilhouetteViewer  5   
   -   5        $��SilhouetteViewer1��5   W   $ 0��6   $ 0��6.   $
��1600��6=   $	��900��6J   $y�Visible=�6�    6p   �� $ kinectSensorChooser1��$ Kinect�6�   5      5      