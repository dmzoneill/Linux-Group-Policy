   M S B A M L   `   `   `  ����S  OLinux Group Policy, Version=1.0.2091.1340, Culture=neutral, PublicKeyToken=null+clr-namespace:LGP.ControlsLGP.Controls  5         LGP.LGPWindow   6	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6K   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   ,localclr-namespace:LGP.Controls     6  -   5          �Name$  LGPMainWindow��5   �  $	��650��6�  $
��1100��6�  $	��300��6�  $	��780��6�   � WindowStartupLocation$ CenterScreen=�6   J� Opacity$ 1Q�6F   � AllowsTransparency .  � WindowStyle$
 None=�6l   � 
ResizeMode$ CanResizeWithGrip=�6  $��0q�6�   � Title 6X  0  *Infralution.Localization.Wpf.ResxExtension     ResxName$ LGP.Properties.Resources��
   Key$ LinuxGroupPolicyStudio����60  Z� .��5      � -   5        	 �Name$	 outer��5   	   
 � RowDefinitions
 5      �� 5      $��* �6    � ColumnDefinitions 5      �� 5      $
��250* �6   .+�5
      �� 
���   �$��1q�6!   $��0q�65   $��5��6@    �� Padding$ 10,10,10,10q�6Q    �� CornerRadius$ 3��6g   ��5      �� 5   
    �� 
StartPoint$ 0.5,0&�6    �� EndPoint$ 0.5,1&�61   .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$	 0.1Q�6+   � 5      $ 	#EEFFFFFF��6   $	 0.2Q�6+   � 5      $ 	#DDFFFFFF��6   $	 0.3Q�6+   � 5      $ 	#CCFFFFFF��6   $	 0.4Q�6+   � 5      $ 	#BBFFFFFF��6   $	 0.5Q�6+   � 5      $ 	#BBFFFFFF��6   $	 0.6Q�6+   � 5      $ 	#CCFFFFFF��6   $	 0.7Q�6+   � 5      $ 	#DDFFFFFF��6   $	 0.8Q�6+   � 5      $ 	#FFFFFFFF��6   $	 0.9Q�6+   � 5      $ 	#FFFFFFFF��6   $	 1.0Q�6+   5      ._�5      � -   5        $
	 main��5      $��1��6   $��1��6(   
 5   
   �� 5      $��25 �6   $��25��6&   �� 5      $��1 �6   $��1��6%   �� 5      $
��395* �6   �� 5       $��1 �6   $��1��6%   �� 5!      -   5         �Name$ statusBarRow��5!   3   $��0 �6   $��0��6%   63    5#   
   �� 5$      $
��220* �6   $	��200��6*   �� 5%      $
��500* �6   $	��250��6*   �� 5&      $	��80* �6   $��80��6)   .+�5)   
   o� -   5        $��menu1��5)   >    o� 
IsMainMenu . $��0��6!   $��0��61   $��0q�6K   $��0q�6V   $
��Left=�6b    K� ZIndex$	 200��6}   
������ $	��top=�6�   �� 5+   
   -   5         ��Name$ 
rectangle1��5+   5   $��1��6   $��3��6!   ���	#00E3EFFF$��5,0,5,0q�6\      LGP.Controls.Background  5-   
   $��0��6   $��3��6+   $��Center=�6?   $��Center=�6\   $��2��6w   6�   �� 5/   
   -   5        $ 
rectangle2��5/   F   $��3��6   $��3��6!   $��5,0,5,0q�65   ���	#00E3EFFF$ 1��6m   �� 51   
   -   5         ��Name$ 
statusBar1��51   R   $��4��6   $��3��6!   $��4,0,0,0q�65   $��0q�6F   $ 1��6f   
������ $��Bottom=�6�   ��52      �� 53      .R�54      � 
 55      �� 56      $��* �6"    58      �� 59      $��4* �6%   �� 5:      $
��Auto �6%   �� 5;      $��* �6%   �� 5<      $��* �6%   5>      5?      .@�5A      �� .��5B      �� Ready6   5C      �� 5D      $��1��6   .��5E      �  � Value$ 30Q�6   $��80��6%   $��18��60   $y�Hidden=�6<   5F      �� 5G      $��2��6   .��5H      �� $y�Hidden=�6   Set61   5I      �� 5J      $��3��6   .��5K      �� $y�Hidden=�6   Go!61   5L      5M      �� 5O   
   -	   5        $��dragBar��5O   V   $��1��6   $��0��6    $��Stretch=�6-   $��0q�6K   $��Stretch=�6e   5Q   	   5R      �� 5T      -
   5        $��Right=�5T      $��	0,15,65,0q�6)   $��17��6<   $��17��6G   $	��Top=�6S   $	 0.3Q�6�   ��5U      �� 5V   
   $	 0,1&�6   $	 0,0&�6-   .��5W      � $ 	#FFF3F3F3��6   $ 0Q�6+   � 5X      $ 	#FFEBEBEB��6   � 5Y      $ 	#FFDDDDDD��6   $ 0.806Q�6+   � 5Z      $ 	#FF3F76BF��6   $ 1Q�6+   5[      5]      �� 5_      -   5        $��Right=�5_      $��	0,15,40,0q�6)   $��17��6<   $��17��6G   $	��Top=�6S   $	 0.3Q�6�   ��5`      �� 5a   
   $	 0,1&�6   $	 0,0&�6-   .��5b      � $ 	#FFF3F3F3��6   $ 0Q�6+   � 5c      $ 	#FFEBEBEB��6   � 5d      $ 	#FFDDDDDD��6   $ 0.806Q�6+   � 5e      $ 	#FF3F76BF��6   $ 1Q�6+   5f      5h      �� 5j      -   5        $��Right=�5j      $��	0,15,15,0q�6)   $��17��6<   $��17��6G   $	��Top=�6S   $	 0.3Q�6�   ��5k      �� 5l   
   $	 0,1&�6   $	 0,0&�6-   .��5m      � $ 	#FFF3F3F3��6   $ 0Q�6+   � 5n      $ 	#FFEBEBEB��6   � 5o      $ 	#FFDDDDDD��6   $ 0.806Q�6+   � 5p      $ 	#FF3F76BF��6   $ 1Q�6+   5q      5s      5t      5u      