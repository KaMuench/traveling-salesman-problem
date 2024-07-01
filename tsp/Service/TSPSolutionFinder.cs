using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{

    public class TSPSolutionFinder
    {
        public TSPData? Data { get; set; }

        public void Run()
        {

        }

        public void LoadData(string dataName)
        {
            Data = TSPData.LoadData(dataName);
        }

        public static double CalculateDistance(City a, City b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
