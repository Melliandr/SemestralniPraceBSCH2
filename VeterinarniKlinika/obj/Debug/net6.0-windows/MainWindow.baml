   M S B A M L   `   `   `  ����M  IVeterinarniKlinika, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:VeterinarniKlinika.ViewModelVeterinarniKlinika.ViewModel  5   	   #    VeterinarniKlinika.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:VeterinarniKlinika.ViewModel     5   	     � Title$  Veterinarni klinika��5   	   $	��620��6%   $
��1000��62   $��16(�5	   	   $��Segoe UI)�6    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5         ButtonStyle �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value�� 5   
   %�  &	        �� 5      " ��6(   .�5      ��  � �� �#007acc6/   �� 5       � ��
 �����6/   �� 5       � ��$
 Bold"�6/   �� 5       � ��$ 18(�6-   �� 5       � ��$ 10q�6+   �� 5       � ��$ 10q�6,   �� 5       �@�Cursor$
 Hand��6+    5      P� 5       �@J�IsMouseOver$
 True��65   . �5      ��  � �� �#00508e67   5      5      5      .��5#      � 	 � RowDefinitions	 5$   
   �� 5%      $
��Auto �6   �� 5&      $��* �6   .+�5*   
   �� $��Veterinarni klinika��6   $��0��6/   $��36(�5+      $
��Bold"�6"   $��Center=�5,      $��Center=�61   $��10q�5-      �� 50   
   $��1��6   $��10q�6$   $
��Auto=�60   .��51      �� .+�52      �� $��About Us��6   $
��Bold"�6,   $��25(�6>   �� 53      $
��Wrap=�6   $��0,10,0,0q�60   ��Welcome to Happy Paws Veterinary Clinic, where we provide top-notch care for your beloved pets. Our team of experienced veterinarians and caring staff are dedicated to ensuring the health and well-being of your furry family members.57      �� 5;      $��Our Services��6   $
��Bold"�60   $��25(�6B   $��0,10,0,0q�6P   �� 5<      $��0,10,0,0q�6   �� 5=      $*�$Add an appointment for a new patient��6
   �� 5>      �� 5?      $�Our veterinarians��6
   �� 5@      �� 5A      $�Our patients��6
   5B      5D      5E      �� 5H   
   $��1��6   
 �� Orientation$
 
Horizontal=�6"   $
��Left=�5I      $	��Top=�60   $��70��5J      $��579,436,0,0q�6!   $	��390��66   .+�5K      �� -   5        $��Animals��5K      $	��110��6'   #��[  63   $��12(�5L   ;   �� 5M      -   5        $��Veterinarians��5M      $	��110��6-   #��[  69   $��12(�5N   @   �� 5O      -   5        $��Appointments��5O      $	��110��6,   #��[  68   $��12(�5P   7   5Q      5R      5S      