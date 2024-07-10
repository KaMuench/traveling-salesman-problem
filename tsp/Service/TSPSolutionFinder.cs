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
        /// <summary>
        /// The list containing the City objects for the tsp. The coordinates of these objects are used to run the calculations.
        /// </summary>
        public TSPData? Data { get; private set; }
        public TSPPopulation? Population { get; private set; }

        private Queue<int[]> _solutionsQueue = new Queue<int[]>();
        private int[]? _currentSolution;
        private int[]? _bestSolution;


        /// <summary>
        /// This method starts the caluclation. 
        /// </summary>
        /// <exception cref="InvalidOperationException">LoadData must be run successfully, before invoking this method.</exception>
        public void Run()
        {
            if (Data == null) throw new InvalidOperationException("Data is null! Need to call LoadData() successfully first!");
        }

        /// <summary>
        /// This method has the purpose of creating a TSPData instance ready to be used. It also setsup the arrays needed for the calculation.
        /// </summary>
        /// <param name="dataName">The relative path to the .tsp file to set up the data with. The path must be relative to the project root folder.</param>
        public void SetupSolution(string dataName, int populationSize)
        {
            Data = new TSPData(dataName);
            Population = new TSPPopulation(Data, populationSize);

            _currentSolution = Enumerable.Range(0, Data.Cities.Length).ToArray();
            _bestSolution = (int[]) _currentSolution.Clone();
        }


        /// <summary>
        /// Method to put a newly calculated solution (order) in the solutions fifo queue at the bottom. The method provides syncronized write access.
        /// </summary>
        /// <param name="solution">The solution to be added to the queue</param>
        public void PutSolution(int[] solution)
        {
            lock (_solutionsQueue)
            {
                _solutionsQueue.Enqueue(solution);
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
