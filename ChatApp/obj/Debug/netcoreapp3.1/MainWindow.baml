   M S B A M L   `   `   `  ����B  >ChatApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ChatAppChatApp  5   	       ChatApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ChatApp     5   	    
  Window#��[  5   	     � Title$  
MainWindow��5	   	   .��5
      �  � RowDefinitions 5   
   �� 5   
   6   �� 5   
   $��50 �6   6&   �� 5   
   $��50 �6   6&   �� 5   
   6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� 4  .System.Windows.Automation.AutomationProperties   AutomationId$ MainWindowTitle��5      $��0��5      $��0��5      $��40(�5      $��Center=�5      $��Bottom=�5      $��2��5      $��Log in��5      �� 5!   
   $��1��5"      $��0��5#      $��2��5$       �� Orientation$ 
Horizontal=�5%      $��Center=�5&      .+�5(      �� $	��150��5)      $��30(�5*      $��Login��5+      �� 5,      -   5         ��Name$ Login��5-      $ MainWindowLoginTextBox��5.        TextBox#��[ 5/      $��Center=�50      51      �� 53   
   $��2��54      $��0��55      $��2��56      $ 
Horizontal=�57      $��Center=�58      .+�5:      �� $	��150��5;      $��30(�5<      $��Password��5=      G� 5>      -   5         G�Name$ Password��5?      $# MainWindowPasswordPasswordBox��5@        PasswordBox#��[ 5A      $��Center=�5B      5C      �� 5H   
   -   5        $ MainWindowLoginButton��5I        ChangeWindowBtn#��[ 5J      $��3��5K      $��1��5L      $	��Top=�5M      $
��Left=�5N      $��90 20q�5O      $��login��5P      5Q      �� 5S   
   -   5        $ MainWindowRegisterButton��5T      #��[ 5U      $��3��5V      $��0��5W      $	��Top=�5X      $��Right=�5Y      $��	0 20 90 0q�5Z      $��register��5[      5]      5^      5_      