﻿using System.IO;
using System.Text.RegularExpressions;

namespace TSP.Service
{
    /// <summary>
    /// This object type is used to manage tsp data. It contains a list of City objects as well as a bunch of variables related to the Cities list.
    /// </summary>
    public class TSPData
    {
        public City[] Cities { get; private set; }
        public string Name { get; private set; } = "Not loaded";
        public double XSmallest { get; private set; }
        public double XLargest { get; private set; }
        public double YSmallest { get; private set; }
        public double YLargest { get; private set; }

        private double[,] _distanceMatrix;

        private static readonly string _REGEX_NUMBER = @"[+-]?(\d+\.\d+|\d+)";
        private static readonly string _REGEX_COORD  = @"^\s*\d+\s+" + _REGEX_NUMBER + @"\s+" + _REGEX_NUMBER + @"\s*$";

        /// <summary>
        /// This constructor creates the TSPData object using the content of an .tsp file. It calles LoadData passing the 
        /// name of the tsp file to be loaded.It instanciates the Cities array and constructs a distance matrix containing 
        /// the distances between each city. 
        /// </summary>
        /// <param name="tspFileName"></param>
        public TSPData(string tspFileName) 
        {
            LoadData(tspFileName);

            double sX = double.MaxValue, sY = double.MaxValue;
            double bX = double.MinValue, bY = double.MinValue;


            // Filter dimension ranges
            foreach (City city in Cities!)
            {
                if (city.X < sX) sX = city.X;
                if (city.X > bX) bX = city.X;
                if (city.Y < sY) sY = city.Y;
                if (city.Y > bY) bY = city.Y;
            }
            XSmallest = sX; YSmallest = sY;
            XLargest = bX; YLargest = bY;

            // If negative cities use of offset to make them positive
            if (XSmallest < 0 || YSmallest < 0)
            {
                foreach (City city in Cities!)
                {
                    city.X += Math.Abs(XSmallest);
                    city.Y += Math.Abs(YSmallest);
                }

                XSmallest += Math.Abs(XSmallest);
                YSmallest += Math.Abs(YSmallest);
                XLargest  += Math.Abs(XLargest);
                YLargest  += Math.Abs(YLargest);
            }


            _distanceMatrix = new double[Cities.Length,Cities.Length];
            for (int i = 0; i < _distanceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _distanceMatrix.GetLength(1); j++)
                {
                    _distanceMatrix[i,j] = CalculateDistance(Cities[i], Cities[j]);
                }
            }
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
                if (Regex.IsMatch(iterator.Current, @"^COMMENT\s?:\s?.+$")) Name = iterator.Current.Split(":")[1].Trim();
                else if (Regex.IsMatch(iterator.Current, @"^DIMENSION\s?:\s?\d+$")) Cities = new City[int.Parse(iterator.Current.Split(":")[1].Trim())];  
                
            }

            iterator = lines.GetEnumerator();
            if (Cities == null) throw new InvalidDataException($"{tspFileName}: no DIMENSION given!");

            // Create cities for each coordinate pair in file
            while (iterator.MoveNext())
            {
                if (Regex.IsMatch(iterator.Current, _REGEX_COORD)) 
                {
                    string[] parts = iterator.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    int index = int.Parse(parts[0]);
                    double x = double.Parse(parts[1]);
                    double y = double.Parse(parts[2]);

                    if((index-1) == Cities.Length) throw new InvalidDataException($"{tspFileName}: DIMENSION was {Cities.Length} but is more!");
                    else Cities[index - 1] = new City(x,y);
                }
            }

            // Check if all entries in Cities array are filled with a city
            for(int i = 0;i<Cities.Length;i++)
            {
                if (Cities[i] == null) throw new InvalidDataException($"{tspFileName}: DIMENSION was {Cities.Length} but was {i+1}!");
            }
        }


        /// <summary>
        /// This method calculates the distance between two cities using pytagoras.
        /// </summary>
        /// <param name="a">First City object</param>
        /// <param name="b">Second City object</param>
        /// <returns>Distance between the two, as double</returns>
        public static double CalculateDistance(City a, City b)
        {
            double value = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            //Console.WriteLine($"City A: ({a.X,-6} : {a.Y,-6}) City B: ({b.X,-6} : {b.Y,-6}) Effort: {value}");
            return value;
        }

        /// <summary>
        /// This method returns the distance between two cities using the distance Matrix. 
        /// </summary>
        /// <param name="a">First City object</param>
        /// <param name="b">Second City object</param>
        /// <returns>Distance between the two, as double</returns>
        public double CalculateDistance(int a, int b)
        {
            if (Cities == null) throw new InvalidOperationException("Cities is null, LoadData() must be called successfully first!");
            else if(a < 0 || a > Cities.Length) throw new ArgumentException($"Parameter a must be between 0 and {Cities.Length}, but was ${a}!");
            else if(b < 0 || b > Cities.Length) throw new ArgumentException($"Parameter b must be between 0 and {Cities.Length}, but was ${b}!");

            //Debug.WriteLine($"City A: ({Cities[a].X,-6} : {Cities[a].Y,-6}) City B: ({Cities[b].X,-6} : {Cities[b].Y,-6}) Effort: {_distanceMatrix[a, b]}");
            return _distanceMatrix[a, b];
        }
    }
}
