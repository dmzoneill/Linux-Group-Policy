   M S B A M L   `   `   `  ����(  $LGP.Modules.OrganizationUnitExplorerH    BLGP.Modules.OrganizationUnitExplorer.Internal.NodeViews.ClientPane   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     6[   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6  .��5      � $��20q�6	     � RowDefinitions  5      �� 5      $��80 �6   �� 5      $	��100 �6   �� 5      $	��160 �6   �� 5      $
��160* �6   .+�5
      �� -   5        $��image1��5
   G   $��64��6   $��Right=�6   $��	0,10,10,0q�64   $
��Fill=�6U   $	��Top=�6d   $��64��6|   �� 5      -   5        $��	groupBox1��5   -   $	��100��6   $��0,0,0,0q�6   $	��Top=�6>   $��0q�6m   $��1��6�   ��6V   Z� .��5      � .+�5   
   �� $��0,0,0,0q�6r   $��28��6�   $��Right=�6�   $	��Top=�6�   $��12(�6�   $
��Bold"�6�   ���	#FF666666��6   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$? 9LGP.Modules.OrganizationUnitExplorer.Properties.Resources��
   Key$ ClientId��6�   �� 5   
   $��0,15,0,0q�6   $��20��6&   $��Stretch=�62   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5   
   -   5        $��ClientNameLabel��5   "   $��0,25,0,0q�6   $��Client name��69   $��28��6O   $��Right=�6[   $	��Top=�6w   $��12(�6�   �� 5   
   -   5        $��ClientGuidLabel��5   '   $��	5,55,05,0q�6   $��Client Guid��6>   $��33��6Q   $	��Top=�6]   $��Stretch=�6u    �� TextAlignment$ Right=�6�   $��12(�6�   $
��Wrap=�6�   5   	   5      �� 5      -   5        $��	groupBox2��5   -   $	��160��6   $��0,0,0,0q�6   $	��Top=�6>   $��0q�6m   $��2��6�   ��6V   Z� .��5      � .+�5   
   �� $��0,0,0,0q�6r   $��28��6�   $��Right=�6�   $	��Top=�6�   $��12(�6�   $
��Bold"�6�   ���	#FF666666��6     $? 9LGP.Modules.OrganizationUnitExplorer.Properties.Resources��$ ClientOs��6�   �� 5   
   $��0,15,0,0q�6   $��20��6&   $��Stretch=�62   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5   
   -   5        $��ClientOSLabel��5   "   $��0,25,0,0q�6   $��Os��67   $��28��6D   $��Right=�6P   $	��Top=�6l   $��12(�6�   �� 5   
   -   5        $��ClientArchLabel��5   "   $��0,50,0,0q�6   $��Architecture��69   $��28��6P   $��Right=�6\   $	��Top=�6x   $��12(�6�   �� 5   
   -   5        $��ClientKernelLabel��5   "   $��0,75,0,0q�6   $��Kernel��6;   $��28��6L   $��Right=�6X   $	��Top=�6t   $��12(�6�   �� 5   
   -	   5        $��ClientPsuedonameLabel��5   #   $��	0,100,0,0q�6   $��Psuedo name��6@   $��28��6V   $��Right=�6b   $	��Top=�6~   $��12(�6�   �� 5   
   -
   5        $��ClientDistributionLabel��5   #   $��	0,125,0,0q�6   $��Distribution��6B   $��28��6Y   $��Right=�6e   $	��Top=�6�   $��12(�6�   �� 5   
   -   5        $$��ClientDistributionVersionLabel��5   #   $��	0,150,0,0q�6   $��Distribution version��6I   $��28��6h   $��Right=�6t   $	��Top=�6�   $��12(�6�   5   	   5       �� 5"      -   5        $��	groupBox3��5"   -   $	��160��6   $��0,0,0,0q�6   $	��Top=�6>   $��0q�6m   $��3��6�   ��6V   Z� .��5#      � .+�5$   
   �� $��12(�6v   $
��Bold"�6�   $��28��6�   $��Right=�6�   $��0,0,0,0q�6�   $	��Top=�6�   ���	#FF666666��6     $? 9LGP.Modules.OrganizationUnitExplorer.Properties.Resources��$ ClientStatus��6�   �� 5%   
   $��20��6   $��Stretch=�6    $��0,15,0,0q�6>   $	��Top=�6P   ���	#FFD6E2F8���	#FFD6E2F86   �� 5&   
   -   5        $��ClientLastSeenLabel��5&   l   $��	Last seen��6   $��12(�6$   $��28��62   $��Right=�6>   $��0,25,0,0q�6Z   $	��Top=�6�   �� 5'   
   -   5        $��ClientBindedLabel��5'   i   $��Binded��6   $��12(�6!   $��28��6/   $��Right=�6;   $��0,50,0,0q�6W   $	��Top=�6�   �� 5(   
   -   5        $��ClientVersionLabel��5(   j   $��Version��6   $��12(�6"   $��28��60   $��Right=�6<   $��0,75,0,0q�6X   $	��Top=�6�   �� 5)   
   -   5        $��ClientIpAddressLabel��5)   n   $��
Ip address��6   $��12(�6%   $��28��63   $��Right=�6?   $��	0,100,0,0q�6[   $	��Top=�6�   �� 5*   
   -   5        $��ClientPortLabel��5*   h   $
��Port��6   $��12(�6   $��28��6-   $��Right=�69   $��	0,125,0,0q�6U   $	��Top=�6   5+   	   5,      5-      5.      