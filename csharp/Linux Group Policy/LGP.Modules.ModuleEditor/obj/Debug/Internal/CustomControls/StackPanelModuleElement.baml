   M S B A M L   `   `   `  ����  LGP.Modules.ModuleEditorN    HLGP.Modules.ModuleEditor.Internal.CustomControls.StackPanelModuleElement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � Triggers  5      :� 5       :� RoutedEvent$ UserControl.Loaded��6   .[�5      �� .��5	   
   �� .��5
      W� -   5         W�Name$ LoadedDoubleAnimation��5
       �� 
TargetName$ stop1��6;    �� TargetProperty$ Offset�5       W� From$
 0.99X�6?   	 W� To$
 0.65X�6K    g� Duration$ 0:0:.3D�6U    g� 	BeginTime$ 0:0:0X�6g   	 g� AutoReverse	 . 6y   5      5   	   5      .��5      � 
 � RowDefinitions
 5      �� 5      $��54 �6   �� 5      $��1 �6   .+�5      �� -   5        $��PanelItemBorder��5      
������ $��0q�5   '   $��Stretch=�6;   $��0q�6Y   $��Stretch=�5   $    �� CornerRadius$ 1��6@   ��5      �� 5   
   .��5      � -   5        	stop1�� � Color$ White��5   (    � Offset$
 0.99Q�66   � 5      $ 	#FFAEC7E5��6   $	 1.0Q�6+   5      ._�5      � -   5        $��PanelItemGrid��5      $��Stretch=�6"   $��Stretch=�6>   $��2q�6\    � ColumnDefinitions 5   
   �� 5      $��38 �6   �� 5       $
��150* �6   
 5"   
   �� 5#      $	��26* �6   �� 5$      $	��18* �6   .+�5&   
   �� -   5        $��PanelItemImage��5&   U   $��2��6   $��28��6!   $��Center=�6-   $��2q�6J   $��Center=�5'      $��28��6+    J� Opacity$	 0.6Q�66   �� 5(   
   -   5        $��PanelItemModuleLabel��5)      $��Module Name��5(      $��1��6&   $��30��66   $��Stretch=�6B   $��2,4,0,0q�6`   $	��Top=�5)   ,   $��12(�6D   $
��Bold"�6R   ���	#FF689AAA
������ 5*      �� 5+   
   -   5        $��PanelItemDetails��5,   "   $��Version��5+      $��1��6"   $��1��62   $��22��6?   $��Stretch=�6K   $��5,-8,5,0q�5,      $	��Top=�6:   $��10(�6R   $��Normal"�6`   
������ 5-      5.   	   5/      �� 50      -   5        $��
rectangle1��50   4   $��55��6   $
��Left=�6   $	��Top=�6F   $	��410��6^    K� ZIndex$ 65��51      
������   52      :� 53   
   $ Canvas.MouseEnter��6   .[�54      �� .��55      �� .��56      W� $ stop1��6    $ Offset�6>   $
 0.99X�6a   $
 0.65X�6m   $ 0:0:.3D�57       W� 58      $ PanelItemImage��6    $ Opacity�6G   $	 0.6X�6k   $	 0.8X�59       $ 0:0:.2D�6)   5:      5;      5<      :� 5=   
   $ Canvas.MouseLeave��6   .[�5>      �� .��5?      �� .��5@      W� $ stop1��6    $ Offset�6>   $
 0.65X�6a   $
 0.99X�6m   $ 0:0:.3D�5A       W� 5B      $ PanelItemImage��6    $ Opacity�6G   $	 0.8X�6k   $	 0.6X�5C       $ 0:0:.2D�6)   5D      5E      5F      5H      �� 5I      $��1��6   
������ ���	#FFAAAAAA$��0q�6K   $��1��6_   6l   5J      5K      