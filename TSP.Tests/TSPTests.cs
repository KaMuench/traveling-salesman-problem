

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
            tSPSolutionFinder.LoadData("./Resources/valid_0.tsp");
            tSPSolutionFinder.SetupSolution(4, 1, 0.5f);
            Assert.That(tSPSolutionFinder.GetData(),            !Is.EqualTo(null));
            Assert.That(tSPSolutionFinder.PopulationFactory,    !Is.EqualTo(null));
            Assert.That(tSPSolutionFinder.Population,           !Is.EqualTo(null));
        }

        [Test]
        public void TestLoadDataFail()
        {
            TSPSolutionFinder tSPSolutionFinder = new TSPSolutionFinder();
            Assert.Throws<InvalidOperationException>(() => tSPSolutionFinder.SetupSolution(4, 1, 0.5f));
            Assert.Throws<InvalidOperationException>(() => tSPSolutionFinder.Run(1));
        }
    }
}