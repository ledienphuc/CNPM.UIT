   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp2.ViewsWpfApp2.Views  5   	       WpfApp2.Views.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfApp2.Views     5   	     � Title$  
MainWindow��5   	    � WindowState$ 	Maximized=�6    � DataContext 64   �� *Main+   Locator �� Source# [  .��5	      � $��0,-1,0,1q�6    � ColumnDefinitions 5
   
   �� 5      $��75 �6   �� 5      $
��207* �6   �� 5      $
��478* �6    � RowDefinitions 5   
   �� 5      $��78 �6   �� 5      $��78 �6   �� 5      $��78 �6   �� 5      $��78 �6   �� 5      $��78 �6   �� 5      $	��114 �6   .+�5   
   �� $��Trang chủ��6   $
��Left=�6%   $��Center=�6@   $��75��6[   $��Center=�6f   $��20��6�   $
��0,29q�6�   �� 5   
   $��Phòng��6   $
��Left=�6I   $��1��6d   $��Center=�6q   $��75��6�   $��20��6�   $
��0,29q�6�   ��6!   �� *DatPhongViewCommand+6�   �� 5   
   $��	Báo cáo��6   $
��Left=�6#   $��3��6>   $��Center=�6K   $��75��6f   $��20��6q   $��	0,30,0,28q�6}   �� 5   
   $��Chỉnh sửa��6   $
��Left=�6%   $��4��6@   $��Center=�6M   $��75��6h   $��20��6s   $��	0,33,0,25q�6   �� 5   
   $��
Danh Mục��6   $
��Left=�6Q   $��2��6l   $��Center=�6y   $��75��6�   $��20��6�   $��	0,31,0,27q�6�   ��6$   �� *DanhSachPhongViewCommand+6�   �� 5   
   $��Thoát��6   $��5��6!   $��75��6.   $
��Left=�69   $��Center=�6T   $��20��6o   $��	0,60,0,34q�6{   �� 5   
   $��1��6?   $	��Top=�6O   $	��504��6g   $��6��6t   $��2��6�   ��6   �� *CurrentViewModel+6�   5      5       