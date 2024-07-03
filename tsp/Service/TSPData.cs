using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TSP.Service
{
    /// <summary>
    /// This object type is used to manage tsp data. It contains a list of City objects as well as a bunch of variables related to the Cities list.
    /// </summary>
    public class TSPData
    {
        public City[] Cities { get; private set; }
        public string? Name { get; private set; }
        public double XSmallest { get; private set; }
        public double XLargest { get; private set; }
        public double YSmallest { get; private set; }
        public double YLargest { get; private set; }

        public double XDimension { get; private set; }
        public double YDimension { get; private set; }

        public TSPData(string tspFileName) 
        {
            LoadData(tspFileName);
        }

        /// <summary>
        /// This method has the purpose to load the tsp data of a .tsp file. 
        /// The .tsp file must at least contain a COMMENT : ".*" and a DIMENSION : "\d+" line.
        /// As well as a collection of 2 dimensional coordinates. Each tuple must be written in a seperate line starting with the index of the entry followed
        /// by two numbers seperated by one space. The coordinates must follow COMMENT and DIMESION line and the number provided by DIMENSION must match the 
        /// amount of coordinate lines.
        /// 
        /// Example:
        ///
        /// COMMENT : TSP Problem nr 1
        /// DIMENSION : 4
        /// 
        /// 1 20 10
        /// 2 123 12
        /// 3 1 34
        /// 4 522 2
        /// 
        /// </summary>
        /// <param name="tspFileName">The relative filepath of the .tsp file from the project root folder.</param>
        /// <exception cref="InvalidDataException">Is thrown if the format of the file doesn't match the description or the file cannot be found.</exception>
        private void LoadData(string tspFileName)
        {
            IEnumerable<string> lines = File.ReadLines(tspFileName);

            IEnumerator<string> iterator =  lines.GetEnumerator();

            // Filter the dimension and name of the TSP 
            while(iterator.MoveNext())
            {
                if (Regex.IsMatch(iterator.Current, @"^COMMENT : .+$")) Name = iterator.Current.Split(" : ")[1];
                else if (Regex.IsMatch(iterator.Current, @"^DIMENSION : \d+$")) Cities = new City[int.Parse(iterator.Current.Split(" : ")[1])];  
                
            }

            iterator = lines.GetEnumerator();
            if (Cities == null) throw new InvalidDataException($"{tspFileName}: no DIMENSION given!");

            // Create cities for each coordinate pair in file
            while (iterator.MoveNext())
            {
                if(Regex.IsMatch(iterator.Current, @"^\d+ \d+ \d+$"))
                {
                    int index = int.Parse(iterator.Current.Split(' ')[0]);
                    double x = double.Parse(iterator.Current.Split(' ')[1]);
                    double y = double.Parse(iterator.Current.Split(' ')[2]);

                    if((index-1) == Cities.Length) throw new InvalidDataException($"{tspFileName}: DIMENSION was {Cities.Length} but is more!");
                    else Cities[index - 1] = new City(x,y);
                }
            }

            // Check if all entries in Cities array are filled with a city
            for(int i = 0;i<Cities.Length;i++)
            {
                if (Cities[i] == null) throw new InvalidDataException($"{tspFileName}: DIMENSION was {Cities.Length} but was {i+1}!");
            }

            double sX = double.MaxValue, sY = double.MaxValue;
            double bX = double.MinValue, bY = double.MinValue;
            // Filter dimension ranges
            foreach(City city in Cities)
            {
                if (city.X < sX) sX = city.X;
                if (city.X > bX) bX = city.X;
                if (city.Y < sY) sY = city.Y;
                if (city.Y > bY) bY = city.Y;
            }
            XSmallest = sX; YSmallest = sY;
            XLargest = bX;  YLargest = bY;

            XDimension = XLargest - XSmallest;
            YDimension = YLargest - YSmallest;
        }
    }
}
