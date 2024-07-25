using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class TSPPopulationTests
    {

        Dictionary<string, double> dict = new Dictionary<string, double>();

        [Test]
        public void TestCalculateEffort()
        {
            dict.Add("0123", 1998.388); dict.Add("1230", 1998.388); dict.Add("2301", 1998.388); dict.Add("3012", 1998.388);
            dict.Add("0132", 3427.070); dict.Add("1320", 3427.070); dict.Add("3201", 3427.070); dict.Add("2013", 3427.070);
            dict.Add("0213", 1999.098); dict.Add("2130", 1999.098); dict.Add("1302", 1999.098); dict.Add("3021", 1999.098);
            dict.Add("0231", 3427.070); dict.Add("2310", 3427.070); dict.Add("3102", 3427.070); dict.Add("1023", 3427.070);
            dict.Add("0312", 1999.026); dict.Add("3120", 1999.026); dict.Add("1203", 1999.026); dict.Add("2031", 1999.026);
            dict.Add("0321", 1998.388); dict.Add("3210", 1998.388); dict.Add("2103", 1998.388); dict.Add("1032", 1998.388);

            TSPData data = new TSPData("./Resources/valid_0.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            double effort = population.CalculateEffort(0);
            StringBuilder key = new StringBuilder();

            foreach (int i in population.GetSolutionCopy(0))
            {
                key.Append(i);
            }

            // Round to 5 digits is enough to proove accuracy and correctness of caluclation
            Assert.That(Math.Round(effort, 3), Is.EqualTo(dict.GetValueOrDefault(key.ToString())), $"Order was {key.ToString()}");
        }

        [Test]
        public void TestPopulationIsNull()
        {
            TSPData data = null;
            TSPPopulation population = null;
            TSPPopulationFactory factory = null;
            Assert.Throws<InvalidDataException>(() => data = new TSPData("./Resources/invalid_0.tsp"));
            Assert.Throws<ArgumentNullException>(() => factory = new TSPPopulationFactory(data));
            Assert.Throws<NullReferenceException>(() => population = factory.NewPopulation(4, 0, 1));
        }

        [Test]
        public void TestPopulationSizeFail()
        {
            TSPData data = new TSPData("./Resources/valid_0.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            Assert.Throws<ArgumentException>(() => factory.NewPopulation(3, 0, 1));
        }

        [Test]
        public void TestCrossOverEven()
        {
            TSPData data = new TSPData("./Resources/valid_0.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);


            int[][] solutions = new int[][]
            {
                [3,1,2,0],
                [2,1,3,0],
                [1,2,3,0],
                [0,3,2,1]
            };


            int[][] solutionsnew = new int[][]
            {
                [1,2,0,3],
                [1,2,3,0],
                [1,2,3,0],
                [0,3,2,1]
            };

            population.SetPopulation(solutions);
            population.CrossOver();

            for(int i=0;i<solutions.Length;i++)
            {
                Assert.That(population.GetSolutionCopy(i), Is.EquivalentTo(solutionsnew[i]));
            }
        }

        [Test]
        public void TestCrossOverOdd()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);


            int[][] solutions = new int[][]
            {
                [3,4,1,2,0],
                [2,1,4,3,0],
                [4,1,2,3,0],
                [0,3,4,2,1]
            };


            int[][] solutionsnew = new int[][]
            {
                [3,4,1,2,0],
                [2,1,4,3,0],
                [4,1,2,3,0],
                [0,3,4,2,1]
            };

            population.SetPopulation(solutions);
            population.CrossOver();

            for (int i = 0; i < solutions.Length; i++)
            {
                Assert.That(population.GetSolutionCopy(i), Is.EquivalentTo(solutionsnew[i]));
            }
        }


        [Test]
        public void TestSetPopulationNull()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            Assert.Throws<NullReferenceException>(() => population.SetPopulation(null));
        }

        [Test]
        public void TestSetPopulationInvalidSolutionsCount()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            int[][] solutions = new int[][]
            {
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4]
            };
            var ex = Assert.Throws<ArgumentException>(() => population.SetPopulation(solutions));
            Assert.That(Regex.IsMatch(ex.Message, @"solutions\.Length.*"));
        }


        [Test]
        public void TestSetPopulationInvalidSolutionRange()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            int[][] solutions = new int[][]
            {
                [0,1,2,3,4],
                [0,1,2,3],
                [0,1,2,3,4],
                [0,1,2,3,4]
            };
            var ex = Assert.Throws<ArgumentException>(() => population.SetPopulation(solutions));
            Assert.That(Regex.IsMatch(ex.Message, @"The length.*"));
        }

        [Test]
        public void TestSetPopulationValuesOutOfRange()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            int[][] solutions =
            [
                [0,1,2,3,4],
                [0,1,2,3,5],
                [0,1,2,3,4],
                [0,1,2,3,4]
            ];
            var ex = Assert.Throws<ArgumentException>(() => population.SetPopulation(solutions));
            Assert.That(Regex.IsMatch(ex.Message, @".*must be between.*"));

            int[][] solutions2 =
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,-1,3,4]
             ];

            ex = Assert.Throws<ArgumentException>(() => population.SetPopulation(solutions2));
            Assert.That(Regex.IsMatch(ex.Message, @".*must be between.*"));
        }

        [Test]
        public void TestSetPopulationDublicateValue()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);

            int[][] solutions = 
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,0,3,4]
            ];
            var ex = Assert.Throws<ArgumentException>(() => population.SetPopulation(solutions));
            Assert.That(Regex.IsMatch(ex.Message, @".*contains multiple values of the same kind.*"));
        }

        [Test]
        public void TestMutation1()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 1, 1);

            int[][] solutions = 
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4]
            ];

            int[][] solutions2 =
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
            ];

            population.SetPopulation(solutions);
            population.Mutate();

            Assert.That(solutions2[0], Is.EquivalentTo(population.GetSolutionCopy(0)));
        }

        [Test]
        public void TestMutation2()
        {
            TSPData data = new TSPData("./Resources/valid_1.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0.5f, 1);

            int[][] solutions =
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4]
            ];

            int[][] solutions2 =
            [
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
                [0,1,2,3,4],
            ];


            population.SetPopulation(solutions);
            population.Mutate();

            Assert.That(solutions2[0], Is.EquivalentTo(population.GetSolutionCopy(0)));
        }

        [Test]
        public void TestGetBestSol() 
        {
            TSPData data = new TSPData("./Resources/valid_0.tsp");
            TSPPopulationFactory factory = new TSPPopulationFactory(data);
            TSPPopulation population = factory.NewPopulation(4, 0, 1);


            int[][] solutions = new int[][]
            {
                [3,1,2,0],
                [2,1,3,0],
                [1,2,3,0],
                [0,3,2,1]
            };

            population.SetPopulation(solutions);
            Assert.That(population.GetBestSolution(), Is.EqualTo(2));
            Assert.That(population.GetBestSolution(), Is.InRange(0, population.PopulationSize));
        }


    }
}
