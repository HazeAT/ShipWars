   M S B A M L   `   `   `  ����  WPF_SemesterProject*    $WPF_SemesterProject.DetailViewWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Details��5   	   $	��450��6   $	��740��6&    � 
ResizeMode$ NoResize=�62   .��5
      � 
���   � � ColumnDefinitions 5   
   �� 5      $
��20px �6   �� 5      $��350px �6   �� 5      $��350px �6   �� 5      $
��20px �6    � RowDefinitions 5   
   �� 5      $
��20px �6   �� 5      $
��50px �6   �� 5      $��320px �6   �� 5      $
��20px �6   .+�5   
   �� -   5         ��Name$ LabelInfoTitle��5      
��� ���$��1��6<   $��2��6L   $�� ��6`   $
��20px(�6k   $��Center=�6{   $��1��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ ListViewInfo��5      
���   �
��� ���$��1��6Q   $��2��6a    �� View 5      �� 5      ! �� ColumnHeaderContainerStyle 5      �� 5       �� 
TargetType" ��6    .�5      �� 	 �� Property	 � ��
 �� Value

 �   �6;   �� 5       	 � ��

 � ���6;   5!      .P�5#      �� 5$      5&      �� 5'   
   -   5         ��Name$ 	ImageInfo��5'      $��2��6#   $
��Left=�63   $	��250��6N   $��2��6[   $	��Top=�6h   $	��250��6�   $��	44,33,0,0q�6�   �� 5(   
   -   5         ��Name$ TextLabelInfo��5(      
��� ���
���   �
���   �$��2��6d   $
��Left=�6t   $��2��6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��253��6�   $��	44,33,0,0q�6�    J� RenderTransformOrigin$ 0.379,0.086&�6�   $	��262��6  $y�Hidden=�6    �� 
IsReadOnly$
 True��64  5+      5,      