   M S B A M L   `   `   `  ����  WPFFudbalskiKlub     WPFFudbalskiKlub.FrmUgovor   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  Dodaj/Izmeni ugovor��5      $	��300��6*   $	��300��67   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� $��	UgovorID:��6   �� 5      -   5         ��Name$ UgovorID��5      $	��150��6(   $��5q�64   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��5q�6;   .+�5      �� $��Datum:��6   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ Datum��5      $	��150��6(   $��5q�64   5      �� 5   
   $��2��6   $ 
Horizontal=�6"   $��5q�6;   .+�5      �� $��NovcanaVrednost:��6   �� 5       -   5        $ NovcanaVrednost��5       $	��150��6/   $��5q�6;   5!      �� 5#   
   $��3��6   $ 
Horizontal=�6"   $��5q�6;   .+�5$      �� $��NoviClanID:��6   �� 5%      -   5         ��Name$ cbNoviClanID��5%      $	��150��6-   $��5q�69    �� SelectedValuePath$ 
NoviClanID��6D   5'      �� 5)   
   $��4��6   $ 
Horizontal=�6"   $��5q�6;   .+�5*      �� $��
VlasnikID:��6   �� 5+      -   5        $ cbVlasnikID��5+      $	��150��6,   $��5q�68   $ 	VlasnikID��6C   5-      �� 5/   
   $��5��6   $ 
Horizontal=�6"   $��5q�6;   .+�50      �� $��MenadzerID:��6   �� 51      -   5        $ cbMenadzerID��51      $	��150��6-   $��5q�69   $ 
MenadzerID��6D   53      � 57   
   .+�5;      �� -   5        $��
btnSacuvaj��5;      $��Sacuvaj��6'   $
��Left=�69   $��Bottom=�6T   $��30,0,0,-234q�6o   6�   �� 5>      -   5        $��	btnOtkazi��5>      $��Otkazi��6&   $��Right=�67   $��Bottom=�6S   $��0,0,28,-234q�6n   6�   5?      5B      5C      