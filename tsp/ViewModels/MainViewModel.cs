using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<int> _populationSize;
        private int _selectedPopulationSize;
        private TSPSolutionFinder _solutionFinder;


        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand StartButtonCommand { get; set; }
        public RelayCommand LoadProblemCommand { get; set; }

        public string CentralText
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<int> PopulationSize
        {
            get { return _populationSize; }
            set
            {
                _populationSize = value;
                OnPropertyChanged();
            }
        }
        public int SelectedPopulationSize
        {
            get { return _selectedPopulationSize; }
            set
            {
                _selectedPopulationSize = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _text = "Click the Run button to start the TSP calculation!";
            _selectedPopulationSize = 8;
            _populationSize = new ObservableCollection<int> { 4, 8, 16, 32, 64};

            StartButtonCommand = new RelayCommand(StartRun);
            LoadProblemCommand = new RelayCommand(LoadProblem);

            _solutionFinder = new TSPSolutionFinder();
        }

        private async void StartRun(object? parameter)
        {
            StartButtonCommand.SetCanExecute(false);
            LoadProblemCommand.SetCanExecute(false);
            if (_solutionFinder.Data != null)
            {
                Debug.WriteLine($"Run calculaton!");
            } else
            {
                CentralText = "Load the data first, before running the caluclation!";
                Debug.WriteLine($"Load data first!");
            }
            StartButtonCommand.SetCanExecute(true);
            LoadProblemCommand.SetCanExecute(true);
        }

        private async void LoadProblem(object? parameter)
        {
            LoadProblemCommand.SetCanExecute(false);
            StartButtonCommand.SetCanExecute(false);
            await Task.Run(() => _solutionFinder.SetupSolution("./Resources/att48.tsp", SelectedPopulationSize));

            if(_solutionFinder.Data != null)
            {
                string message = $"Data loaded!\nName:\t\t{_solutionFinder.Data.Name}\nDimension:\t{_solutionFinder.Data.Cities.Length}";
                Debug.WriteLine(message);
                CentralText = message;
            }
            LoadProblemCommand.SetCanExecute(true);
            StartButtonCommand.SetCanExecute(true);
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            Debug.WriteLine($"OnPropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
