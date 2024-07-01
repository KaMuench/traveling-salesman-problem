using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
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
