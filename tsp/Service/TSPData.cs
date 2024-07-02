using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TSP.Service
{
    public class TSPData
    {
        private City[]? _cities;

        public City[]? Cities { get; private set; }
        public string? Name { get; private set; }
        public double XSmallest { get; private set; }
        public double XLargest { get; private set; }
        public double YSmallest { get; private set; }
        public double YLargest { get; private set; }

        public double XDimension { get; private set; }
        public double YDimension { get; private set; }

        private TSPData() { }

        public static TSPData LoadData(string tspFileName)
        {
            TSPData data = new TSPData();
            IEnumerable<string> lines = File.ReadLines(tspFileName);

            IEnumerator<string> iterator =  lines.GetEnumerator();

            // Filter the dimension and name of the TSP 
            while(iterator.MoveNext())
            {
                if (Regex.IsMatch(iterator.Current, @"^COMMENT : .+$")) data.Name = iterator.Current.Split(" : ")[1];
                else if (Regex.IsMatch(iterator.Current, @"^DIMENSION : \d+$")) data.Cities = new City[int.Parse(iterator.Current.Split(" : ")[1])];  
                
            }

            iterator = lines.GetEnumerator();
            if (data.Cities == null) throw new InvalidDataException($"{tspFileName}: no DIMENSION given!");

            // Create cities for each coordinate pair in file
            while (iterator.MoveNext())
            {
                if(Regex.IsMatch(iterator.Current, @"^\d+ \d+ \d+$"))
                {
                    int index = int.Parse(iterator.Current.Split(' ')[0]);
                    double x = double.Parse(iterator.Current.Split(' ')[1]);
                    double y = double.Parse(iterator.Current.Split(' ')[2]);

                    if((index-1) == data.Cities.Length) throw new InvalidDataException($"{tspFileName}: DIMENSION was {data.Cities.Length} but is more!");
                    else data.Cities[index - 1] = new City(x,y);
                }
            }

            // Check if all entries in Cities array are filled with a city
            for(int i = 0;i<data.Cities.Length;i++)
            {
                if (data.Cities[i] == null) throw new InvalidDataException($"{tspFileName}: DIMENSION was {data.Cities.Length} but was {i+1}!");
            }

            double sX = double.MaxValue, sY = double.MaxValue;
            double bX = double.MinValue, bY = double.MinValue;
            // Filter dimension ranges
            foreach(City city in data.Cities)
            {
                if (city.X < sX) sX = city.X;
                if (city.X > bX) bX = city.X;
                if (city.Y < sY) sY = city.Y;
                if (city.Y > bY) bY = city.Y;
            }
            data.XSmallest = sX; data.YSmallest = sY;
            data.XLargest = bX;  data.YLargest = bY;

            data.XDimension = data.XLargest - data.XSmallest;
            data.YDimension = data.YLargest - data.YSmallest;

            
            return data;
        }
    }
}
