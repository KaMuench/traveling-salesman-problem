using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TSP.Service
{
    /// <summary>
    /// This class contains a list of solution arrays for a tsp problem. These arrays represent orders for the TSPData.Cities. 
    /// A TSP Data object is required to instanciate a TSPPopulation object. 
    /// 
    /// </summary>
    public class TSPPopulation
    {
        private readonly int _SIZE_SOLUTION;
        private readonly TSPPopulationFactory _factory;
        private int[][] _population;
        private Random rand = new Random();

        public float MutationPropability { get; set; }
        public int MutationRange { get; set; }

        /// <summary>
        /// This constuctor needs a TSPData instance to create its population. The resulting population
        /// will contain a list of several solution arrays. The content of the solutions are random unique indexes for the 
        /// TSPData.Cities.
        /// </summary>
        /// 
        /// <param name="size">The size of the population, meaning the amount of solution arrays this population shall contain.</param>
        /// <param name="factory">The factory used to create this population</param>
        /// <param name="mutationPropability">The propability of mutation happening.</param>
        /// <param name="mutationRange">The range in which the gene swap is done. </param>
        public TSPPopulation(TSPPopulationFactory factory, int size, float mutationPropability, int mutationRange)
        {
            _factory = factory;
            _population = new int[size][];
            _SIZE_SOLUTION = _factory.Data.Cities.Length;
            MutationPropability = mutationPropability;

            if (mutationRange < 1 || mutationRange >= _SIZE_SOLUTION) throw new ArgumentOutOfRangeException($"mutationRange must be greater than 0 and smaller than {_SIZE_SOLUTION}! Was {mutationRange}");
            else MutationRange = mutationRange;


            for (int i = 0; i < size; i++)
            {
                _population[i] = Enumerable.Range(0, _SIZE_SOLUTION).ToArray();
                Random.Shared.Shuffle(_population[i]);
            }

            Debug.WriteLine($"\nPopulation with size {_population.Length} created: ");
            foreach (int[] i in _population)
            {
                Debug.Write($"\t[{i[0],4}");
                for (int j = 1; j < i.Length; j++)
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
        /// <returns>The total effort for one solution at index "index"</returns>
        public double CalculateEffort(int index)
        {
            double sum = 0;
            double value = 0;

            int indexFirst = 0;
            int indexSecond = 0;

            for (int i = 0; i < _SIZE_SOLUTION; i++)
            {
                indexFirst = _population[index][i];
                if (i != _SIZE_SOLUTION - 1)
                {
                    indexSecond = _population[index][i + 1];

                    value = _factory.Data.CalculateDistance(indexFirst, indexSecond);
                    Console.WriteLine($"City A: ({_factory.Data.Cities[indexFirst].X,-6} : {_factory.Data.Cities[indexFirst].Y,-6}) City B: ({_factory.Data.Cities[indexSecond].X,-6} : {_factory.Data.Cities[indexSecond].Y,-6}) Effort: {value}");
                    sum += value;
                }
                else
                {
                    indexSecond = _population[index][0];

                    value = _factory.Data.CalculateDistance(indexFirst, indexSecond);
                    Console.WriteLine($"City A: ({_factory.Data.Cities[indexFirst].X,-6} : {_factory.Data.Cities[indexFirst].Y,-6}) City B: ({_factory.Data.Cities[indexSecond].X,-6} : {_factory.Data.Cities[indexSecond].Y,-6}) Effort: {value}");
                    sum += value;
                }
            }

            return sum;
        }
        
        
        /// <summary>
        /// This method executes a mutation on the solution arrays. The mutation means randomly swapping values inside a solution array.
        /// The <see cref="MutationPropability"/> determines how often a mutation might occure. For each value inside a solution array,
        /// this propability is used to decide if the value is swapped. The <see cref="MutationRange"/> determines with which other value
        /// the first one is swapped with. For example a range 1 means the value is swapped with its right neighbour, a 5 means it is swapped with
        /// the value five indices further to the right. If the index + range exceeds the size of the solution array, the modulo of (index + range) % array.Length
        /// is used.
        /// </summary>
        public void Mutate()
        {
            int value = -1;
            foreach (int[] array in _population)
            {
                for (int i = 0; i < _SIZE_SOLUTION; i++)
                {
                    if (rand.NextSingle() <= MutationPropability)
                    {
                        value = array[i];
                        array[i] = array[(i + MutationRange) % _SIZE_SOLUTION];
                        array[(i + MutationRange) % _SIZE_SOLUTION] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Copies the first half of the _population array into the first half of one array
        /// and copies the second half of the _population array into the second half of the other array.
        /// Then it fills up the remaining entries of firstGroup[l] array with the values of _population[i+1] 
        /// which are not presented in the firstGroup[l] yet, in the order they appear inside the _population[i+1] array.
        /// Same goes with secondGroup[l] array. Then, firstGroup[l] is copied into _population[i] and secondGroup[l] into _population[i+1].
        /// 
        /// Example:
        /// 
        /// _population[i] = [0, 1, 2, 3, 4]
        /// firstGroup[l]  = [0, 1, -, -, -]
        /// secondGroup[l] = [-, -, 2, 3, 4]
        /// 
        /// population[i + 1]   = [3, 2, 1, 0, 4]
        /// firstGroup[l]       = [0, 1, 3, 2, 4]
        /// secondGroup[l]      = [1, 0, 2, 3, 4]
        /// 
        /// _population[i]   = [0, 1, 3, 2, 4]
        /// _population[i+1] = [1, 0, 2, 3, 4]
        /// 
        /// 
        /// </summary>
        public void CrossOver()
        {
            int[][] firstGroup = new int[_population.Length / 2][];
            int[][] secondGroup = new int[_population.Length / 2][];

            for (int i = 0; i < _population.Length / 2; i++)
            {
                firstGroup[i] = Enumerable.Repeat(-1, _population[i].Length).ToArray();
                secondGroup[i] = Enumerable.Repeat(-1, _population[i].Length).ToArray();
            }
            for (int solIndex = 0, newSolIndex = 0, length = 0; solIndex < _population.Length; solIndex += 2, newSolIndex++)
            {
                length = _SIZE_SOLUTION / 2;
                Array.Copy(_population[solIndex], 0, firstGroup[newSolIndex], 0, length);
                Array.Copy(_population[solIndex], length, secondGroup[newSolIndex], length, _SIZE_SOLUTION - length);

                bool inFirstGroup = false;
                // For each value in _population[i+1] which is present in firstGroup[l], put it in the secondGroup
                for (int indexOld = 0, indexSecond = 0, indexFirst = length; indexOld < _SIZE_SOLUTION; indexOld++)
                {
                    for (int valueNew = 0; valueNew < length; valueNew++)
                    {
                        // If _population[i+1][j] is already in firstGroup[l] look for next value in _population
                        if (_population[solIndex + 1][indexOld] == firstGroup[newSolIndex][valueNew])
                        {
                            secondGroup[newSolIndex][indexSecond] = _population[solIndex + 1][indexOld];
                            indexSecond++;
                            inFirstGroup = true;
                            break;
                        }
                    }

                    if (!inFirstGroup)
                    {
                        firstGroup[newSolIndex][indexFirst] = _population[solIndex + 1][indexOld];
                        indexFirst++;
                    }
                    inFirstGroup = false;
                }

                Array.Copy(firstGroup[newSolIndex], _population[solIndex], _SIZE_SOLUTION);
                Array.Copy(secondGroup[newSolIndex], _population[solIndex + 1], _SIZE_SOLUTION);
            }
        }

        public int[] GetSolutionCopy(int index)
        {
            return (int[])_population[index].Clone();
        }

        public void SetPopulation(int[][] solutions)
        {
            if (solutions.Length != _population.Length) throw new ArgumentException($"solutions.Length must match _population.Length! Expected {_population.Length} was {solutions.Length} ");

            foreach (int[] i in solutions)
            {
                if (i.Length != _SIZE_SOLUTION) throw new ArgumentException($"The length of each inner array must be: {_SIZE_SOLUTION}");
                HashSet<int> set = new HashSet<int>();
                foreach (var item in i)
                {
                    if (set.Contains(item)) throw new ArgumentException($"Solution at index {i} contains multiple values of the same kind! Value: {item}");
                    else set.Add(item);
                    if (item < 0 || item >= _SIZE_SOLUTION) throw new ArgumentException($"Solution at index {i} contains {item}, must be between 0 and {_SIZE_SOLUTION}");

                }
            }

            _population = solutions;
        }
    }
}
