using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
    /// <summary>
    /// This class contains a list of solution arrays for a tsp problem. These arrays represent orders for the TSPData.Cities. 
    /// A TSP Data object is required to instanciate a TSPPopulation object. 
    /// 
    /// </summary>
    public class TSPPopulation
    {
        protected TSPData _data;
        protected int[][] _population;

        /// <summary>
        /// This constuctor needs a TSPData instance to create its population. The resulting population
        /// will contain a list of several solution arrays. The content of the solutions are random unique indexes for the 
        /// TSPData.Cities.
        /// </summary>
        /// <param name="data">The TSPData object with which this population is asociated.</param>
        /// <param name="size">The size of the population, meaning the amount of solution arrays this 
        /// population shall contain.</param>
        public TSPPopulation(TSPData data, int size) 
        { 
            _data = data;
            _population = new int[size][];

            for(int i=0; i < size; i++) 
            {
                _population[i] = Enumerable.Range(0, _data.Cities.Length).ToArray();
                Random.Shared.Shuffle(_population[i]);
            }

            Debug.WriteLine($"\nPopulation with size {_population.Length} created: ");
            foreach (int[] i in _population)
            {
                Debug.Write($"\t[{i[0],4}");
                for (int j = 1; j < i.Length;j++)
                {
                    Debug.Write($",{i[j],4}");
                }
                Debug.Write($"]\n");
            }
            Debug.WriteLine("");
        }


        /// <summary>
        /// This method caluculates the total effort needed to travel from city to city for one solution in the population.
        /// 
        /// </summary>
        /// <param name="solution">The index of the solution in the population array, containing the order.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">LoadData must be run successfully, before invoking this method.</exception>
        public double CalculateEffort(int index)
        {
            if (_data == null) throw new InvalidOperationException("Data is null!");
            double sum = 0;
            double value = 0;

            foreach (int i in _population[index])
            {
                if (i != _data.Cities.Length - 1)
                {
                    value = _data.CalculateDistance(i, i + 1);
                    Console.WriteLine($"City A: ({_data.Cities[i].X,-6} : {_data.Cities[i].Y,-6}) City B: ({_data.Cities[i + 1].X,-6} : {_data.Cities[i + 1].Y,-6}) Effort: {value}");
                    sum += value;
                }
                else
                {
                    value = _data.CalculateDistance(i, 0);
                    Console.WriteLine($"City A: ({_data.Cities[i].X,-6} : {_data.Cities[i].Y,-6}) City B: ({_data.Cities[0].X,-6} : {_data.Cities[0].Y,-6}) Effort: {value}");
                    sum += value;
                }
            }

            return sum;
        }
    }
}
