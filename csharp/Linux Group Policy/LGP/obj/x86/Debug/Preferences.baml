   M S B A M L   `   `   `  ����  Linux Group Policy    LGP.Preferences   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��40 �6   �� 5      $��40 �6   �� 5      $	��144 �6   �� 5      $��20 �6   �� 5      $	��44* �6    � ColumnDefinitions 5   
   �� 5      $	��140 �6   �� 5      $
��200* �6   �� 5      $��60 �6   .+�5   
   �� -   5        $��label1��5   p   $��28��6Q   $��	0,10,20,0q�6]   $	��Top=�6~   $��Right=�6�   ��6   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$ LGP.Properties.Resources��
   Key$ Language��6�   �� 5   
   -   5        $��_cultureCombo��5   '   $��
10,10,10,0q�6   $��1��6l   $��20��6|   $	��Top=�6�   �� 5   
   -   5        $��label4��5   o   $��28��6Q   $��0,4,10,0q�6]   $	��Top=�6}   $��Right=�6�   $��2��6�   ��6     $ LGP.Properties.Resources��$ Detected��6�   �� 5   
   -   5        $��ServerListBox��5   k   $��1��6   $��Stretch=�6"   $��	10,4,10,0q�6X   $��Stretch=�6�   $��2��6�   ��6@   �� 6�   �� 5   
   -   5        $��border1��5   g   $��0q�6   $��1��6%   $��Stretch=�65   $��
10,0,10,10q�6S   $��Stretch=�6v   $��3��6�   ��5      �� 5      .��5      � -   5        	stop1�� � Color$ White��5   2    � Offset$
 -0.2Q�6@   � 5      -   5        	stop2��$ 	#FFAEC7E5��5   2   $
 -0.1Q�6D   � 5      -   5        	stop3��$ 	#FFAEC7E5��5   2   $	 0.0Q�6D   � 5       -	   5        	stop4��$ White��5    2   $	 0.1Q�6@   5!       � Triggers 5#      :� 5$       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5%      �� ��5&      �� 5'       g� AutoReverse . 	 g� RepeatBehavior$	 Forever��6<   .��5(   "   W� 
 g� Duration$
 0:0:3D�62    W� From$
 -0.2X�6C    �� 
TargetName$ stop1��6O    �� TargetProperty$ Offset�6m   	 W� To$	 0.9X�6�   W� 5)   "   $
 0:0:3D�62   $
 -0.1X�6C   $ stop2��6O   $ Offset�6m   $	 1.0X�6�   W� 5*   "   $
 0:0:3D�62   $	 0.0X�6C   $ stop3��6N   $ Offset�6l   $	 1.1X�6�   W� 5+   "   $
 0:0:3D�62   $	 0.1X�6C   $ stop4��6N   $ Offset�6l   $	 1.2X�6�   5,      5.      5/      51      �� 52   
   -
   5        $��button2��52   �   $��1��6P   $��23��6`   $��Right=�6l   $��0,4,10,0q�6�   $	��Top=�6�   $	��100��6�   $��4��6�   ��6     $ LGP.Properties.Resources��$ Select��6�   �� 53   
   -   5        $��label5��53   d   $�� ��6   $��1��6   $��28��6+   $
��Left=�67   $��10,4,0,0q�6R   $	��Top=�6r   $	��200��6�   $��4��6�   �� 54   
   $��10(�6X   $
��Bold"�6f   ���	#FF666666$��28��6�   $��Stretch=�6�   $��
10,12,10,0q�6�   $��Stretch=�6�   $��1��6�   $��1��6�   ��6     $ LGP.Properties.Resources��$ ServerDiscovery��6�   55      56      