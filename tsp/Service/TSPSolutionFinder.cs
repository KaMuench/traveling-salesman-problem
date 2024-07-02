using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{

    public class TSPSolutionFinder
    {
        public TSPData? Data { get; private set; }

        private Queue<int[]> _solutionsQueue = new Queue<int[]>();
        private int[]? _currentSolution;
        private int[]? _bestSolution;

        public void Run()
        {
            if (Data == null) throw new InvalidOperationException("Data is null! Need to call LoadData() successfully first!");
        }

        public void LoadData(string dataName)
        {
            Data = TSPData.LoadData(dataName);

        }

        public static double CalculateDistance(City a, City b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public void PutSolution(int[] solution)
        {
            lock (_solutionsQueue)
            {
                _solutionsQueue.Enqueue(solution);
            }
        }

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
