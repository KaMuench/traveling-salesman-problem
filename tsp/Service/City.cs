using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
    public class City
    {
        private float _x;
        public float X 
        { 
            get => _x; 
            set  {
                if (value < 0) throw new ArgumentException($"Coordinate must be greater 0 but was {_x}");
                else _x = value;
            } 
        }
        private float _y;
        public float Y
        {
            get => _y;
            set
            {
                if (value < 0) throw new ArgumentException($"Coordinate must be greater 0 but was {_y}");
                else _y = value;
            }
        }

        public City(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void SetCoordinates(float x, float y)
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
