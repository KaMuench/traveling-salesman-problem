using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
    /// <summary>
    /// The purpose of this class is to be used as a storage for x and y coordinates. 
    /// </summary>
    public class City
    {

        public double X { get; set; }

        public double Y { get; set; }

        public City(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void SetCoordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X} : {Y})";
        }
    }
}
