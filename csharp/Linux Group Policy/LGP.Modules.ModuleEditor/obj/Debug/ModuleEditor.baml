   M S B A M L   `   `   `  ����  LGP.Modules.ModuleEditor+    %LGP.Modules.ModuleEditor.ModuleEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     6>   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   @d2http://schemas.microsoft.com/expression/blend/2008     6�   ` \ICSharpCode.AvalonEdit, Version=4.1.0.8000, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  6;  -   5        $	��250��5   �  .��5      �   � RowDefinitions  5      �� 5      $��45 �6   �� 5      $��1 �6   �� 5      $
��300* �6   �� 5      $��1 �6   �� 5      $��44 �6    � ColumnDefinitions 5
      �� 5      $
��400* �6   $	��250��6&   �� 5      -   5         �Name$ VerticalSeparator1��5   #   $��2 �6   6#   �� 5      -   5        $ DetailsColumn��5   %   $	��245 �6   $	��180��6:   .+�5      � $��0��6   $��0��6   $��	25,5,25,0q�6(    5      �� 5   
   $
��200* �6   �� 5   
   $	��260 �6   .+�5      o� $��0,8,0,7q�6   $
��Left=�6   
�������
�������$��30��6_   $��Center=�6k   $��2,2,10,2q�6�   .@�5   
   m� $��0q�6   $��2q�6    m� Icon 5      �� 5      -   5        $��ModuleImage��5   F   $��20��6   $
