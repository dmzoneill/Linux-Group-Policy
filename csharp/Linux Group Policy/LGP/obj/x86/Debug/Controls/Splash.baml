   M S B A M L   `   `   `  ����  Linux Group Policy    LGP.Controls.Splash   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     6=   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6   -   5          �Name$  SplashWindow��5   '    � Title$ Splash��6�   $	��228��6�   $	��573��6�    � WindowStartupLocation$ CenterScreen=�6�    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6/   � AllowsTransparency .  � WindowStyle$
 None=�6_   � Topmost . 
������  � Triggers 5      :� 5      	 :� RoutedEvent$	 Window.Unloaded��6   .[�5      �� .��5   
   �� -   5        
 g�Name$
 FormFadeOut��5      .��5      W� -   5        $
 FormFadeOutAnimation��5       �� 
TargetName$ SplashWindow��68    �� TargetProperty$ (Window.Opacity)�6]    W� From$	 0.9X�6�   	 W� To$	 0.0X�6�    g� Duration$ 0:0:1D�6�    g� AutoReverse .   g� RepeatBehavior$ 1x��6�   5      5   	   5	      .��5      � 
������ $��0q�6"   .+�5      �� $��1q�6    �� CornerRadius$ 3��6!   
������� �� Padding$ 0q�6E   ��5      �� 5   
    �� 
StartPoint$ 0.5,0&�6    �� EndPoint$ 0.5,1&�61   .��5      �  � Color$ 	#66FFFFFF��6    � Offset$	 0.1Q�6+   � 5      $ 	#FFFFFFFF��6   $	 0.2Q�6+   � 5      $ 	#FFFFFFFF��6   $	 0.3Q�6+   � 5      $ 	#FFFFFFFF��6   $	 0.4Q�6+   � 5      $ 	#AAFFFFFF��6   $	 0.5Q�6+   � 5      $ 	#BBFFFFFF��6   $	 0.6Q�6+   � 5      $ 	#CCFFFFFF��6   $	 0.7Q�6+   � 5      $ 	#DDFFFFFF��6   $	 0.8Q�6+   � 5      $ 	#EEFFFFFF��6   $	 0.9Q�6+   � 5      $ 	#FFFFFFFF��6   $	 1.0Q�6+   5      ._�5      � $��0q�6   $��Stretch=�6   $��Stretch=�64   .+�5   
   �� -   5        $��textBlockComponentName��5   O   $��19��6   $
��Left=�6    $��
17,178,0,0q�6;   $��Loading components��6m   $	��Top=�6�   $	��543��6�   $��Medium"�6�   $��12(�6�    K� ZIndex$ 2��6�   �� 5   
   -   5        $��label1��5   �   $��50��6_   $
��Left=�6k   $��9,41,0,0q�6�   $	��Top=�6�   $	��551��6�   $��26(�6�   $
��Bold"�6�   $ 2��6�   ��6   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$ LGP.Properties.Resources��
   Key$ LinuxGroupPolicyStudio��6�   �� 5   
   -   5        $��textBlockDllPath��5   O   $��24��6   $
��Left=�6    $��
17,200,0,0q�6;   $��	TextBlock��6g   $	��Top=�6x   $	��543��6�   $��9(�6�   $ 2��6�   $
��Bold"�6�   �� 5   
   -   5        $��textBlockComponentVersion��5   c   $��Normal"�6   $��19��6(   $
��Left=�64   $��
17,160,0,0q�6O   $��Loading components��6�   $	��Top=�6�   $	��543��6�   $��10(�6�   $ 2��6�   �� 5    
   -   5        $��textBlockLoading��5    M   $��17��6   $
��Left=�6    $��14,7,0,0q�6;   $	��Top=�6�   $	��136��6�   $��10(�6�   ��6e     $ LGP.Properties.Resources��$ Loading��6�   �� 5!   
   -	   5        $��image1��5!   N   $	��320��6   $��Right=�6   $��0,-21,-79,0q�69   $
��Fill=�6\   $	��Top=�6k   $	��320��6�   $ 1��6�    J� Opacity$	 0.7Q�6�    5"      :� 5#      $	 Image.Loaded��6   .[�5$      �� .��5%      �� .��5&      W� $ image1��6$   $ Opacity�6C   $	 0.5X�6g   $	 0.9X�6r   $ 0:0:2D�6{    . $ Forever��6�   5'      5(      5)      5+      �� 5,   
   -
   5        $��textBlockCopyright��5,   N   $��65��6   $
��Left=�6    $��	17,89,0,0q�6;   $�� ��6h   $	��Top=�6p   $	��543��6�   $ 2��6�   5-   	   5.      5/      50      