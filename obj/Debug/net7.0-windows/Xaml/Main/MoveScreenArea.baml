   M S B A M L   `   `   `  ����Y  UWuthering Waves comfort vision, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main(Wuthering_Waves_comfort_vision.Xaml.Main  5      =    7Wuthering_Waves_comfort_vision.Xaml.Main.MoveScreenArea   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main     5        N� Title$  MoveScreenArea��5	      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,0&�63   .��5      �  � Color$ #aa4b6b��6    � Offset$ 0Q�6/   � 5      $ #6b6b83��6   $	 0.5Q�6/   � 5      $ #3b8d99��6   $ 1Q�6/   5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��500 �6   �� 5      $��* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    5&      � 5)   
   $��1��6   $��10,10,10,10q�6    .+�5*      �� $	��Top=�6   .+�5+      �� -   5         ��Name$ HeroName��5+      $��Move Screen Area��6*   
�������$��Center=�6X   $��Center=�6s   $��25(�6�   �� 5,      -   5         ��Name$ Combobox��5,      $��Center=�6-   $��0,10,0,0q�6H   	 �� SelectedIndex$	 0��6�   $��Center=�6�   .@�5-      �� 

 � Tag$
 first��6#   $��First Character��6/   �� 5.      $
 second��6#   $��Second Character��60   �� 5/      $
 third��6#   $��Third Character��6/   �� 50      $
 firstUltimate��6#   $��Copy first ultimate��67   �� 51      $
 secondUltimate��6#   $��Copy second ultimate��68   �� 52      $
 thirdUltimate��6#   $��Copy third ultimate��67   �� 54      $
 firstConcert��6#   $��Copy first Concert��66   �� 55      $
 secondConcert��6#   $��Copy second Concert��67   �� 56      $
 thirdConcert��6#   $��Copy third Concert��66   �� 58      $
 
firstForte��6#   $��Copy first Forte��64   �� 59      $
 secondForte��6#   $��Copy second Forte��65   �� 5:      $
 
thirdForte��6#   $��Copy third Forte��64   5;      �� 5=      -   5         ��Name$ EnableOverlay��5=      $��
20,20,20,0q�5>      $��Enable overlay��5?      $��18(�5@      $��Center=�5A      
�������
�������5E      �� 5H      -   5        $��
20,20,20,0q�5H      $��Orignal area��6-   ���	#FFFFB5B56`   �� 5J      -   5        $��	20,0,20,0q�5J      $��Overlay area��6,   ���	#FFB7BEF36^   �� 5M      $��Center=�6   .+�5N      �� $��Width image��6   
�������$��18(�6E   �� 5O      -   5         ��Name$ 
widthImage��5O      $��64��5P      $��18(�6   $��Center=�5Q      5R      �� 5S      $��Height image��6   
�������$��18(�6F   �� 5T      -   5        $ heightImage��5T      $��64��5U      $��18(�6   $��Center=�5V      5W      5[      �� 5\      -   5        $��
20,20,20,0q�5\      $
��Hide��6-   
�������6P   �� 5]      -	   5        $��	20,0,20,0q�5]      $
��Save��6,   ���	#FF8BFFB76O   5^      5`      � 5b   
   $��1��6   $��10,10,10,10q�6!   .+�5c      �� $��Bottom=�6   5h      5i      � 5k   
   $��2��6   $��10,10,10,10q�6!   .+�5l      �� $��Center=�6   5o      5p      5s      5t      