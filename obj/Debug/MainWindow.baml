   M S B A M L   `   `   `  ����J  FDCodeSerialComm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DCodeSerialCommDCodeSerialComm  5   	        DCodeSerialComm.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DCodeSerialComm     5   	     � Title$  DCode Serial Comm��5   	   $	��160��6#   $	��590��60   .��5	      �  J� RenderTransformOrigin$ 
0.488,0.52&�6   $��0,0,2,0q�6.    � ColumnDefinitions 5
   
   �� 5      $	��10* �6   �� 5      $
��561* �6   .+�5   
   �� -   5         ��Name$ browse_button��5      $��Browse��6(   $
��Left=�69   $��
456,10,0,0q�6T   $	��Top=�6h   $	��101��6�   $ 	0.379,1.6&�6�   $��30��6�   $��1��6�   6�   �� 5   
   -   5         ��Name$ dcode_filename��5      $
��Left=�6*   $��30��6E   $��0,10,0,0q�6Q    �� TextWrapping$
 Wrap=�6c   $��Filename��6w   $	��Top=�6�   $	��451��6�   $ -1.322,0.226&�6�   $��1��6�   �� 5   
   -   5        $ verify_send_button��5      $��Verify and Send��6-   $
��Left=�6G   $	��Top=�6b   $	��100��6z   $��
351,45,0,0q�6�   $ 	0.979,2.8&�6�   $��30��6�   $��1��6�   6�   � 5   
   -   5         �Name$ send_progress_bar��5      $��1��61   $
��Left=�6A   $��20��6\   $��0,80,0,0q�6h   $	��Top=�6z   $	��557��6�   $ -0.83,-1&�6�   �� 5   
   -   5         ��Name$ progress_bar_percent��5      $��1��62   $
��Left=�6B   $��0,80,0,0q�6]   $
��Wrap=�6o   $�� ��6�   $	��Top=�6�   $ 2.067,0.529&�6�   $	��557��6�    �� TextAlignment$ Center=�6�   $��20��6�   �� 5   
   -   5        $ 
tbProgress��5      $
��Left=�6(   $��	0,105,0,0q�6C   $
��Wrap=�6V   $�� ��6j   $	��Top=�6r   $	��557��6�   $ Center=�6�   $��20��6�   $ 	0.557,2.5&�6�   $��1��6�   �� 5   
   -   5        $ cancel_button��5      $��Cancel��6(   $
��Left=�69   $	��Top=�6T   $	��101��6l   $��
456,45,0,0q�6x   $ 	0.979,2.8&�6�   $��30��6�   $��1��6�   {�.  6�   �� 5   
   -   5        $ com_port_text��5      $��1��6+   $
��Left=�6;   $��
105,45,0,0q�6V   $
��Wrap=�6j   $��Find COM Port��6~   $	��Top=�6�   $��30��6�   $	��241��6�   $ Center=�6�   �� 5   
   -	   5        $ find_com_port_button��5      $��Find COM Port��6/   $
��Left=�6G   $	��Top=�6b   $	��100��6z   $��0,45,0,0q�6�   $ 	0.979,2.8&�6�   $��30��6�   $��1��6�   6�   5      5      