

using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class TSPTests
    {

        [Test]
        public void TestCalculateDistance()
        {
            City a = new City(3, 6);
            City b = new City(6, 2);

            Assert.That(TSPSolutionFinder.CalculateDistance(a, b), Is.EqualTo(5.0));

            a.SetCoordinates(5, 9);
            b.SetCoordinates(16, 2);
            Assert.That(TSPSolutionFinder.CalculateDistance(a, b), Is.EqualTo(Math.Sqrt(170.0)));
        }

        [Test]
        public void TestCalculateEffort()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            tSPSolutionFinder.LoadData("./Resources/valid_0.tsp");
            double effort = tSPSolutionFinder.CalculateEffort(new int[] {0,1,2,3});

            // Round to 5 digits is enough to proove accuracy and correctness of caluclation
            Assert.That(Math.Round(effort, 5), Is.EqualTo(1998.38824));
        }

        [Test]
        public void TestCalculateEffortFail()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            Assert.Throws<InvalidDataException>(() => tSPSolutionFinder.LoadData("./Resources/invalid_0.tsp"));
            Assert.Throws<InvalidOperationException>(() => tSPSolutionFinder.CalculateEffort(new int[] { 0, 1, 2, 3 }));

        }


        [Test]
        public void TestLoadData()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            tSPSolutionFinder.LoadData("./Resources/valid_0.tsp");
            Assert.That(tSPSolutionFinder.Data, !Is.EqualTo(null));
        }

        [Test]
        public void TestLoadDataFail()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            Assert.Throws<InvalidDataException>(() => tSPSolutionFinder.LoadData("./Resources/invalid_0.tsp"));
            Assert.Throws<InvalidOperationException>(() => tSPSolutionFinder.Run());
        }
    }
}