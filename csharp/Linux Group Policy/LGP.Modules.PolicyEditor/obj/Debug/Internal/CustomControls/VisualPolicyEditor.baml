   M S B A M L   `   `   `  ����Y  ULGP.Modules.PolicyEditor, Version=1.0.4162.1340, Culture=neutral, PublicKeyToken=nullU/clr-namespace:LGP.Modules.PolicyEditor.Internal!LGP.Modules.PolicyEditor.Internal  5   Y  I    CLGP.Modules.PolicyEditor.Internal.CustomControls.VisualPolicyEditor   6   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     6\   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6  DInternal/clr-namespace:LGP.Modules.PolicyEditor.Internal     6Y    � 	Resources  5         alt0 �� 
StartPoint �� EndPoint � Color � Offset  alt1  RowStyle �� 
TargetType)  #System.Windows.Controls.DataGridRow �� 	Resources �� Color �� Triggers	 P� Property
 P� Value �� Property �� Value �� RelativeSource �� Path � ToolTip �� AncestorType��%6  &	        &	 �     &	 @    7   7  �� 5      $ 0.5,0&�6'   $ 0.5,1&�6:   .��5      � $ 	#FFFFFFFF��6   $	 0.5Q�6'   � 5      $ 	#FFEEEEEE��6   $	 1.0Q�6'   5      �� 5	      $ 0.5,0&�6'   $ 0.5,1&�6:   .��5
      � $ 	#FFFFFFFF��6   $	 0.5Q�6'   � 5      $ 	#FFDDDDDD��6   $	 1.0Q�6'   5      �� 5      "  6    5      ���� 5   
   $ 0.5,0&�6P   $ 0.5,1&�6c   (��       6   * SystemColors.HighlightBrushKey+).��5      � $ 	#FFFFFFFF��6   $	 0.5Q�6+   � 5      $ 	#FFD6E2F8��6   $	 1.0Q�6+   5      �� 5   
   $ 	#FF000000��6P   (��       6   *$"SystemColors.HighlightTextBrushKey+)6P   5   	    5      P� 5   
   	 �@J�IsMouseOver
 . . �5      ��  � ��$ 	ExtraBold"�6)   �� 5       � �� �	#FF466A8D6)   5      P� 5   
   	 �@ AlternationIndex$
 1��6.   . �5      ��  � ��8   6)   5      P� 5    
   	 �@ AlternationIndex$
 0��6.   . �5!      ��  � ��8  6)   5"      P� 5#   
   	 �@=�HasError
 . . �5$      ��  � ��$ 1q�6.   �� 5%       � ��
 �  ��6*   �� 5&       �@�ToolTip 6&   ��  �� *Self+$2 ,(Validation.Errors).CurrentItem.ErrorContent�5'      P� 5(   
   	 �@=�HasError
 .  . �5)      ��  �@�ToolTip$  ��6&   5*      .�5-      ��  �@ ValidationErrorTemplate 5.   
   �� 5/      .R�50      � $��2,2,2,2q�6    6$   ��  �� *FindAncestor+"  $2 ,(Validation.Errors).CurrentItem.ErrorContent�.+�51      A� $��0��6   ���	#AAAA0000$��14��6=   $��14��6H   $��Center=�6T   $��Center=�6q   �� 52      $��!��6   $��9(�6#   $
��Bold"�60   
�������$��Center=�6U   $��Center=�6r   53      54      56   	   58      5:      .��5;      � .+�5<      &   System.Windows.Controls.DataGrid  -   5        $��	dataGrid1��5<   ^   $��Stretch=�6   $��0,5,0,0q�6M   $��Stretch=�6o      AutoGenerateColumns .  $��1q�6     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .  ���	#FF688CAF   HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����   MinRowHeight$ 20Q�6�     	RowHeight$ 25Q�6�     ColumnHeaderHeight$ 25Q�6�  $��11(�6�   �� AlternationCount$ 2��6     RowDetailsVisibilityMode$ Visible=�6     RowStyle# [ 6>   J� 	AllowDrop . ��6�   �� $ _list� �� ValidatesOnDataErrors .  �� ValidatesOnExceptions .     RowValidationRules  5>      9   3LGP.Modules.PolicyEditor.Internal.RowValidationRule  5?   
   ,  &System.Windows.Controls.ValidationRule!   ValidationStep$! UpdatedValue=�6%   "   Columns" 5A      4  .System.Windows.Controls.DataGridCheckBoxColumn  5B   
   -   5        Selected��,  &System.Windows.Controls.DataGridColumn#   
IsReadOnly# .  1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 5B   !   �� *
IsSelected+% �� Mode$% TwoWay=�& �� NotifyOnSourceUpdated& . '   Header' 6g   0  *Infralution.Localization.Wpf.ResxExtension  (   ResxName$3( -LGP.Modules.PolicyEditor.Properties.Resources��
)   Key$) Selected��6�   4	  .System.Windows.Controls.DataGridComboBoxColumn	  5C   
   -   5        Key��' 5C   !     $3( -LGP.Modules.PolicyEditor.Properties.Resources��$	) Key��* 	  SelectedValueBinding* 6q   �� *
EntryKey+$% TwoWay=�& . 6�   0
  *System.Windows.Controls.DataGridTextColumn
  5D   
   $' Rvalue1��6`   -   5        Rvalue1��$ 5D      �� *	Rvalue1+$% TwoWay=�& . 6q   
  5E   
   $' Rvalue2��6`   -   5        Rvalue2��$ 5E      �� *	Rvalue2+$% TwoWay=�& . 6q   
  5F   
   $' Rvalue3��6`   -   5        Rvalue3��$ 5F      �� *	Rvalue3+$% TwoWay=�& . 6q   
  5G   
   $' Rvalue4��6`   -   5        Rvalue4��$ 5G      �� *	Rvalue4+$% TwoWay=�& . 6q   
  5H   
   $' Rvalue5��6`   -   5        Rvalue5��$ 5H      �� *	Rvalue5+$% TwoWay=�& . 6q   
  5I   
   $' Rvalue6��6`   -	   5        Rvalue6��$ 5I      �� *	Rvalue6+$% TwoWay=�& . 6q   
  5J   
   $' Rvalue7��6`   -
   5        Rvalue7��$ 5J      �� *	Rvalue7+$% TwoWay=�& . 6q   
  5K   
   $' Rvalue8��6`   -   5        Rvalue8��$ 5K      �� *	Rvalue8+$% TwoWay=�& . 6q   
  5L   
   $' Rvalue9��6`   -   5        Rvalue9��$ 5L      �� *	Rvalue9+$% TwoWay=�& . 6q   
  5M   
   $' Rvalue10��6a   -   5        Rvalue10��$ 5M      �� *
Rvalue10+$% TwoWay=�& . 6s   
  5N   
   $' Rvalue11��6a   -   5        Rvalue11��$ 5N      �� *
Rvalue11+$% TwoWay=�& . 6s   +   	CellStyle+ 5P      �� 5Q   
   *  $System.Windows.Controls.DataGridCell"  6   .�5R      ��  � ��$ 5,3,5,0q�6&   �� 5S       � �� 5T      �� 5U      , �� 
TargetType",  6    .R�5V      �� - �� Padding#- z��6   #��z��6=   #��z��6i   #��z��6�   . J� SnapsToDevicePixels. . ._�5W      �� #. z. 6%   5X      5Y      5[      5\      ��5^      �� 5_   
   5a      5b      5c      