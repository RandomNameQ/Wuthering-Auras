   M S B A M L   `   `   `  ����Y  UWuthering Waves comfort vision, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main(Wuthering_Waves_comfort_vision.Xaml.Main  5      :    4Wuthering_Waves_comfort_vision.Xaml.Main.BuffOverlay   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main     5        N� Title$  BuffOverlay��5	      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,0&�63   .��5      �  � Color$ #aa4b6b��6    � Offset$ 0Q�6/   � 5      $ #6b6b83��6   $	 0.5Q�6/   � 5      $ #3b8d99��6   $ 1Q�6/   5       � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    .+�5#      � $��0��6   $��Center=�6    $
��Left=�6;    5$      �� 5%      $
��Auto �6'   �� 5&      $
��Auto �6'   �� 5'      $
��Auto �6'   .+�5+      �� -   5         ��Name$ FirstHeroButton��5+      
 � Tag$ 	FirstHero��62   $	��100��6B   $	��100��6N   $��
20,20,0,20q�6[   $��Center=�6o   $��0��6�   ��5,      �� 5-      	 �� 
TargetType"	 ��6*   .R�5.      �� $��0q�6%   
������ ._�5/   "   A� 
 A�Name$
 ButtonEllipse��6*   ��50   &   �� 51   *    �� ImageSource$8 2pack://application:,,,/Assets/Characters/Rover.png��65    j� Stretch$ UniformToFill=�6v   53   #   54      55      57      58      � 5:      $��1��6   $��Center=�6    $
��Left=�6;    5;      �� 5<      $
��Auto �6'   �� 5=      $
��Auto �6'   �� 5>      $
��Auto �6'   .+�5B      �� -   5        $ SecondHeroButton��5B      $ 
SecondHero��63   $	��100��6D   $	��100��6P   $��
20,20,0,20q�6]   $��Center=�6q   $��0��6�   ��5C      �� 5D      "	 ��6*   .R�5E      �� $��0q�6%   
������ ._�5F   "   A� $
 ButtonEllipse��6*   ��5G   &   �� 5H   *   $8 2pack://application:,,,/Assets/Characters/Rover.png��65   $ UniformToFill=�6v   5J   #   5K      5L      5N      5Q      � 5S      $��2��6   $��Center=�6    $
��Left=�6;    5T      �� 5U      $
��Auto �6'   �� 5V      $
��Auto �6'   �� 5W      $
��Auto �6'   .+�5[      �� -   5        $ ThirdHeroButton��5[      $ 	ThirdHero��62   $	��100��6B   $	��100��6N   $��
20,20,0,20q�6[   $��Center=�6o   $��0��6�   ��5\      �� 5]      "	 ��6*   .R�5^      �� $��0q�6%   
������ ._�5_   "   A� $
 ButtonEllipse��6*   ��5`   &   �� 5b   *   $8 2pack://application:,,,/Assets/Characters/Rover.png��65   $ UniformToFill=�6v   5d   #   5e      5f      5h      5k      5l      � 5o   
   $��1��6   $��10,10,10,10q�6    .+�5p      �� $	��Top=�6   .+�5q      �� -   5         ��Name$ HeroName��5q      $��HeroName��6*   
�������$��Center=�6P   $��Center=�6k   $��25(�6�   �� 5r      -   5         ��Name$ AbilityComboBox��5r      $��Center=�64   $��0,10,0,0q�6O    �� SelectedIndex$ 0��6�   $��Center=�6�   .@�5s      �� $��intro��6#   �� 5t      $
��echo��6#   �� 5u      $��	resonance��6#   �� 5v      $��ultimate��6#   �� 5w      $��outro��6#   �� 5x      $��element��6#   �� 5y      $��inherit��6#   �� 5z      $��weapon��6#   �� 5{      $��cooldownIntro��6#   �� 5|      $��cooldownEcho��6#   �� 5}      $��cooldownResonance��6#   �� 5~      $��cooldownUltimate��6#   �� 5      $��cooldownOutro��6#   �� 5�      $��cooldownElement��6#   �� 5�      $��cooldownInherit��6#   �� 5�      $��cooldownWeapon��6#   5�      �� 5�      -   5         ��Name$ NeedActivateCheckBox��5�   >    � ToolTip$ Cheked: icon will render��6   $��
Show image��6\   $��18(�6q   $��Center=�6   
�������
�������6�   �� 5�      -   5        $ GlobalBuffCheckBox��5�   3   $ Does not work��6   $��Global buff��6O   $��18(�6f   $��Center=�6t   
�������
�������6�   �� 5�      -   5        $ BuffNextCharacterCheckBox��5�   3   $ Does not work��6   $��Buff next character��6V   $��18(�6t   $��Center=�6�   
�������
�������6�   �� 5�      -	   5        $' !CancelBuffIfSwapCharacterCheckBox��5�   3   $ Does not work��6   $#��Cancel buff if swap character��6^   $��18(�6�   $��Center=�6�   
�������
�������6�   �� 5�      -
   5        $ ShowNumberCheckBox��5�   A   $! Cheked: show timer cooldown��6   $��Show number��6]    d� 	IsChecked$
 TrueY�6s   $��18(�6�   $��Center=�6�   
�������
�������6�   �� 5�      -   5        $ 	SounAlert��5�   3   $ Does not work��6   $��Sound when end��6F   $��18(�6a   $��Center=�6o   
�������
�������6�   �� 5�      -   5        $ DoubleTapBox��5�   3   $ Does not work��6   $��Cansel image double tap��6I   $��18(�6l   $��Center=�6z   
�������
�������6�   �� 5�      -   5        $ ReuseBuffInSpamBox��5�   �   $b \Cheked: if a second has not passed after pressing, then press again and the icon will update��6   $��Reuse if spam��6�   $
 TrueY�6�   $��18(�6�   $��Center=�6�   
�������
�������6  �� 5�      $��Duration��6   
�������$��18(�6>   �� 5�      -   5         ��Name$ DurationTextBox��5�      $	��9.9��63   $��18(�6>   $��Center=�6L   6n   �� 5�      $��Hotkey��6   
�������$��18(�6<   �� 5�      -   5        $ HotkeyTextBox��5�      $��Q��61   $��18(�6:   $��Center=�6H   6j   �� 5�      $��
Double Tap��6   
�������$��18(�6@   �� 5�      -   5        $ HotkeyDoubleTextBox��5�      $��Q��67   $��18(�6@   $��Center=�6N   6p   �� 5�      -   5        $��Path to image��5�      61   5�      5�      � 5�   
   $��1��6   $��10,10,10,10q�6!   .+�5�      �� $��Bottom=�6   5�      5�      � 5�   
   $��2��6   $��10,10,10,10q�6!   .+�5�      �� $��Center=�6   .+�5�      �� $��Width image��6   
�������$��18(�6A   �� 5�      -   5        $ 
widthImage��5�      $��64��5�      $��18(�6   $��Center=�5�      5�      �� 5�      $��Height image��6   
�������$��18(�6B   �� 5�      -   5        $ heightImage��5�      $��64��5�      $��18(�6   $��Center=�5�      5�      �� 5�      -   5        $
��SAVE��5�      
���   ����	#FF3FFF6E$��20(�6n   �� 5�      -   5        $��Show images��5�      
���   �$��18(�6^   �� 5�      -   5        $��Hide images��5�      
���   �$��18(�6^   5�      5�      5�      5�      