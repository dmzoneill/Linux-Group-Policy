   M S B A M L   `   `   `  ����   Linux Group Policy    
LGP.Splash   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  SplashWindow�� � Title$ Splash��$	��228��$	��573�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � AllowsTransparency .  � WindowStyle$
 None=� � Topmost . 
������  � Triggers :� 	 :� RoutedEvent$	 Window.Unloaded��.[��� .���� -   
 g�Name$
 FormFadeOut��.��W� -   $
 FormFadeOutAnimation�� �� 
TargetName$ SplashWindow�� �� TargetProperty$ (Window.Opacity)� W� From$	 0.9X�	 W� To$	 0.0X� g� Duration$ 0:0:1D� g� AutoReverse .   g� RepeatBehavior$ 1x��.��� 
������ $��0q�.+��� $��1q� �� CornerRadius$ 3��
������� �� Padding$ 0q�����  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.���  � Color$ 	#66FFFFFF�� � Offset$	 0.1Q�� $ 	#FFFFFFFF��$	 0.2Q�� $ 	#FFFFFFFF��$	 0.3Q�� $ 	#FFFFFFFF��$	 0.4Q�� $ 	#AAFFFFFF��$	 0.5Q�� $ 	#BBFFFFFF��$	 0.6Q�� $ 	#CCFFFFFF��$	 0.7Q�� $ 	#DDFFFFFF��$	 0.8Q�� $ 	#EEFFFFFF��$	 0.9Q�� $ 	#FFFFFFFF��$	 1.0Q�._�� $��0q�$��Stretch=�$��Stretch=�.+��� -   $��textBlockComponentName��$��19��$
��Left=�$��
17,178,0,0q�$��Loading components��$	��Top=�$	��543��$��Medium"�$��12(� K� ZIndex$ 2���� -   $��label1��$��Linux Group Policy Studio��$��50��$
��Left=�$��9,41,0,0q�$	��Top=�$	��551��$��26(�$
��Bold"�$ 2��� -   $��progressBar1��$��20��$
��Left=�$��
15,148,0,0q�$	��Top=�$	��452�� � Value$ 1Q� J� Opacity$	 0.8Q����	#660162D3$��0q�$y�Hidden=��� -   $��textBlockDllPath��$��24��$
��Left=�$��
17,200,0,0q�$��	TextBlock��$	��Top=�$	��543��$��9(�$ 2��$
��Bold"��� -   $��textBlockComponentVersion��$��Normal"�$��19��$
��Left=�$��
17,160,0,0q�$��Loading components��$	��Top=�$	��543��$��10(�$ 2���� -	   $��textBlockLoading��$��17��$
��Left=�$��14,7,0,0q�$��	Loading..��$	��Top=�$	��136��$��10(��� -
   $��image1��$	��320��$��Right=�$��0,-21,-79,0q�$
��Fill=�$	��Top=�$	��320��$ 1��$	 0.7Q��� -   $��textBlockCopyright��$��65��$
��Left=�$��	17,89,0,0q�$�� ��$	��Top=�$	��543��$ 2��