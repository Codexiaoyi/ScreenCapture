   M S B A M L   `   `   `  ����L  HScreenCaptureDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ScreenCaptureDemoScreenCaptureDemo  5   	   #    ScreenCaptureDemo.PrintScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ScreenCaptureDemo     5   	   -   5          �Name$  windows��5      $��0q�5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$
 None=�5
   	    � Title$ PrintScreen��5   	    � 	Resources 5         Exit �� Text��%(   &	        �� 5   
   $
 Exit��6'   5       J� InputBindings 5      �� 5   
    �� Gesture$	 Esc��6   
 �� Key$	 Esc��6#   	 �� Command#	 [  6.   6P   
 J� CommandBindings
 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      6M   .��5      � -   5         �Name$ 	Container��5      .+�5   
   �� -   5        $��ImageContainer��5       � UseLayoutRounding . 6&   �� 5   
   $��Center=�6   $��Center=�6/    J� Opacity$	 0.3Q�6L   �� 6[   -   5         ��Name$ 	SkinImage��5   a   $��Center=�6t   $��Center=�6�   6�   �� 5   
   -   5        $��MainGrid��5      
������ 5      �� 5   
   -   5        $��	ToolPanel��5   .    �� Orientation$ 
Horizontal=�6   $y�	Collapsed=�6@   $��28��6W   $	��150��6c   ���#f9fffb.+�5      �� -   5         ��Name$ OkClick��5      ���#f9fffb$��0q�6L   .��5      �� $��./ok.png��6   6,   5      �� 5      -	   5        $ 	ExitClick��5      ���#f9fffb$��0q�6P   .��5      �� $��./error.png��6   6/   5       �� 5!      -
   5        $ 	SaveClick��5!      ���#f9fffb$��0q�6P   .��5"      �� $��
./save.png��6   6.   5#      5$      5%      5'      