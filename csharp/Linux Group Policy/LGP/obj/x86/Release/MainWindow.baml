   M S B A M L   `   `   `  ����   Linux Group Policy    LGP.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  LGPMainWindow�� � Title$ 
MainWindow��$	��500��$
��1000��$	��300��$	��700�� � WindowStartupLocation$ CenterScreen=� J� Opacity$ 1Q� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ CanResizeWithGrip=���Z� .��� -    �Name$ outer�� � RowDefinitions �� $��* �	 � ColumnDefinitions	 �� $
��250* �.+��� 
���   �$��1q�$��0q�$��5��
 �� Padding$
 5q� �� CornerRadius$ 3������  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.���  � Color$ 	#FFFFFFFF�� � Offset$	 0.1Q�� $ 	#EEFFFFFF��$	 0.2Q�� $ 	#DDFFFFFF��$	 0.3Q�� $ 	#CCFFFFFF��$	 0.4Q�� $ 	#BBFFFFFF��$	 0.5Q�� $ 	#BBFFFFFF��$	 0.6Q�� $ 	#CCFFFFFF��$	 0.7Q�� $ 	#DDFFFFFF��$	 0.8Q�� $ 	#FFFFFFFF��$	 0.9Q�� $ 	#FFFFFFFF��$	 1.0Q�._�� -   $
 main��$��1��$��1�� �� $��25 �$��25���� $��1 �$��1���� $
��395* ��� $��1 �$��1���� $��30 �$��30��	 �� $
��220* �$	��200���� $
��500* �$	��250���� $	��80* �$��80��.+�o� -   $��menu1�� o� 
IsMainMenu . $��0��$��0��$��0q�$
��Left=� K� ZIndex$	 200��
������ �� -    ��Name$ 
rectangle1��$��1��$��3�����	#00E3EFFF$��5,0,5,0q��� -   $ 
rectangle2��$��3��$��3��$��5,0,5,0q����	#00E3EFFF$ 1���� -    ��Name$ 
statusBar1��$��4��$��3��$��0q�$��5,5,0,0q�$ 1��
������ ���� .R��  �� $��* �	 �� $��4* ��� $
��Auto ��� $��* ��� $��* �.@��� .���� Ready�� $��1��.���  � Value$ 30Q�$��80��$��18���� $��2��.���� Set�� $��3��.���� Go!�� -   $��dragBar��$��1��$��0��$��Stretch=�$��0q�$��Stretch=��� -	   $��Right=�$��	0,10,65,0q�$��17��$��17��$	��Top=�$	 0.3Q����� $	 0,1&�$	 0,0&�.��� $ 	#FFF3F3F3��$ 0Q�� $ 	#FFEBEBEB��� $ 	#FFDDDDDD��$ 0.806Q�� $ 	#FF3F76BF��$ 1Q��� -
   $��Right=�$��	0,10,40,0q�$��17��$��17��$	��Top=�$	 0.3Q����� $	 0,1&�$	 0,0&�.��� $ 	#FFF3F3F3��$ 0Q�� $ 	#FFEBEBEB��� $ 	#FFDDDDDD��$ 0.806Q�� $ 	#FF3F76BF��$ 1Q��� -   $��Right=�$��	0,10,15,0q�$��17��$��17��$	��Top=�$	 0.3Q����� $	 0,1&�$	 0,0&�.��� $ 	#FFF3F3F3��$ 0Q�� $ 	#FFEBEBEB��� $ 	#FFDDDDDD��$ 0.806Q�� $ 	#FF3F76BF��$ 1Q��� -   $��bgImage��$
��Fill=�$	 0.3Q�$	��128��$	��128��$��0,0,-18,-28q�$��Bottom=�$��Right=�$	 200��