   M S B A M L   `   `   `  ����Y  UWuthering Waves comfort vision, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Wuthering_Waves_comfort_vision.View#Wuthering_Waves_comfort_vision.View  5   	   /    )Wuthering_Waves_comfort_vision.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Clocal1clr-namespace:Wuthering_Waves_comfort_vision.View     5   	     � Title$  Wuthering Auras��5   	   $	��700��5	   	   $	��920��6    � 
ResizeMode$ CanResizeWithGrip=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   
������ .��5      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,0&�63   .��5      �  � Color$ #aa4b6b��6    � Offset$ 0Q�6/   � 5      $ #6b6b83��6   $	 0.5Q�6/   � 5      $ #3b8d99��6   $ 1Q�6/   5      .+�5    
   �� 
���   �$��0 0 1 0q�6%   $��0��6?   $��	10,56,0,0q�6O   $	��Top=�6b   ._�5!      �� 
������ $
��Left=�62   .+�5"      �� -   5        $
��Main��5"      $��25(�6E   
�������$��Arial)�6f   ��5#      �� 5$      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5%      � $ 	#FFA64D6C��6+   � 5&      $ 	#FF3E8B98��6+   $ 1Q�6=   5'      5)      �� 5*      -   5        $
��Team��5*      $��25(�6L   
�������$��Center=�6m   $��Arial)�6�   ��5+      �� 5,      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5-      � $ 	#FFA64D6C��6+   � 5.      $ 	#FF3E8B98��6+   $ 1Q�6=   5/      51      �� 52      -   5        $��Buffs��52      $��23(�6I   
�������$��Center=�6j   $��Arial)�6�   ��53      �� 54      $ 0.5,1&�6.   $ 0.5,0&�6?   .��55      � $ 	#FFA64D6C��6+   � 56      $ 	#FF3E8B98��6+   $ 1Q�6=   57      59      �� 5;      -   5        $��OverlayArea��5;      $��23(�6S   
�������$��Center=�6t   $��Arial)�6�   ��5<      �� 5=      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5>      � $ 	#FFA64D6C��6+   � 5?      $ 	#FF3E8B98��6+   $ 1Q�6=   5@      5B      �� 5C      -   5        $	��QOL��5C      $��25(�6G   
�������$��Center=�6h   $��Arial)�6�   ��5D      �� 5E      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5F      � $ 	#FFA64D6C��6+   � 5G      $ 	#FF3E8B98��6+   $ 1Q�6=   5H      5J      �� 5K      -   5        $��Skills��5K      $��25(�6M   
�������$��Center=�6n   $��Arial)�6�   ��5L      �� 5M      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5N      � $ 	#FFA64D6C��6+   � 5O      $ 	#FF3E8B98��6+   $ 1Q�6=   5P      5R      5U      5V      �� 5X   
   
���   �$��0 0 1 0q�6%   $��0��6?   $��
10,56,0,10q�6O   $��Bottom=�6c   ._�5Y      �� 
������ $
��Left=�62   .+�5Z      �� $��Settings��6   $��20(�6-   
�������$��Arial)�6N   ��5[      �� 5\      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5]      � $ 	#FFA64D6C��6+   � 5^      $ 	#FF3E8B98��6+   $ 1Q�6=   5_      5a      �� 5b      $��Support��6   $��20(�6,   
�������$��Center=�6M   $��Arial)�6h   ��5c      �� 5d      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5e      � $ 	#FFA64D6C��6+   � 5f      $ 	#FF3E8B98��6+   $ 1Q�6=   5g      5i      �� 5j      $��Discord��6   $��20(�6,   
�������$��Center=�6M   $��Arial)�6h   ��5k      �� 5l      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5m      � $ 	#FFA64D6C��6+   � 5n      $ 	#FF3E8B98��6+   $ 1Q�6=   5o      5q      5t      5u      � 5z   
   $��1��6   $��40q�6   .+�5{      ��  �� Orientation$ Vertical=�6   $��Center=�60   .+�5|       � -   5        	  �Name$	 ContentFrame��5|      
  � NavigationUIVisibility$
 Hidden=�6.   �� 5}      -   5         ��Name$ CheckboxMain_DetectHotkey��5}      $��Detect hotkey��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -	   5        $) #CheckboxMain_DetectHotkey_Wuthering��5�      $3��-Detect hotkey if Wuthering Waves window focus��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -
   5        $' !CheckboxMain_RenderOverlayIfFocus��5�      $,��&Render if Wuthering Waves window focus��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -   5        $  CheckboxMain_RenderOverlay��5�   -   $��0,20,0,0q�6   $��Render character overlay��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -   5        $ CheckboxMain_RenderBuffs��5�      $��Render buffs overlay��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -   5        $, &CheckboxMain_SwitchMoveImagePosibility��5�      $#��Can change size position icon��5�       � ToolTip$. (Checked: u can move and change size icon��5�      $
��Left=�5�      $��Center=�5�      
�������
�������$��30(�5�      $��Center=�5�      $��Center=�5�      �� 5�      -   5         ��Name$ ButtonAddNewIcon��5�      $��Add new icon��63   ���	#FF9EE2FF$��20q�6a   $��15(�6m   $
��Bold"�6{   $
}�True��6�    J� SnapsToDevicePixels$
 True��6�   $��3,3,3,3q�6�   5�      5�      5�      5�      