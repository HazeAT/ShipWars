   M S B A M L   `   `   `  ����  WPF_SemesterProject&     WPF_SemesterProject.TravelWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Travel��5   	   $	��470��6   $	��740��6%    � 
ResizeMode$ NoResize=�61   .��5
      � 
���   � � ColumnDefinitions 5   
   �� 5      $
��20px �6   �� 5      $��700px �6   �� 5      $
��20px �6    � RowDefinitions 5   
   �� 5      $
��20px �6   �� 5      $
��50px �6   �� 5      $��320px �6   �� 5      $
��50px �6   �� 5      $
��20px �6   .+�5   
   �� -   5         ��Name$ LabelTravelTitle��5      
��� ���$��1��6>   $��2��6N   $��Travel��6b   $
��20px(�6s   $
��Left=�6�   $��1��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ MyShipsListViewTravel��5      
��� ���
���   �$y�Visible=�6Y   $��2��6n   $��1��6{    �� View 5      �� 5      ! �� ColumnHeaderContainerStyle 5      �� 5       �� 
TargetType" ��6    .�5      �� 	 �� Property	 � ��
 �� Value

 �   �6;   �� 5       	 � ��

 � ���6;   5!      .P�5#      �� $��Model��6%    �� Width$ 330px��6[    �� DisplayMemberBinding 64   �� *model+6[   �� 5$      $��Amount��6%   $
 80px��6]    65   �� *amount+6]   �� 5%      $��Missions��6%   $
 80px��6_    67   �� *health+6_   �� 5&      $��Health��6%   $
 80px��6]    65   �� *health+6]   �� 5'      $��Value��6%   $ 100px��6[    64   �� *value+6[   5(      5*      �� 5+   
   -   5        $��1��5+      $��Travel��6!   
��� ���
���   �
��� ���$
��Left=�6n   $��	640,5,0,0q�6�   $��3��6�   $	��Top=�6�   $��30��6�   $��60��6�   6�   5-      5.      