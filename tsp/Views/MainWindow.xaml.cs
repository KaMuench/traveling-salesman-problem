using System.Diagnostics;
using System.Text;
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
using TSP.ViewModels;

namespace TSP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
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

        private void ConsoleResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var consoleRow = (RowDefinition)this.ConsoleRow;
            double newHeight = consoleRow.Height.Value - e.VerticalChange;

            // Set a minimum height for the TextBox (optional)
            if (newHeight > 50)  // Minimum height to prevent it from becoming too small
            {
                consoleRow.Height = new GridLength(newHeight);
            }
        }

        private void SettingsResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var settingsCol = this.SettingsCol;
            double newWidth = settingsCol.Width.Value - e.HorizontalChange;

            // Set a minimum height for the TextBox (optional)
            if (newWidth > 150)  // Minimum height to prevent it from becoming too small
            {
                settingsCol.Width = new GridLength(newWidth);
            }
        }
    }
}