   M S B A M L   `   `   `  ���� C  ?WpfComAp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfComApWpfComAp      WpfComAp.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:WpfComAp     -   $
��okno��$	��550��$
��1050��  � Title$  CAN Vizualizer�����	#FF292946$	��900��$	��550�� � Icon$8 2C:\Users\GHOST\Documents\Github\WpfComAp\icon1.ico��$	��560��.���� .+��  � Orientation$ 
Horizontal=�.+��� $��Center=�$	��Top=�$	��161��$��	20,20,0,0q�.+��� $
��Menu��$��10(�$��Lucida Console)�
��������� -    ��Name$ btnOpenFile��
����������	#FF34346A$��30��$��Normal"�$��Lucida Console)� � UseLayoutRounding$ False��$��Vybrať súbor��
�������$��0,0,0,5q� � 	Resources  �� 
TargetType �� Property �� Value��%9   '��       �� " ��.���  �@��CornerRadius$ 8���� -   $ 
btnConvert��
�������$y�Hidden=�$��30��$��Lucida Console)����#34346a$��Konvertovať��
�������$��0,0,0,5q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 8���� -   $ btnDisplayData��
�������$y�Hidden=�$��30��$��Lucida Console)����#34346a$��Zobraziť dáta��
������� ��%9   '��       �� " ��.���  �@��CornerRadius$ 8���� -   	 ��Name$	 CanIDs��$	��254��$	��217��
 J� RenderTransformOrigin$
 0.5,0.5&�$��0,20,0,0q�$y�Hidden=�z�V� .~��� �� ��  �� Angle$ 0.392Q�T� .+��� $��CAN ID��$��10(�
�������$��5,5,5,5q�$��17,0,0,0q�$��Lucida Console)��� -    ��Name$ listBoxZone��$	��191�����	#FF34346A$	��238��
����������� *selectableIds+ ��%9   '��       �� " ��.���  �@��CornerRadius$ 8������ .R���  ��Name$ CheckBoxZone��-   $��0,5,0,0q�
����������� *	TheText+
 � Tag �� *
TheValue+�� -    ��Name$ Brdr1��
�������$��0q�$
��Left=�$	��Top=�$��0,40,0,0q�$y�Visible=� �� CornerRadius$ 8,8,8,8��._��� -	    ��Name$ frameDetail��$
��Wrap=�$�� ��$��Normal"� �� Padding$ 5,0,0,0q�
�������$��0,0,5,0q�$	��300���� -
   $	 MierkaPanel��$y�Visible=�$	��128��.+��� $��Mierka��
�������$��10(�$��Lucida Console)�$��	10,20,0,0q���Z� �� -   $ BorderSlider��$��1q�
�������$��35�����	#FF34346A$ 8,8,8,8��$y�Visible=�$��10,0,0,0q�._��� -    ��Name$ 
zoomSlider��$	��100�� � Minimum$	 0.1Q� � Maximum$	 100Q� � Value$ 50Q� �� TickFrequency$	 0.1Q� �� IsSnapToTickEnabled$
 True��
�������$��0,7,0,0q� � Cursor$
 Hand����Z� �� -    ��Name$ MierkaLabel��$��Mierka:��
�������$��5,0,5,5q�$��Segoe UI)�$y�Visible=�$��	10,50,0,0q��� -   $ MierkaValue��$��31��
�������$��0,0,2,5q�$��34��$��Segoe UI)�$y�Visible=�$��0,50,0,0q����� *Value+ �� ElementName$ 
zoomSlider�� �� UpdateSourceTrigger$ PropertyChanged=��� -   $ 
MierkaJedn��$��μs/px��$��0,0,5,5q�
�������$��Segoe UI)�$y�Visible=�$��0,50,0,0q��� -   $ VizualizDatLabel��$��Vizualizácia dát��
�������$��	22,30,0,0q�$��10(�$y�Hidden=��� -    ��Name$ scrollViewer1��$��Visible=� %� IsVirtualizing$
 True�� %� VirtualizationMode$ Standard=�$	��200��$��0,5,0,0q�$y�Hidden=�.���� -   $	 timelineContainer��$
 True��$ Standard=����� *timelineWidth+