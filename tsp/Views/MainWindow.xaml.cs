using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private readonly double _RADIUS_CITY_SMALL = 2.5;
        private readonly double _RADIUS_CITY_LARGE = 4.0;

        private int[]? _solutionToDraw;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            _viewModel.PropertyChanged += DataPropertyChangedEvent;
            _viewModel.PropertyChangedAsync += SolutionChangedEvent;

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

                if (_solutionToDraw == null)
                    DrawCoordinatesInCanvas();
                else
                    DrawLinesAndCoordinatesInCanvas();
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

                if (_solutionToDraw == null)
                    DrawCoordinatesInCanvas();
                else
                    DrawLinesAndCoordinatesInCanvas();
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
        private void SolutionChangedEvent(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NewSolution")
            {
                TSPSolutionFinder solFinder = _viewModel.TSPSolutionFinder;
                if (!solFinder.HasNewSolution()) throw new InvalidDataException("No new solution available!");

                Application.Current.Dispatcher.Invoke(() =>
                    {
                        _solutionToDraw = _viewModel.TSPSolutionFinder.RetrieveSolution();
                        double distance = _viewModel.TSPSolutionFinder.CalculateEffort(_solutionToDraw!);
                        string distanceText = string.Format("Distance: {0:F2}", distance);
                        DistanceInfo.Text = distanceText;
                        _viewModel.WriteToConsole(distanceText);

                        DrawLinesAndCoordinatesInCanvas();
                    }
                );
            }
        }
        private void DataPropertyChangedEvent(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.TspData))
            {
                DrawCoordinatesInCanvas();
                _solutionToDraw = null;     // new data, so old solution is not valid anymore
            }
        }

        private void MainWindowSizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            if (_solutionToDraw == null) 
                DrawCoordinatesInCanvas();
            else 
                DrawLinesAndCoordinatesInCanvas();
        }

        private void DrawCoordinatesInCanvas()
        {
            Canvas  canvas = TSPSolutionGraphCanvas;
            TSPData? coordinates = _viewModel.TspData;

            if (coordinates == null || canvas.ActualWidth == 0 || canvas.ActualHeight == 0) return;

            double offsetX, dimX, scaleX,
                   offsetY, dimY, scaleY;

            SetDimensionVariables(coordinates, canvas,
                out offsetX, out offsetY, out dimX, out dimY, out scaleX, out scaleY, 
                coordinates.XLargest, coordinates.YLargest);

            canvas.Children.Clear();

            foreach (City city in coordinates.Cities)
            {
                double mappedX = city.X * scaleX;
                double mappedY = city.Y * scaleY;

                Ellipse ellipse = GetEllipse(offsetX + mappedX, offsetY + (dimY - mappedY), _RADIUS_CITY_SMALL);
                ellipse.Fill = Brushes.OrangeRed;
                canvas.Children.Add(ellipse);
            }

        }
    
        private void DrawLinesAndCoordinatesInCanvas()
        {
            Canvas canvas = TSPSolutionGraphCanvas;
            TSPData? coordinates = _viewModel.TspData;
            int[]? solution = _solutionToDraw;

            if (coordinates == null || solution == null || canvas.ActualWidth == 0 || canvas.ActualHeight == 0) return;

            double offsetX, dimX, scaleX,
                   offsetY, dimY, scaleY;

            SetDimensionVariables(coordinates, canvas,
                out offsetX, out offsetY, out dimX, out dimY, out scaleX, out scaleY,
                coordinates.XLargest, coordinates.YLargest);

            canvas.Children.Clear();

            for (int i = 0, length = coordinates.Cities.Length; i < length; i++)
            {
                int fromIndex = solution[i];
                int toIndex = solution[i + 1 == length ? 0 : i + 1];

                City fromCity = coordinates.Cities[fromIndex];
                City toCity = coordinates.Cities[toIndex];


                double mappedX = fromCity.X * scaleX;
                double mappedY = fromCity.Y * scaleY;

                Ellipse ellipse = GetEllipse(offsetX + mappedX, offsetY + (dimY - mappedY), _RADIUS_CITY_LARGE);
                ellipse.Fill = Brushes.Blue;
                canvas.Children.Add(ellipse);

                
                double mappedX2 = toCity.X * scaleX;
                double mappedY2 = toCity.Y * scaleY;
                Line line = new Line
                {
                    X1 = offsetX + mappedX,
                    Y1 = offsetY + (dimY - mappedY),
                    X2 = offsetX + mappedX2,
                    Y2 = offsetY + (dimY - mappedY2),
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1
                };
                canvas.Children.Add(line);


                Ellipse ellipse2 = GetEllipse(offsetX + mappedX, offsetY + (dimY - mappedY), _RADIUS_CITY_SMALL);
                ellipse2.Fill = Brushes.OrangeRed;
                canvas.Children.Add(ellipse2);
            }
        }

        private static void SetDimensionVariables(TSPData tspData, Canvas canvas,
            out double paddingViewPortX,
            out double paddingViewPortY,
            out double dimensionViewPortX,
            out double dimensionViewPortY,
            out double unitSizeX,
            out double unitSizeY,
            double dimensionDataX,
            double dimensionDataY)
        {
            paddingViewPortX = paddingViewPortY = 10f;
            dimensionDataX = tspData.XLargest;
            dimensionDataY = tspData.YLargest;
            dimensionViewPortX = canvas.ActualWidth - paddingViewPortX * 2;
            dimensionViewPortY = canvas.ActualHeight - paddingViewPortY * 2;
            unitSizeX = dimensionViewPortX / dimensionDataX;
            unitSizeY = dimensionViewPortY / dimensionDataY;
        }
    
        private static Ellipse GetEllipse(double x, double y, double radius)
        {
            Ellipse retEl =  new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
            };

            Canvas.SetLeft(retEl, x - radius);
            Canvas.SetTop(retEl, y  - radius);

            return retEl;
        }
    }
}