   M S B A M L   `   `   `  ����R  NBasic Financial Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Basic_Financial_TrackerBasic_Financial_Tracker  5   	   #    Basic_Financial_Tracker.Popup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Basic_Financial_Tracker     5   	   -   5          �Name$  PopupWindow��5   	    � Title$ Edit Original Balance��5	   	   $	��150��6'   $	��300��64   
������� � Icon$ GoldenCoin.ico��6W    � 
ResizeMode$ NoResize=�6m   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $��27 �6   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� -   5         ��Name$ 
popupLabel��5      $��0��6   $��Current ��61   $
��Left=�6D   $	��Top=�6_   �� 5   
   -   5         ��Name$ popupTextBox��5      $��1��6   $��Stretch=�65    �� TextWrapping$
 Wrap=�6S   $��Insert New Balance��6g   $��Stretch=�6�   6  � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      �� -   5        	 ��Name$	 aPopupButton��5   %   $��0��6   $
��Save��6;   $
��Left=�6J   $��Center=�6e   $��75��6�   $��5,0,5,0q�6�   6�   �� 5      -   5        $	 bPopupButton��5   %   $��1��6   $��Cancel��6;   $
��Left=�6L   $��Center=�6g   $��75��6�   $��5,0,5,0q�6�   6�   5      5      5      