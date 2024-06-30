

using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class TSPTests
    {

        [Test]
        public void TestCalculateDistance()
        {
            City a = new City(3,6);
            City b = new City(6,2);

            Assert.That(TSPSolutionFinder.CalculateDistance(a,b), Is.EqualTo(5.0));

            a.SetCoordinates(5,9);
            b.SetCoordinates(16,2);
            Assert.That(TSPSolutionFinder.CalculateDistance(a, b), Is.EqualTo(Math.Sqrt(170.0)));

        }
    }
}