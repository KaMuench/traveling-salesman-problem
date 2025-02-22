using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TSP.Service;
using TSP.ViewModels;

namespace TSP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;


        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            _viewModel.PropertyChanged += DataPropertyChangedEvent;

            SizeChanged += MainWindowSizeChangedEvent;
        }


        /// <summary>
        /// This method is used to scroll the console to the end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleScrollToEndEvent(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox?.ScrollToEnd();
        }

        private void ConsoleResizeEvent(object sender, DragDeltaEventArgs e)
        {
            var consoleRow = this.ConsoleRow;
            var canvasRow = this.CanvasRow;
            double newConsoleHeight = consoleRow.Height.Value - e.VerticalChange;
            double newCanvasHeight  = canvasRow.ActualHeight + e.VerticalChange;


            if (newConsoleHeight > 50 && newCanvasHeight > 100) 
            {
                consoleRow.Height = new GridLength(newConsoleHeight);
                DrawCoordinatesInCanvas();
            }
        }

        private void SettingsColumnResizeEvent(object sender, DragDeltaEventArgs e)
        {
            var settingsCol = this.SettingsCol;
            var canConCol   = this.CanvasConsoleCol;
            double newSetColWidth = settingsCol.Width.Value - e.HorizontalChange;
            double newCanColWidth = canConCol.ActualWidth + e.HorizontalChange;


            if (newSetColWidth > 150 && newCanColWidth > 250)  
            {
                settingsCol.Width = new GridLength(newSetColWidth);
                DrawCoordinatesInCanvas();
            }
        }

        private void IntTextBoxContentChangedEvent(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(((TextBox)sender).Text + e.Text, out _);
        }

        private void FloatTextBoxContentChangedEvent(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex(@"^[0-9]*\.?[0-9]*$");
            //e.Handled = !regex.IsMatch(((TextBox)sender).Text + e.Text);

            e.Handled = !float.TryParse(((TextBox)sender).Text + e.Text, out _);
        }

        private void DataPropertyChangedEvent(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.TspData))
            {
                DrawCoordinatesInCanvas();
            }
        }

        private void MainWindowSizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            DrawCoordinatesInCanvas();
        }

        private void DrawCoordinatesInCanvas()
        {
            Canvas  canvas = TSPSolutionGraphCanvas;
            TSPData? coordinates = _viewModel.TspData;

            if (coordinates == null) return;

            double  max_CorX = coordinates.XLargest;
            double  max_CorY = coordinates.YLargest;
            double  offsetX = 10f;
            double  offsetY = 10f;

            if (canvas.ActualWidth == 0 || canvas.ActualHeight == 0) return;

            double dimX = canvas.ActualWidth - offsetX*2;
            double dimY = canvas.ActualHeight - offsetY*2;
            double scaleX = dimX  / max_CorX;
            double scaleY = dimY  / max_CorY;

            canvas.Children.Clear();

            foreach(City city in coordinates.Cities)
            {
                double mappedX = city.X * scaleX;
                double mappedY = city.Y * scaleY;

                Ellipse ellipse = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.OrangeRed
                };
                Canvas.SetLeft(ellipse, offsetX + mappedX);
                Canvas.SetTop(ellipse,  offsetY + (dimY - mappedY));
                canvas.Children.Add(ellipse);
            }

        }
    }
}