��Left=�6    $��0q�6;   $
��Fill=�6Y   $	��Top=�6h   $��20��6�   ��5      �� 5       �� Orientation$ 
Horizontal=�6   $��0q�62   .+�5      �� -   5        $��moduleNameLabel��5      $��16(�6-   ���	#FF9ABFE5$
��Bold"�6R   $��0,-2,5,2q�6d   $
��Left=�6v   $��0q�6�   .��6�   Module Name6�   5       @�5"      m� 5#      -   5        $��RenameMenuItem��5#   F   
���   ���6\   0  *Infralution.Localization.Wpf.ResxExtension     ResxName$3 -LGP.Modules.ModuleEditor.Properties.Resources��
   Key$ Rename��m� 5$      -   5        $��DeleteMenuItem��5$   F   
���   ���6\     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Delete��5&      5'   	   ]� 5)      $��1��6   .+�5*   
   a� $	��Top=�6   $��30��6,   
�������.+�5+      c� -   5        $	��Top=�5+       `� IsLocked . 
������� � 	Resources 5,      	 �� 
TargetType
 �� Triggers �� Binding �� RelativeSource �� AncestorType �� AncestorLevel �� Path �� Value �� Property �� Value��%�   '��       �� 5-      "	 ��6   
 5.      �� 5/       6    ��  �� " ��$ 1��$ 	IsEnabled�	 False6�   .a�50      ��  �@J�Opacity$
 0.30Q�60   51      53      54      .@�55      �� -	   5        $��
SaveButton��55       � ToolTip 6A     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Save��.��56      �� -
   5        $��	ImageSave��56      $��16��6'   $��16��63   57      �� 58      �� 59      $	��Cut��6    6#     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$	 Cut��.��5:      �� -   5        $��ImageCut��5:      $��16��6&   $��16��62   5;      �� 5<      $
��Copy��6    6$     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Copy��.��5=      �� -   5        $��	ImageCopy��5=      $��16��6'   $��16��63   5>      �� 5?      $��Paste��6    6%     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Paste��.��5@      �� -   5        $��
ImagePaste��5@      $��16��6(   $��16��64   5A      �� 5B      $��Delete��6    6&     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Delete��.��5C      �� -   5        $��ImageDelete��5C      $��16��6)   $��16��65   5D      �� 5E      �� 5F      $
��Undo��6    6$     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Undo��.��5G      �� -   5        $��	ImageUndo��5G      $��16��6'   $��16��63   5H      �� 5I      $
��Redo��6    6$     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Redo��.��5J      �� -   5        $��	ImageRedo��5J      $��16��6'   $��16��63   5K      �� 5L      �� 5M       d� 	IsChecked 6   ��  �� ElementName$ textEditor1��$ WordWrap� 6S     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ WordWrap��.��5N      �� -   5        $��	ImageWrap��5N      $��16��6'   $��16��63   5O      �� 5P       6   �� $ textEditor1��$ ShowLineNumbers� 6Z     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ LineNumbers��.��5Q      �� $��16��6   $��16��6&    �� TextAlignment$ Center=�61   #6K   5R      �� 5S      -   5        $ FalseY�5S       6o     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ 	SplitView��.��5T      �� -   5        $��SplitViewImage��5T      $��16��6,   $��16��67   5U      5V      5W      5X   	   5Z      �� 5]      $��1��6   $��Stretch=�6   $��Stretch=�68   ���	#FFD6E2F8���	#FFD6E2F8$��	15,0,15,0q�6�   � 5_      -   5        $��
editorGrid��5_   -   $��2��6   $��25,15,25,15q�6   $��Stretch=�6?   $��Stretch=�6]    5`      �� 5a   
   -   5        $ ColumnEditor1��5a   (   $
��124* �6   6(   �� 5b   
   -   5        $ editorSeparator��5b   %   $��2 �6   6%   �� 5c   
   -   5        $ ColumnEditor2��5c   %   $��0 �6   6%   .+�5e      '  !ICSharpCode.AvalonEdit.TextEditor  -   5        $��textEditor1��5e      $��Consolas)�61   $
��10pt(�6G      SyntaxHighlightingM  GICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter$ C# 6W   $��Normal$�6o   $��
ExtraLight"�6�   $��0��6�   $   HorizontalScrollBarVisibility$
 Auto=�6�   "   VerticalScrollBarVisibility$
 Auto=�6�   $��0q�6�   $��0q�6�      Text. 6	   � ContextMenu 5f   
   �� 5g      .@�5h      m� -   5        ��5h        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Copy��6h   m� 5i      -   5        ��5i        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$	 Cut��6g   m� 5j      -   5        ��5j        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Paste��6i   5k      . 5l   -   5m   	   �� 5o      $��1��6   $��Stretch=�6%   $��Stretch=�6C   ���	#FFD6E2F86_     5q      -   5        $��textEditor2��5q      $��Consolas)�61   $
��10pt(�6G   $ C# 6W   $��Normal$�6o   $��
ExtraLight"�6�   $��2��6�   $
 Auto=�6�   $
 Auto=�6�   $��0q�6�   $��0q�6�   . 6	   5r   
   �� 5s      .@�5t      m� -   5        ��5t        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$	 Cut��6g   m� 5u      -   5        ��5u        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$
 Copy��6h   m� 5v      -   5        ��5v        $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Paste��6i   5w      . 5x   -   5y   	   5z      �� 5|      $��3��6   $��Stretch=�6   $��Stretch=�68   ���	#FFD6E2F8���	#FFD6E2F8$��	15,0,15,0q�6�   � 5~      $��4��6   $��	25,8,25,8q�6   $��Stretch=�6+   $��Stretch=�6I    5      �� 5�   
   $��60 �6   �� 5�   
   $��60 �6   �� 5�   
   $��50 �6   �� 5�   
   $��50 �6   �� 5�   
   $	��30* �6   �� 5�   
   $��35 �6   �� 5�   
   $��40 �6   �� 5�   
   $��40 �6   �� 5�   
   $��40 �6   �� 5�   
   $��40 �6   �� 5�   
   $��50 �6   .+�5�      �� $��0��6   $��28��6   $
��Left=�6*   $��5,0,0,0q�6E   $��Bottom=�6V   $��12(�6�   ��6q     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Lenght��6�   �� 5�      -    5        $��
FileLength��5�   V   $��1��6   $��28��6   $
��Left=�6*   $��0,0,0,0q�6E   $��Bottom=�6h   $��0��6�   $��12(�6�   �� 5�      $��2��6   $��28��6   $
��Left=�6*   $��5,0,0,0q�6E   $��Bottom=�6V   $��12(�6�   ��6q     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Lines��6�   �� 5�      -!   5        $��	FileLines��5�   V   $��3��6   $��28��6   $
��Left=�6*   $��0,0,0,0q�6E   $��Bottom=�6g   $��0��6�   $��12(�6�   �� 5�      $��5��6   $��28��6   $
��Left=�6*   $��5,0,0,0q�6E   $��Bottom=�6V   $��12(�6�   ��6q     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$ Ln��6�   �� 5�      -"   5        $��FileLine��5�   V   $��6��6   $��28��6   $
��Left=�6*   $��0,0,0,0q�6E   $��Bottom=�6f   $��0��6�   $��12(�6�   �� 5�      $��7��6   $��28��6   $
��Left=�6*   $��5,0,0,0q�6E   $��Bottom=�6V   $��12(�6�   ��6q     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$	 Col��6�   �� 5�      -#   5        $��
FileColumn��5�   V   $��8��6   $��28��6   $
��Left=�6*   $��0,0,0,0q�6E   $��Bottom=�6h   $��0��6�   $��12(�6�   �� 5�      $��9��6   $��28��6   $
��Left=�6*   $��5,0,0,0q�6E   $��Bottom=�6V   $��12(�6�   ��6q     $3 -LGP.Modules.ModuleEditor.Properties.Resources��$	 Sel��6�   �� 5�      -$   5        $��FileSelection��5�   W   $��10��6   $��28��6   $
��Left=�6+   $��0,0,0,0q�6F   $��Bottom=�6l   $��0��6�   $��12(�6�   5�      �� 5�      $��1��6   $��5��6#   $��Stretch=�64   $��Stretch=�6R   ���	#FFD6E2F86n   �� 5�      -%   5        $��scrollViewer��5�   1   $��Stretch=�6   $��Stretch=�6E   $��0q�6a   $��2��6m   $��5��6}   $
��Auto=�6�   .��5�      �� -&   5        $��ModuleViewBorderContainer��5�   �   
������ $��0q�6)   $��Stretch=�6=   $��0q�6[   $��Stretch=�6f   6�   5�      �� 5�      -'   5        $��ModuleLabelEditBox��5�      $y�Hidden=�6(   $	��200��6<   $��	61,13,5,8q�6c   $
��Left=�6v   6�   5�      5�      