   M S B A M L   `   `   `  ����C  ?WpfComAp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfComApWpfComAp  5   	       WpfComAp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfComAp     5   	   -   5        $
��okno��5   '   $	��550��5      $
��1050��6'     � Title$  CAN Vizualizer��64   ���	#FF292946$	��900��6b   $	��550��6q    � Icon$8 2C:\Users\GHOST\Documents\Github\WpfComAp\icon1.ico��6�   $	��560��6�   .��5      �� .+�5	   
   �  � Orientation$ 
Horizontal=�6   .+�5      �� $��Center=�6   $	��Top=�66   $	��161��6K   $��	20,20,0,0q�6W   .+�5      �� $
��Menu��6   $��10(�6'   $��Lucida Console)�65   
�������6Q   �� 5      -   5         ��Name$ btnOpenFile��5      
����������	#FF34346A$��30��6r   $��Normal"�6~   $��Lucida Console)�6�    � UseLayoutRounding$ False��6�   $��Vybrať súbor��6�   
�������$��0,0,0,5q�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%^   '��       �� 5      " ��6    .�5      ��  �@��CornerRadius$ 8��6=   5      5      5      �� 5      -   5        $ 
btnConvert��5      
�������$y�Hidden=�6Y   $��30��6m   $��Lucida Console)�6y   ���#34346a$��Konvertovať��6�   
�������$��0,0,0,5q�6�    5      ��%^   '��       �� 5      " ��6    .�5      ��  �@��CornerRadius$ 8��6=   5      5      5      �� 5      -   5        $ btnDisplayData��5      
�������$y�Hidden=�6a   $��30��6u   $��Lucida Console)�6�   ���#34346a$��Zobraziť dáta��6�   
������� 5      ��%^   '��       �� 5      " ��6    .�5       ��  �@��CornerRadius$ 8��6=   5!      5"      5#      5%      �� 5'      -   5        	 ��Name$	 CanIDs��5'      $	��254��6)   $	��217��65   
 J� RenderTransformOrigin$
 0.5,0.5&�6B   $��0,20,0,0q�6b   $y�Hidden=�6t   z�5(      V� 5)      .~�5*      �� �� 5+      �� 5,       �� Angle$ 0.392Q�6*   T� 5-      5.      .+�50      �� $��CAN ID��6   $��10(�6)   
�������$��5,5,5,5q�6J   $��17,0,0,0q�6\   $��Lucida Console)�6n   �� 51      -   5         ��Name$ listBoxZone��51      $	��191��52      ���	#FF34346A$	��238��6&   
���������51   /   �� *selectableIds+ 53      ��%^   '��       �� 54      " ��6    .�55      ��  �@��CornerRadius$ 8��6=   56      57      ��58      �� 59      .R�5:      ��  ��Name$ CheckBoxZone��6'   -   5        $��0,5,0,0q�5<      
���������5:   =   �� *	TheText+
 � Tag 5;      �� *
TheValue+5<      5=      5?      5@      �� 5A      -   5         ��Name$ Brdr1��5A      
�������$��0q�68   $
��Left=�6L   $	��Top=�6g   $��0,40,0,0q�6   $y�Visible=�6�    �� CornerRadius$ 8,8,8,8��6�   ._�5B      �� -	   5         ��Name$ frameDetail��5B   0   $
��Wrap=�6   $�� ��6E   $��Normal"�6M    �� Padding$ 5,0,0,0q�6a   
�������$��0,0,5,0q�6�   $	��300��6�   5C      �� 5D      -
   5        $	 MierkaPanel��5D      $y�Visible=�6.   $	��128��6C   .+�5E      �� $��Mierka��6   
�������$��10(�6R   $��Lucida Console)�6`   $��	10,20,0,0q�6|   ��6)   Z� 6|   �� 5F      -   5        $ BorderSlider��5F      $��1q�6/   
�������$��35��6W   ���	#FF34346A$ 8,8,8,8��6z   $y�Visible=�6�   $��10,0,0,0q�6�   ._�5G      �� -   5         ��Name$ 
zoomSlider��5G   E   $	��100��6    � Minimum$	 0.1Q�6)    � Maximum$	 100Q�67    � Value$ 50Q�6Y    �� TickFrequency$	 0.1Q�6d    �� IsSnapToTickEnabled$
 True��6x   
�������$��0,7,0,0q�6�    � Cursor$
 Hand��6�   ��6�   Z� 6�   5H      5I      �� 5J      -   5         ��Name$ MierkaLabel��5J      $��Mierka:��6)   
�������$��5,0,5,5q�6N   $��Segoe UI)�6`   $y�Visible=�6v   $��	10,50,0,0q�6�   �� 5K      -   5        $ MierkaValue��5K      $��31��6�   
�������$��0,0,2,5q�6�   $��34��6�   $��Segoe UI)�6�   $y�Visible=�6�   $��0,50,0,0q�6�   ��6)   �� *Value+ �� ElementName$ 
zoomSlider�� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5L      -   5        $ 
MierkaJedn��5L      $��μs/px��6(   $��0,0,5,5q�68   
�������$��Segoe UI)�6]   $y�Visible=�6s   $��0,50,0,0q�6�   5M      �� 5N   
   -   5        $ VizualizDatLabel��5N      $��Vizualizácia dát��6*   
�������$��	22,30,0,0q�6X   $��10(�6k   $y�Hidden=�6y   �� 5O   
   -   5         ��Name$ scrollViewer1��5O   �   $��Visible=�6    %� IsVirtualizing$
 True��6?    %� VirtualizationMode$ Standard=�6g   $	��200��6�   $��0,5,0,0q�6�   $y�Hidden=�6�   .��5P      �� -   5        $	 timelineContainer��5P   9   $
 True��6T   $ Standard=�6|   ��6   �� *timelineWidth+6|   5Q      5R      5S      