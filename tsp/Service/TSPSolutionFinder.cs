using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{

    public class TSPSolutionFinder
    {

        public void Run()
        {

        }

        public void LoadData(string dataName)
        {

        }

        public static double CalculateDistance(City a, City b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
