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
using System.Windows.Controls;
using System.Windows.Input;
using Accessibility;
using TSP.Service;

namespace TSP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string      _text;
        private int         _selectedPopulationSize;
        private string      _selectedProblemFile;
        private float       _mutationProbability;
        private int         _mutationRange;
        private int         _iterations;
        private bool        _isRunSettingsEnabled;
        private TSPData?     _tspData;

        private int          _mutationRangeMax = int.MaxValue;
        private readonly int _iterationsMax = 1_000_000;

        private ObservableCollection<int>      _populationSize;
        private ObservableCollection<string>   _problemFile;
        private TSPSolutionFinder              _solutionFinder;

        /// <summary>
        /// If a property was changed by code, this event is triggered to notify the UI.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand StartButtonCommand { get; set; }
        public RelayCommand LoadProblemCommand { get; set; }

        public string   SelectedProblemFile
        {
            get { return _selectedProblemFile; }
            set { _selectedProblemFile = value; }
        }
        public string   ConsoleText
        {
            get { return _text; }
            set 
            { 
                _text += value;
                NotifyPropertyChanged();
            }
        }
        public int      SelectedPopulationSize
        {
            get { return _selectedPopulationSize; }
            set { _selectedPopulationSize = value; }
        }
        public int      MutationRange
        {
            get { return _mutationRange; }
            set 
            { 
                if (value < 0 ) _mutationRange = 0;
                else if (value > _mutationRangeMax) _mutationRange = _mutationRangeMax;
                else _mutationRange = value;
            }
        }
        public int      Iterations
        {
            get { return _iterations; }
            set 
            {
                if (value > _iterationsMax) _iterations = _iterationsMax;
                else if (value < 0) _iterations = 0;
                else _iterations = value;
            }
        }
        public float    MutationProbability
        {
            get { return _mutationProbability; }
            set 
            {
                if (value > 1f) _mutationProbability = 1f;
                else if (value < 0f) _mutationProbability = 0f;
                else _mutationProbability = value;
            }
        }
        public bool     IsRunSettingsEnabled
        {
            get { return _isRunSettingsEnabled; }
            set 
            { 
                _isRunSettingsEnabled = value; 
                NotifyPropertyChanged();
            }
        }
        public TSPData?  TspData
        {
            get { return _tspData; }
            set 
            { 
                _tspData = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<int> PopulationSize
        {
            get { return _populationSize; }
            set
            {
                _populationSize = value;
            }
        }
        public ObservableCollection<string> ProblemFile
        {
            get { return _problemFile; }
            set
            {
                _problemFile = value;
            }
        }

        //
        // Constructors
        //

        public MainViewModel()
        {
            _text = "";
            _mutationProbability = 0.1f;
            _mutationRange = 2;
            _iterations = 10000;
            _populationSize = new ObservableCollection<int> { 4, 8, 16, 32, 64};
            _selectedPopulationSize = _populationSize[3];
            _problemFile = new ObservableCollection<string> { "att48.tsp" };
            _selectedProblemFile = _problemFile[0];
            _isRunSettingsEnabled = false;

            StartButtonCommand = new RelayCommand(StartRun);
            LoadProblemCommand = new RelayCommand(LoadProblem);

            _solutionFinder = new TSPSolutionFinder();


        }

        /// <summary>
        /// This method writes a message to the console. If debug is set to true, the message will be written to the debug console as well.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="debug"></param>
        private void WriteToConsole(string message, bool debug = false)
        {
            // Since multiple threads can try to put something into the console
            lock (_text)
            {
                ConsoleText = "\n";
                ConsoleText = message;
                ConsoleText = "\n";
            }
            if (debug) Debug.WriteLine(message);
        }

        private void StartRun(object? parameter)
        {
            RunCalculation(parameter);
        }

        private async void RunCalculation(object? parameter) 
        {
            StartButtonCommand.SetCanExecute(false);
            LoadProblemCommand.SetCanExecute(false);
            if (_solutionFinder.DataLoaded())
            {

                _solutionFinder.SetupSolution(SelectedPopulationSize, MutationRange, MutationProbability);

                if (_solutionFinder.ReadyToRun())
                {
                    WriteToConsole($"Run calculaton!");
                    WriteToConsole($"Parameters: \nPopulation size: {SelectedPopulationSize}\nMutation probability: {MutationProbability}\nMutation range: {MutationRange}");
                    Task runner = Task.Run(() => _solutionFinder.Run(Iterations));
                    await Task.Run(() =>
                    {
                        int[]? solution = null;
                        while (!runner.IsCompleted)
                        {
                            if ((solution = _solutionFinder.RetrieveSolution()) != null)
                            {
                                WriteToConsole($"New score: {_solutionFinder.EffortBestSolution}");

                            }
                        }
                    });
                    WriteToConsole("Done!");
                }
            }
            else
            {
                WriteToConsole("Load the data first, before running the caluclation!");
            }
            StartButtonCommand.SetCanExecute(true);
            LoadProblemCommand.SetCanExecute(true);
        }

        private async void LoadProblem(object? parameter)
        {
            LoadProblemCommand.SetCanExecute(false);
            StartButtonCommand.SetCanExecute(false);

            WriteToConsole($"Loading data file {SelectedProblemFile} ...", true);
            await Task.Run(() => _solutionFinder.LoadData($"./Resources/{SelectedProblemFile}"));

            if(_solutionFinder.DataLoaded())
            {
                string message = _solutionFinder.GetDataInfo();
                WriteToConsole(message, true);

                _mutationRangeMax = _solutionFinder.GetCityCount();
                TspData = _solutionFinder.GetData();
                IsRunSettingsEnabled = true;
            }
            LoadProblemCommand.SetCanExecute(true);
            StartButtonCommand.SetCanExecute(true);
        }

        /// <summary>
        /// This method is used to notify the UI that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            Debug.WriteLine($"OnPropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
