   M S B A M L   `   `   `  ����T  PAvalonDock, Version=1.3.3571.0, Culture=neutral, PublicKeyToken=85a1e0ada7ec13e4;,clr-namespace:AvalonDock;assembly=AvalonDock
AvalonDock  5   2   LGP.Components.Panels"   LGP.Components.Panels.Panels   6   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     65   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6w   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6�   ;ad,clr-namespace:AvalonDock;assembly=AvalonDock     62  .��5      � .+�5         AvalonDock.DockingManager  -   5           Name$  dockManager��5      
������ $��5,0,5,0q�6F   .��5         AvalonDock.ResizingPanel     Orientation$ Vertical=�6   .+�5   
     $ 
Horizontal=�6   .+�5         AvalonDock.DockablePane  -   5          Name$ SideBarExplorer��5         ResizeWidth$	 220 �65   
�������6k      AvalonDock.DocumentPane  5      -   5        $��MainComponentContainer��5   /   
�������6M   5	        5
   
   -   5        $��	BottomBar��5
   Q      ResizeHeight$	 100 �6   
�������.@�5          AvalonDock.DockableContent  -   5          Name$ 	errorPane��5   n      AvalonDock.ManagedContent   Title 6   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$0 *LGP.Components.Panels.Properties.Resources��
   Key$ Errors��.��5      �� $��0q�6   $��0q�6"   
������ $��0q�6H   	 �� View	 5      �� 5      .P�5      �� ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ Error���� 5      ��6#     $0 *LGP.Components.Panels.Properties.Resources��$
 Page���� 5      ��6#     $0 *LGP.Components.Panels.Properties.Resources��$
 File���� 5      ��6#     $0 *LGP.Components.Panels.Properties.Resources��$	 Row��5      5      5        5      -   5        $ 
outputPane��5   n    6     $0 *LGP.Components.Panels.Properties.Resources��$ Output��.��5      �� -   5        $��	listView1��5   @   $��Stretch=�6   $��0q�65   $��Stretch=�6Q   	 5      �� 5      .P�5      �� 
 �� Width$	
 200��6�   ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ Message�� �� DisplayMemberBinding 6t   �� *	Message+ �� CellTemplate 5      �� 5      .R�5      ��  �� TextAlignment$
 Left=�6=   $
��Wrap=�6R   ��6$   �� *	Message+6R   5      5!      �� 5"      $	
 150��6�   ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ ExceptionType�� 6z   �� *ExceptionType+ 5#      �� 5$      .R�5%      �� $
 Left=�6C   $
��Wrap=�6X   ��6$   �� *ExceptionType+6X   5&      5(      �� 5)      $	
 200��6�   ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ Source�� 6s   �� *Source+ 5*      �� 5+      .R�5,      �� $
 Left=�6<   $
��Wrap=�6Q   ��6$   �� *Source+6Q   5-      5/      �� 50      $	
 300��6�   ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ 
StackTrace�� 6w   �� *
StackTrace+ 51      �� 52      .R�53      �� $
 Left=�6@   $
��Wrap=�6U   ��6$   �� *
StackTrace+6U   54      56      �� 57      $	
 150��6�   ��6#     $0 *LGP.Components.Panels.Properties.Resources��$ ThrownBy�� 6u   �� *
TargetSite+ 58      �� 59      .R�5:      �� $
 Left=�6@   $
��Wrap=�6U   ��6$   �� *
TargetSite+6U   5;      5=      5>      5@      5A      5B      5D   	   5E      5G      5H      