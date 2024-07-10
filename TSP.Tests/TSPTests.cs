

using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class TSPTests
    {


        [Test]
        public void TestLoadData()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            tSPSolutionFinder.SetupSolution("./Resources/valid_0.tsp", 4);
            Assert.That(tSPSolutionFinder.Data, !Is.EqualTo(null));
        }

        [Test]
        public void TestLoadDataFail()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            Assert.Throws<InvalidDataException>(() => tSPSolutionFinder.SetupSolution("./Resources/invalid_0.tsp", 4));
            Assert.Throws<InvalidOperationException>(() => tSPSolutionFinder.Run());
        }
    }
}