   M S B A M L   `   `   `  ����Y  UWuthering Waves comfort vision, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Sub'Wuthering_Waves_comfort_vision.Xaml.Sub  5      9    3Wuthering_Waves_comfort_vision.Xaml.Sub.IconCreator   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Sub     5        N� Title$  IconCreator��5	      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,0&�63   .��5      �  � Color$ #aa4b6b��6    � Offset$ 0Q�6/   � 5      $ #6b6b83��6   $	 0.5Q�6/   � 5      $ #3b8d99��6   $ 1Q�6/   5       � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��300 �6   �� 5      $
��auto �6   .+�5   
   � $��0��6   .+�5      � $��0��6   .+�5      �� $��Center=�6   $��Center=�6:   .+�5       �� -   5         ��Name$ ComboBox_Skill��5        �� SelectedIndex$ 0��67   $
��Auto��6J   $��	0,20,0,10q�6W   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6�   5!      �� 5"      $��/Xaml/Main/Taoqi.png��6   �� 5#      -   5        $��Path to image��5#      ���	#FF9EE2FF$��15(�6L   $��10q�6Z   $
��Bold"�6f   $
}�True��6x    J� SnapsToDevicePixels$
 True��6�   $��3,3,3,3q�6�   5%      5)      5*      � 5-   
   $��1��6   $��
10,0,10,10q�6    .+�5.      �� $	��Top=�6   $��5,0,5,0q�62   .+�50      �� $
��Name��6   $��Center=�6'   $��Center=�6I   $��17(�6m   
�������$
��Bold"�6�   �� 51      -   5        	 ��Name$	 	NameSkill��51      $��Noname��6-   $��15(�6;   $��Center=�6I   $��Center=�6m   6�   �� 53      $��Description��6   $��Center=�6.   $��Center=�6P   $��17(�6t   
�������$
��Bold"�6�   �� 54      -   5        $	 DescriptionSkill��54      $��50��64   $��Nodescription��6@   
 �� TextWrapping$

 Wrap=�6U   $��Center=�6i   $��Center=�6�   $��15(�6�   6�   �� 56      $��
Skill type��6   $��Center=�6-   $��Center=�6O   $��17(�6s   
�������$
��Bold"�6�   �� 58      -   5        $ 	TypeSkill��58      $ 0��6/   $��30��6A   $
��Auto��6M   $��0,0,0,10q�6Z   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6�   5:      �� 5;      $��Skill action��6   $��Center=�6/   $��Center=�6Q   $��17(�6u   
�������$
��Bold"�6�   �� 5=      -   5        $ ActionTypeSkill��5=      $ 0��65   $��30��6G   $
��Auto��6S   $��0,0,0,10q�6`   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6  5?      �� 5A      $��Character variant��6   $��Center=�64   $��Center=�6V   $��17(�6z   
�������$
��Bold"�6�   �� 5B      -   5        $ CharacterVariant��5B      $ 0��66   $��30��6H   $
��Auto��6T   $��0,0,0,10q�6a   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6  5D      �� 5G      $��Duration��6   $��Center=�6+   $��Center=�6M   $��17(�6q   
�������$
��Bold"�6�   �� 5H      -   5        $	 DurationSkill��5H      $	��666��61   $��15(�6<   $��Center=�6J   $��Center=�6n   6�   5K      5M      � 5O   
   $��1��6   $��10,10,10,10q�6!   .+�5P      �� $��Bottom=�6   5T      5U      � 5W   
   $��2��6   $��10,10,10,10q�6!   .+�5X      �� $��Center=�6   .+�5Y      �� $��Global buff��6   
�������$��Center=�6i   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6�    d� 	IsChecked 61   �� *Skill.isGlobal+6�   �� 5Z      $��For next character��6   
�������$��Center=�6z   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   68   �� *Skill.isForNextCharacter+6  �� 5[      $��Cancel if swap��6   
�������$��Center=�6   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   64   �� *#!Skill.isCanselWhenChangeCharacter+6  �� 5\      $��Reuse if spam��6   
�������$��Center=�6q   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   63   �� *Skill.isReusedIfSpam+6  �� 5]      $��Show time number��6   
�������$��Center=�6t   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   66   �� *Skill.isShowCooldown+6  �� 5^      $��Show hotkey��6   
�������$��Center=�6n   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6�    61   �� *Skill.isShowHotkeys+6�   �� 5_      $ ��Show hotkey when press key��6   
�������$��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6@   �� *" Skill.isShowHotkeysWhenHPressKey+6  �� 5`      $��Activate when outro��6   
�������$��Center=�6x   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6	   69   �� *Skill.isOutroActivate+6	  �� 5a      $��Activate when intro��6   
�������$��Center=�6x   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6	   69   �� *Skill.isIntroActivate+6	  �� 5b      $��Activate when resonance��6   
�������$��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6=   �� *Skill.isResonanceActivate+6  �� 5c      $��Activate when echo��6   
�������$��Center=�6v   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   68   �� *Skill.isEchoActivate+6  �� 5d      $��Activate when ultimate��6   
�������$��Center=�6~   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6<   �� *Skill.isUltimateActivate+6  �� 5f      $��Hotkey��6   
�������$��18(�6<   �� 5g      -	   5        $	 HotkeyTextBox��5g      $��xxxxxx��61   $��18(�6?   $��Center=�6M   $��Center=�6p   $��Center=�6�   �� 5h      -
   5        $��Create icon��5h      ���	#FF9EFFBF$��15(�6F   $
��Bold"�6T   $
}�True��6f   $
 True��6�   $��3,3,3,3q�6�   5k      5l      5o      5p      