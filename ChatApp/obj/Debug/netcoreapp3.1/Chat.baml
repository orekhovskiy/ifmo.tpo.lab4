   M S B A M L   `   `   `  ����B  >ChatApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ChatAppChatApp  5   	       ChatApp.Chat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ChatApp     5   	    
  Window#��[  5   	     � Title$
  Chat��5	   
   .��5
      �  � RowDefinitions 5   
   �� 5      $��35 �6   6*   �� 5      6   �� 5      $��40 �6   6*   .+�5   
   � $��0��5       � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      �� $��0��6   $��2��6%   
����  �$��0 0 0 1q�5      ��6/   Z� �� 5      $��0��5      $
��Left=�5       �� Orientation$ 
Horizontal=�5      .+�5       �� $��20(�5!      $��You are logged in as��5"      �� 5$      -   5         ��Name$ CurrentLogin��5%      4  .System.Windows.Automation.AutomationProperties   AutomationId$% ChatWindowCurrentLoginTextBlock��5&      $��20(�5'      $
��10 0q�5(      
����  �5)      5*      �� 5,      $��1��5-      $ 
Horizontal=�5.      $��Right=�5/      .+�50      �� -   5        $ ChatWindowLogoutButton��51        ChangeWindowBtn#��[ 52      $	��100��53      $��30��54      $	��Top=�55      $
��Left=�56      $��18(�57      $��logout��58      $
��20 2q�59      5:      5;      5<      �� 5>   
   -   5         ��Name$ ScrollViewer��5?      $ Wrapper��5@      $��1��5A      $
��True��5B      .��5C      �� -   5         ��Name$ Messages��5D      $ ChatWindowMessagesListBox��5E      $��Hidden=�5F        ListBox#��[ 5G      ��5H      �� 5I      .R�5J      �  5K      �� 5L   "   $��2* �63   6@   �� 5M   "   $	��10* �63   6A    5O      �� 5P   "   62   �� 5Q   "   62   .+�5T      �� $��0��6%   $��2��65   
����  � �� Padding$ 5q�5U   %   $��1q�61   ��6E   Z� �� 5W       	 Login#��[ 5X   !   $��0��5Y   !   $��0��5Z   !   $��Bottom=�5[   !   ��5\   !   �� 	 �� Path$	 Login��� 5]        DaySent#��[ 5^   !   $��1��5_   !   $��0��5`   !   ��5a   !   �� $	 DaySent��� 5b        Content#��[ 5c   !   $��0��5d   !   $��1��5e   !   $��2��5f   !   $��25(�5g   !   $��Center=�5h   !   $��5q�5i   !   ��5j   !   �� $	 Content�5k      5l      5o      5p      � 5s   
   $��2��6    5t      �� 5u      $	��10* �6#   61   �� 5v      $��2* �6#   60   .+�5y      �� -   5        
 ��Name$
 Message��5~      $ ChatWindowMessageTextBox��5z        TextBox#��[ 5{      $
��auto��5|      $
��auto��5}      $��0��5      �� 5�      -   5        $! ChatWindowSendMessageButton��5�      #��[ 5�      $
��auto��5�      $
��auto��5�      $��1��5�      $
��Send��5�      5�      5�      5�      5�      