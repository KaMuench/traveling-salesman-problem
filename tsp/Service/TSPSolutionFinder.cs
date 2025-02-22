using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
    /// <summary>
    /// This class has the purpose of running the genetic algorithm to solve a traveling salesman problem.
    /// </summary>
    public class TSPSolutionFinder
    {
        public TSPPopulationFactory? PopulationFactory { get; private set; }
        public TSPPopulation?        Population { get; private set; }

        public double? EffortBestSolution { get; private set; }

        /// <summary>
        /// The list containing the City objects for the tsp. The coordinates of these objects are used to run the calculations.
        /// </summary>
        private TSPData? Data { get; set; }
        private Queue<int[]> _solutionsQueue = new Queue<int[]>();
        private int[]? _currentSolution;
        private int[]? _bestSolution;


        //
        // Data Loading and Setup
        //

        /// <summary>
        /// This method checks if the data and the population factory are loaded and ready to be used.
        /// </summary>
        /// <returns>True if Data and Factory are set up otherwise false</returns>
        public bool DataLoaded() 
        { 
            return Data != null && PopulationFactory != null; 
        }
        public bool ReadyToRun()
        {
            return DataLoaded() && Population != null && _bestSolution != null;
        }
        /// <summary>
        /// This method loads the data from a .tsp file. It creates a TSPData object and a TSPPopulationFactory object.
        /// </summary>
        /// 
        /// <param name="dataName">name of the data file</param>
        public void LoadData(string dataName)
        {
            Data = new TSPData(dataName);
            PopulationFactory = new TSPPopulationFactory(Data);
        }
        /// <summary>
        /// This method has the purpose of creating a TSPData instance ready to be used. It also setsup the arrays needed for the calculation.
        /// </summary>
        /// 
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="ArgumentException"/>"
        /// 
        /// <param name="dataName">The relative path to the .tsp file to set up the data with. The path must be relative to the project root folder.</param>
        public void SetupSolution(int populationSize, int mutationRange, float mutationProb)
        {
            if (!DataLoaded()) throw new InvalidOperationException("Data must be loaded first!");
            if (mutationProb > 1 || mutationProb <= 0) throw new ArgumentException("Mutation probability must be between 0 and 1!");
            if (mutationRange > Data!.Cities.Length || mutationRange < 1) throw new ArgumentException("Mutation range must be between 1 and the number of cities!");


            Population = PopulationFactory!.NewPopulation(populationSize, mutationProb, mutationRange);

            _currentSolution = Enumerable.Range(0, Data.Cities.Length).ToArray();
            _bestSolution = (int[])_currentSolution.Clone();
        }
        /// <summary>
        /// This method starts the caluclation. 
        /// </summary>
        /// <exception cref="InvalidOperationException">LoadData must be run successfully, before invoking this method.</exception>
        public void Run(int iterations)
        {
            if (!ReadyToRun()) throw new InvalidOperationException("Data must be loaded and the solution must be set up first!");

            int bestSolIndex = -1;

            for (int i=0; i<iterations;i++)
            {
                Population!.CrossOver();
                Population.Mutate();

                bestSolIndex = Population.GetBestSolution();
                _currentSolution = Population.GetSolutionCopy(bestSolIndex);

                if ((Population.CalculateEffort(_currentSolution)) < Population.CalculateEffort(_bestSolution!))
                {
                    _bestSolution = _currentSolution;
                    PutSolution((int[]) _bestSolution.Clone());
                }

                //Debug.Write($"\nIteration: {i}\n");
                //Population.DebugPopulation();
            }
        }

        /// <summary>
        /// This method returns a string with the information about the data loaded.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetDataInfo()
        {
            if (!DataLoaded()) throw new InvalidOperationException("Data must be loaded first!");

            string message = $"Name:\t\t{Data!.Name}\nDimension:\t{Data.Cities.Length}";

            return message;
        }
        public int    GetCityCount()
        {
            if (!DataLoaded()) throw new InvalidOperationException("Data must be loaded first!");

            return Data!.Cities.Length;
        }

        public TSPData GetData()
        {
            if (!DataLoaded()) throw new InvalidOperationException("Data must be loaded first!");
            return Data!;
        }

        //
        // Solution Fifo Queue
        //

        /// <summary>
        /// Method to put a newly calculated solution (order) in the solutions fifo queue at the bottom. The method provides syncronized write access.
        /// </summary>
        /// <param name="solution">The solution to be added to the queue</param>
        private void  PutSolution(int[] solution)
        {
            lock (_solutionsQueue)
            {
                _solutionsQueue.Enqueue(solution);
                EffortBestSolution = Population?.CalculateEffort(solution);
            }
        }
        /// <summary>
        /// Method to retrieve the element at the top of the fifo solutions queue, This method provides syncronized read access.
        /// </summary>
        /// <returns>The element at the top of the solutions queue</returns>
        public int[]? RetrieveSolution()
        {
            lock (_solutionsQueue)
            {
                if (_solutionsQueue.Count != 0) return _solutionsQueue.Dequeue();
                else return null;
            }
        }
    }
}
