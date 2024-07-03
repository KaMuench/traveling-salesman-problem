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
        public void LoadData(string dataName)
        {
            Data = new TSPData(dataName);
            _currentSolution = Enumerable.Range(0, Data.Cities.Length).ToArray();
            _bestSolution = (int[]) _currentSolution.Clone();
        }

        /// <summary>
        /// This method calculates the distance between two cities using pytagoras.
        /// </summary>
        /// <param name="a">First City object</param>
        /// <param name="b">Second City object</param>
        /// <returns>Distance between the two, as double</returns>
        public static double CalculateDistance(City a, City b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        /// <summary>
        /// This method caluculates the total effort needed to travel from city to city in the order provided by the parameter.
        /// </summary>
        /// <param name="solution">The order of the cities, the dimension must match the Data.Cities.Length</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">LoadData must be run successfully, before invoking this method.</exception>
        public double CalculateEffort(int[] solution)
        {
            if (Data == null) throw new InvalidOperationException("Data is null! Need to call LoadData() successfully first!");
            double sum = 0;

            foreach (int i in solution)
            {
                if (i != Data.Cities.Length - 1)
                {
                    Console.WriteLine($"City A: ({Data.Cities[i].X,-6} : {Data.Cities[i].Y,-6}) City B: ({Data.Cities[i + 1].X,-6} : {Data.Cities[i + 1].Y,-6}) Effort: {CalculateDistance(Data.Cities[i], Data.Cities[i + 1])}");
                    sum += CalculateDistance(Data.Cities[i], Data.Cities[i + 1]);
                }
                else
                {
                    Console.WriteLine($"City A: ({Data.Cities[i].X,-6} : {Data.Cities[i].Y,-6}) City B: ({Data.Cities[0].X,-6} : {Data.Cities[0].Y,-6}) Effort: {CalculateDistance(Data.Cities[i], Data.Cities[0])}");
                    sum += CalculateDistance(Data.Cities[i], Data.Cities[0]);
                }
            }

            return sum;
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
