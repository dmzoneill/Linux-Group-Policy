   M S B A M L   `   `   `  ����  LGP.Modules.ServerControl0    *LGP.Modules.ServerControl.ConnectionViewer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     6C   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6  .��5      � $��20q�6	     � RowDefinitions  5      �� 5      $��80 �6   �� 5      $��80 �6   �� 5      $	��160 �6   �� 5      $	��240 �6   .+�5	      �� -   5        $��image1��5	   @   $��64��6   $��Right=�6   $��10q�64   $
��Fill=�6N   $	��Top=�6]   $��64��6u   �� 5
      $��70��6   $��0,5,0,0q�6   $	��Top=�6,   $��0q�6[   $��1��6o   ��6D   Z� .��5      � .+�5   
   �� $��0,0,0,0q�6g   $��28��6x   $��Right=�6�   $	��Top=�6�   $��12(�6�   $
��Bold"�6�   ���	#FF666666��6   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$4 .LGP.Modules.ServerControl.Properties.Resources��
   Key$ ServerId��6�   �� 5   
   $��0,15,0,0q�6   $��20��6&   $��Stretch=�62   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5   
   -   5        $��ipaddr��5   7   $��0,25,5,0q�6   $��
Ip address��6"   $��28��6E   $��Right=�6Q   $	��Top=�6m   $��12(�6�   5   	   5      �� 5      $	��150��6   $��0,5,0,0q�6   $	��Top=�6-   $��0q�6\   $��2��6p   ��6E   Z� .��5      � .+�5   
   �� $��0,0,0,0q�6l   $��28��6}   $��Right=�6�   $	��Top=�6�   $��12(�6�   $
��Bold"�6�   ���	#FF666666��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ PolicyControl��6�   �� 5   
   $��0,15,0,0q�6   $��20��6&   $��Stretch=�62   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5   
   -   5        $��PushPolicyUpdate��5   �   $��23��6l   $��Right=�6x   $��0,35,5,0q�6�   $	��Top=�6�   $	��163��6�   ��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ SyncPolicies��6�   �� 5   
   -   5        $��PushClientUpdate��5   �   $��23��6k   $��Right=�6w   $��0,70,5,0q�6�   $	��Top=�6�   $	��163��6�   ��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ SyncClients��6�   �� 5   
   -   5        $��PushModulesUpdate��5   �   $��23��6k   $��Right=�6w   $��	0,105,5,0q�6�   $	��Top=�6�   $	��163��6�   ��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ SyncModules��6�   5   	   5      �� 5      $	��230��6   $��0,5,0,0q�6   $	��Top=�6-   $��0q�6\   $��3��6p   ��6E   Z� .��5      � .+�5   
   �� $��0,0,0,0q�6i   $��28��6z   $��Right=�6�   $	��Top=�6�   $��12(�6�   $
��Bold"�6�   ���	#FF666666��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ 
StressTest��6�   �� 5   
   $��0,15,0,0q�6   $��20��6&   $��Stretch=�62   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5   
   $��5,25,5,0q�6   $��28��6y   $��Right=�6�   $	��Top=�6�   $��12(�6�   ��6"     $4 .LGP.Modules.ServerControl.Properties.Resources��$ Simulate��6�   �� 5   
   -   5        $��slider1��5   M   $��23��6   $��Stretch=�6   $��5,55,5,0q�6;   $	��Top=�6\    � Value$ 1Q�6t    �� IsDirectionReversed .   � SmallChange$ 1Q�6�    � Maximum$	 500Q�6�    � Minimum$ 1Q�6�    �� IsSnapToTickEnabled . 	 �� Interval$	 1��6�   6�   �� 5    
   -   5        $��
numUpdates��5    �   $��5,75,5,0q�6   $��28��6~   $��Right=�6�   $	��Top=�6�   $��12(�6�   ��6"     $4 .LGP.Modules.ServerControl.Properties.Resources��$ ClientUpdates��6�   �� 5!   
   -   5        $��slider2��5!   N   $��23��6   $��Stretch=�6   $��	5,100,5,0q�6;   $	��Top=�6]   $	 1��6u   $	 500Q�6�   $ 5Q�6�   $ 5Q�6�    . 6�   �� 5"   
   -	   5        $��delay��5"   �   $��	5,120,5,0q�6   $��28��6w   $��Right=�6�   $	��Top=�6�   $��12(�6�   ��6#     $4 .LGP.Modules.ServerControl.Properties.Resources��$ Delay��6�   �� 5#   
   -
   5        $��
StressTest��5#   �   $��23��6b   $��Right=�6n   $��	5,150,5,0q�6�   $	��Top=�6�   $��75��6�   ��6     $4 .LGP.Modules.ServerControl.Properties.Resources��$ Go��6�   �� 5$   
   -   5        $��threadStatusLabel��5$   V   $�� ��6   $��28��6   $��Right=�6'   $��	5,180,5,0q�6C   $	��Top=�6o   5%   
   5&      5'      5(      