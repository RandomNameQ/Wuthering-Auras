   M S B A M L   `   `   `  ����Y  UWuthering Waves comfort vision, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main(Wuthering_Waves_comfort_vision.Xaml.Main  5      ;    5Wuthering_Waves_comfort_vision.Xaml.Main.SkillLibrary   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:Wuthering_Waves_comfort_vision.Xaml.Main     5        N� Title$  SkillLibrary��5	      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,0&�63   .��5      �  � Color$ #aa4b6b��6    � Offset$ 0Q�6/   � 5      $ #6b6b83��6   $	 0.5Q�6/   � 5      $ #3b8d99��6   $ 1Q�6/   5       � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��300 �6   �� 5      $
��auto �6   .+�5   
   � $��0��6   .+�5      � $��0��6   .+�5      �� $	��Top=�6   $��5,10,5,0q�65   $	��400��6G   .+�5!      �� -   5        $��Add new skill��5!      ���	#FF9EE2FF$��15(�6L   $
��Bold"�6Z   $
}�True��6l    J� SnapsToDevicePixels$
 True��6�   $��3,3,3,3q�6�   �� 5%      -   5         ��Name$ ComboBox_Skill��5%       �� SelectedIndex$ 0��67   $	��150��6J   $��	0,20,0,10q�6V   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6�   5&      �� 5'      $��/Xaml/Main/Taoqi.png��6   �� 5(      -   5        $��	0,100,0,0q�5(      $��Delete this skill��60   ���	#FF9A0000$��10(�6c   $
}�True��6q   $
 True��6�   $��3,3,3,3q�6�   
�������6�   5*      5.      5/      � 52   
   $��1��6   $��
10,0,10,10q�6    .+�53      �� $	��Top=�6   $��5,0,5,0q�62   .+�55      �� $
��Name��6   $��Center=�6'   $��Center=�6I   $��17(�6m   
�������$
��Bold"�6�   �� 56      -   5        	 ��Name$	 	NameSkill��56      $��Noname��6-   $��15(�6;   $��Center=�6I   $��Center=�6m   6�   �� 58      $��Description��6   $��Center=�6.   $��Center=�6P   $��17(�6t   
�������$
��Bold"�6�   �� 59      -   5        $	 DescriptionSkill��59      $��50��64   $��Nodescription��6@   
 �� TextWrapping$

 Wrap=�6U   $��Center=�6i   $��Center=�6�   $��15(�6�   6�   �� 5;      $��
Skill type��6   $��Center=�6-   $��Center=�6O   $��17(�6s   
�������$
��Bold"�6�   �� 5=      -   5        $ 	TypeSkill��5=      $ 0��6/   $��30��6A   $
��Auto��6M   $��0,0,0,10q�6Z   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6�   5?      �� 5@      $��Skill action��6   $��Center=�6/   $��Center=�6Q   $��17(�6u   
�������$
��Bold"�6�   �� 5B      -   5        $ ActionTypeSkill��5B      $ 0��65   $��30��6G   $
��Auto��6S   $��0,0,0,10q�6`   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6  5D      �� 5F      $��Character variant��6   $��Center=�64   $��Center=�6V   $��17(�6z   
�������$
��Bold"�6�   �� 5G      -   5        $ CharacterVariant��5G      $ 0��66   $��30��6H   $
��Auto��6T   $��0,0,0,10q�6a   $��Center=�6�   $��Center=�6�   $��15(�6�   $
��Bold"�6  5I      �� 5L      $��Duration��6   $��Center=�6+   $��Center=�6M   $��17(�6q   
�������$
��Bold"�6�   �� 5M      -	   5        $	 DurationSkill��5M      $	��666��61   $��15(�6<   $��Center=�6J   $��Center=�6n   6�   �� 5P      -
   5        $��Path to image��5P      ���	#FF9EE2FF$��15(�6H   $��10q�6V   $
��Bold"�6b   $
}�True��6t   $
 True��6�   $��3,3,3,3q�6�   5Q      5S      � 5U   
   $��1��6   $��10,10,10,10q�6!   .+�5V      �� $��Bottom=�6   5Z      5[      � 5]   
   $��2��6   $��10,10,10,10q�6!   .+�5^      �� $��Center=�6   .+�5_      �� $��Global buff��6   
�������$��Center=�6i   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6�    d� 	IsChecked 61   �� *Skill.isGlobal+6�   �� 5`      $��For next character��6   
�������$��Center=�6z   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   68   �� *Skill.isForNextCharacter+6  �� 5a      $��Cancel if swap��6   
�������$��Center=�6   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   64   �� *#!Skill.isCanselWhenChangeCharacter+6  �� 5b      $��Reuse if spam��6   
�������$��Center=�6q   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   63   �� *Skill.isReusedIfSpam+6  �� 5c      $��Show time number��6   
�������$��Center=�6t   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   66   �� *Skill.isShowCooldown+6  �� 5d      $��Show hotkey��6   
�������$��Center=�6n   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6�    61   �� *Skill.isShowHotkeys+6�   �� 5e      $ ��Show hotkey when press key��6   
�������$��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6@   �� *" Skill.isShowHotkeysWhenHPressKey+6  �� 5f      $��Activate when outro��6   
�������$��Center=�6x   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6	   69   �� *Skill.isOutroActivate+6	  �� 5g      $��Activate when intro��6   
�������$��Center=�6x   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6	   69   �� *Skill.isIntroActivate+6	  �� 5h      $��Activate when resonance��6   
�������$��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6=   �� *Skill.isResonanceActivate+6  �� 5i      $��Activate when echo��6   
�������$��Center=�6v   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   68   �� *Skill.isEchoActivate+6  �� 5j      $��Activate when ultimate��6   
�������$��Center=�6~   $��Center=�6�   $��Center=�6�   $��Center=�6�   
�������$��16(�6   6<   �� *Skill.isUltimateActivate+6  �� 5m      $��Hotkey��6   
�������$��18(�6<   $��Center=�6J   $��Center=�6g   �� 5n      -   5        $	 HotkeyTextBox��5n      $��Press a key��61   $��18(�6D   $��Center=�6R   $��Center=�6t   $��Center=�6�   6�   �� 5o      -   5        $��
Save skill��5o      ���	#FF9EFFBF$��15(�6E   $
��Bold"�6S   $
}�True��6e   $
 True��6�   $��3,3,3,3q�6�   5r      5s      5v      5w      