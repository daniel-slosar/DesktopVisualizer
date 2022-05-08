using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace WpfComAp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// TODO: VYTVORIT METODU, KTORA CHECKNE CI SU DATA DOSTATOCNE PREFILTROVANE (MENEJ AKO 0.5 MILIONA FRAMOV)
    
    public partial class MainWindow : Window
    {
        private VCDGenerator vcdGenerator;
        public ObservableCollection<Selectable> selectableIds { get; set; }
        private List<string> selectedIds = new (), prevSelectedIds = new ();
        private double accuracy = 1, timelineWidth = 0, prevAccuracy = 0;
        private string[] files, prevFiles;
        private long timeStart = 0, timeLast = 0, timeSpan = 0;
        private List<Canvas> timelines = new ();
        public MainWindow()
        {
            InitializeComponent();
            CreateCheckBoxList();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                files = openFileDialog.FileNames;
                vcdGenerator = new VCDGenerator(files);
                btnConvert.Visibility = Visibility.Visible;
                btnDisplayData.Visibility = Visibility.Visible;
                vcdGenerator.FindAllIDs();
                selectableIds.Clear();

                for (int i = 0; i<vcdGenerator.AllIDs.Count; i++)
                {
                    string id = vcdGenerator.AllIDs[i];
                    selectableIds.Add(new Selectable { TheText = id, TheValue = i});
                }
            }
            StackPanel stckpanel1 = new StackPanel();
            CanIDs.Visibility = Visibility.Visible;
        }
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        { 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "vcd";
            saveFileDialog.Title = "Kam chcete uložiť súbor?";
            saveFileDialog.ValidateNames = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = "vystup";
            if (saveFileDialog.ShowDialog() == true)
            {
                //textProgress.Text = "Konvertuje sa...";
                vcdGenerator.setIdFilter(selectedIds);
                vcdGenerator.Generate(saveFileDialog.FileName);
                //textProgress.Text = "Úspešne uložené";
            }
        }
        private void btnDisplayData_Click(object sender, RoutedEventArgs e)
        {
            accuracy = zoomSlider.Value;
            Label label1 = new Label();
           


            if ((selectedIds.All(prevSelectedIds.Contains) || prevFiles != files || prevAccuracy != accuracy) && selectedIds.Count != 0)
            {
                BorderSlider.Visibility = Visibility.Visible;
                scrollViewer1.Visibility = Visibility.Visible;
                VizualizDatLabel.Visibility = Visibility.Visible;
                MierkaPanel.Visibility = Visibility.Visible;
                MierkaLabel.Visibility = Visibility.Visible;
                MierkaJedn.Visibility = Visibility.Visible;
                MierkaValue.Visibility = Visibility.Visible;
                timelineContainer.Children.Clear();
                timelines.Clear();
                prevFiles = files;
                prevSelectedIds = selectedIds;
                prevAccuracy = accuracy;
                for (int i = 0; i<selectedIds.Count; i++)
                {
                    Canvas canvas = new Canvas();
                    canvas.Height = 10;
                    canvas.Margin = new Thickness(0, 20, 0, 20);
                    canvas.Background = new SolidColorBrush(Colors.Blue);
                    timelines.Add(canvas);
                    timelineContainer.Children.Add(canvas);
                }
                

                StreamReader reader = new StreamReader(files[0], Encoding.UTF8);
                reader.ReadLine();
                CANFrame firstFrame = new(reader.ReadLine());
                timeStart = firstFrame.Time;
                foreach (var file in files)
                {
                    reader = new StreamReader(file, Encoding.UTF8);
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        CANFrame frame = new(line);
                        Ellipse button = new Ellipse();
                        
                        button.Width = 20;
                        button.Height = 20;
                        button.Stroke = new SolidColorBrush(Colors.Gray);
                        button.StrokeThickness = 1;
                        if (frame.FD1)
                        {
                            button.Fill = new SolidColorBrush(Colors.CadetBlue);
                            if (frame.Esi)
                                button.Fill = new SolidColorBrush(Colors.Red);
                        }
                        else
                            button.Fill = new SolidColorBrush(Colors.Cyan);

                        button.AddHandler(Ellipse.MouseEnterEvent, new RoutedEventHandler(onFrameClick));
                        button.Tag = frame;

                        int index = selectedIds.IndexOf(frame.Id);
                        if (index != -1)
                        {
                            timelines[index].Children.Add(button);
                            Canvas.SetTop(button, -5);
                            Canvas.SetLeft(button, (frame.Time-timeStart)/accuracy);
                        }
                    }
                }
                timeLast = ((timelines[timelines.Count-1].Children[(timelines[timelines.Count-1].Children.Count-1)] as Ellipse).Tag as CANFrame).Time;
                timeSpan = timeLast - timeStart;
                timelineWidth = timeSpan / accuracy;
                for(long i = 0; i < timeSpan; i += (long)(400 * accuracy))
                {
                    Polyline polyline = new Polyline();
                    polyline.StrokeThickness = 1;
                    polyline.Stroke = new SolidColorBrush(Colors.White);
                    PointCollection points = new PointCollection();
                    points.Add(new Point(i/accuracy, -2 * timelines.Count * timelineContainer.ActualHeight));
                    points.Add(new Point(i/accuracy, 50));
                    polyline.Points = points;
                    timelines[timelines.Count-1].Children.Add(polyline);
                    Label timeLabel = new ();
                    timeLabel.Content = CANFrame.HumanizeTimeStatic(i+timeStart);
                    timeLabel.FontSize = 10;
                    timeLabel.Foreground = Brushes.White;
                    timelines[timelines.Count - 1].Children.Add(timeLabel);
                    Canvas.SetBottom(timeLabel, -23);
                    Canvas.SetLeft(timeLabel, i / accuracy);


                }
                foreach (var timeline in timelines)
                    timeline.Width = timelineWidth;
                timelines.Clear();


                /*
                for (int i = 0; i<frames.Count; i++)
                {
                    ObservableCollection<CANFrame> id = frames[i];
                    Canvas canvas = new Canvas();
                    canvas.Height = 10;
                    canvas.Width = timelineWidth;
                    canvas.Margin = new Thickness(0, 20, 0, 20);
                    canvas.Background = new SolidColorBrush(Colors.Blue);
                    timelines.Add(canvas);
                    timelineContainer.Children.Add(canvas);
                    for (int j = 0; j<id.Count; j++) 
                    {
                        CANFrame frame = id[j];
                        Button button = new Button();
                        button.Width = 20;
                        button.Height = 20;
                        if (frame.FD1)
                        {
                            button.Background = new SolidColorBrush(Colors.CadetBlue);
                            if(frame.Esi)
                                button.Background = new SolidColorBrush(Colors.Red);
                        }
                        else
                            button.Background = new SolidColorBrush(Colors.Cyan);
                        button.BorderThickness = new Thickness(2);
                        button.Click += new RoutedEventHandler(onFrameClick);
                        button.Tag = frame;
                        canvas.Children.Add(button);
                        Canvas.SetLeft(button, (frame.Time-timeStart)/accuracy);
                    }
                }*/
            }
        }


        private void onFrameClick(object sender, RoutedEventArgs e)
        {
            
            try
            {
                CANFrame frame = (e.Source as Ellipse).Tag as CANFrame;
                Border border = new Border();
                
               
                if (frame != null) 
                {
                    
                    Brdr1.BorderThickness = new Thickness(1);
                    Brdr1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF34346A")); ;
                    string DataWspaces = System.Text.RegularExpressions.Regex.Replace(frame.Data, ".{4}", "$0 ");
                    frameDetail.Text =
                        //"Poradie: " + frame.Index + "\n" +
                        "Čas: " + frame.TimeHumanized() + "\n" +
                        "CAN ID: " + frame.Id + "\n" +
                        "Extended: " + frame.IDE1 + "\n" +
                        "FD: " + frame.FD1 + "\n" +
                        "ESI: " + frame.Esi + "\n" +
                        "Dĺžka: " + frame.Length + "\n" +
                        "Dáta: " + DataWspaces + "\n";
                        //"         " + data2 + "\n" +
                        //"Real data:" + frame.Data + "\n" +
                        //"Dáta: " +  + "\n" +
                        //"Dlzka lol: " + frame.Data.Length + "\n";
               

                    
                }
               
            }
            catch (Exception exception)
            { 
                
            }
        }
       
        public class Selectable
        {
            public string TheText { get; set; }
            public int TheValue { get; set; }
        }
        
        public void CreateCheckBoxList()
        {
            selectableIds = new ObservableCollection<Selectable>();
            this.DataContext = this;
        }
        private void CheckBoxZone_Change(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;

            if (chkZone.IsChecked == true)
                selectedIds.Add(chkZone.Content.ToString());
            else
                selectedIds.Remove(chkZone.Content.ToString());
        }
    }
}
