   M S B A M L   `   `   `  ����I  EStepChat.UI2_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:StepChat.UI2_2StepChat.UI2_2  5   	       StepChat.UI2_2.ChatWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:StepChat.UI2_2     5   	     � Title$  
ChatWindow��5   	   $	��450��6   $	��800��6)   .��5	      ]� .+�5
   
   �� $��Right=�6   
���  ��$	��250��6=   .+�5      �� $��Bottom=�6    �� Orientation$ 
Horizontal=�65   .+�5      �� $��Bottom=�6   $��Right=�64   .��6P   
Your ID:6Z   �� 5      -   5         ��Name$
 lbId��5      $��Center=�6*   $��Center=�6G   6e   5      �� 5      .+�5      �� -   5        $��ContactList��5      ��. $��Visible=�6T   $	��150��6�    �� View 5      �� 5      .P�5      �� $��UserName��6)    �� Width$ 75��6;    �� DisplayMemberBinding 6F   �� *ContactNickname+�� 5      $��IsOnline��6)   $ 75��6;    6F   �� *
IsOnline+�� 5      $��ID��6-   $ 75��69    6D   �� *ApplicationUserId+5      5      5      �� 5   
   .+�5   
   �� -   5        $��	GroupList��5      ��. $��Visible=�6J   $	��150��6}    5!      �� 5#      .P�5$      �� $
��Name��6-   $	 250��6;    6I   �� *Title+5&      5)      5*      �� 5+      .+�5,      �� -   5         ��Name$ btCreate��5,      .��6B   Create Group6P   5-      51      �� 54   
   -   5         ��Name$ Chats��54      6&   56      57      