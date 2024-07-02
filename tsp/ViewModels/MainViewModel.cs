using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TSP.Service;

namespace TSP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _text;


        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand StartButtonCommand { get; set; }

        public string CentralText
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _text = "Click the Run button to start the TSP calculation!";
            StartButtonCommand = new RelayCommand(StartRun);
        }

        private async void StartRun(object? parameter)
        {
            Debug.WriteLine("Start Run!");
            StartButtonCommand.SetCanExecute(false);

            TSPSolutionFinder solutionFinder = new TSPSolutionFinder();

            await Task.Run(()=>solutionFinder.LoadData("./Resources/att48.tsp"));

            Debug.WriteLine($"Name: {solutionFinder.Data.Name}");
            Debug.WriteLine($"Dimension: {solutionFinder.Data.Cities.Length}");

            StartButtonCommand.SetCanExecute(true);
            CentralText = "Calculation completed!";    
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            Debug.WriteLine($"OnPropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
