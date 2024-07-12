using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Service
{
    public class TSPPopulationFactory
    {
        public TSPData Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The TSPData object with which this population factory is asociated.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TSPPopulationFactory(TSPData data)
        {
            if(data == null) throw new ArgumentNullException("Data must not be null!");
            Data = data;
        }

        public TSPPopulation NewPopulation(int size, float mutationProbability, int mutationRange)
        {
            if (size % 2 != 0) throw new ArgumentException("size % 2 must be 0!");
            return new TSPPopulation(this, size, mutationProbability, mutationRange);
        }
    }
}
