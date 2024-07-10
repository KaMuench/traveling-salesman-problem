using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class TSPPopulationTests : TSPPopulation
    {
        public TSPPopulationTests(TSPData data, int size) : base(data, size)
        {
            _data = data;
            _population = new int[size][];

            for (int i = 0; i < size; i++)
            {
                _population[i] = Enumerable.Range(0, _data.Cities.Length).ToArray();
            }
        }

        public TSPPopulationTests() : base(null,0){ }

        [Test]
        public void TestCalculateEffort()
        {
            TSPData data = new TSPData("./Resources/valid_0.tsp");
            TSPPopulation population = new TSPPopulationTests(data, 4);
            double effort = population.CalculateEffort(0);

            // Round to 5 digits is enough to proove accuracy and correctness of caluclation
            Assert.That(Math.Round(effort, 5), Is.EqualTo(1998.38824));
        }

        [Test]
        public void TestPopulationIsNull()
        {
            TSPData data = null;
            TSPPopulation population = null;
            Assert.Throws<InvalidDataException>(()     => data = new TSPData("./Resources/invalid_0.tsp"));
            Assert.Throws<NullReferenceException>(()   => population = new TSPPopulationTests(data, 4));
        }
    }
}
