using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    internal class TSPDataTests
    {

        [Test]
        public void TestLoadValid()
        {
            TSPData data = new TSPData("./Resources/att48.tsp");

            Assert.That(data, !Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidNoFile()
        {
            TSPData? data = null;
            Assert.Throws<FileNotFoundException>(() => data = new TSPData("./att48.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionMissing()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = new TSPData("./Resources/invalid_0.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionToHigh()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = new TSPData("./Resources/invalid_1.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionToLow()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = new TSPData("./Resources/invalid_2.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadCheckValues()
        {
            TSPData? data = new TSPData("./Resources/valid_0.tsp");
            Assert.That(data, !Is.EqualTo(null));
            Assert.That(data.XSmallest, Is.EqualTo(3.0));
            Assert.That(data.XLargest, Is.EqualTo(100.0));
            Assert.That(data.YSmallest, Is.EqualTo(10.0));
            Assert.That(data.YLargest, Is.EqualTo(1000));
            Assert.That(data.XDimension, Is.EqualTo(97.0));
            Assert.That(data.YDimension, Is.EqualTo(990.0));

        }

        [Test]
        public void TestCalculateDistance()
        {
            City a = new City(3, 6);
            City b = new City(6, 2);

            Assert.That(TSPData.CalculateDistance(a, b), Is.EqualTo(5.0));

            a.SetCoordinates(5, 9);
            b.SetCoordinates(16, 2);
            Assert.That(TSPData.CalculateDistance(a, b), Is.EqualTo(Math.Sqrt(170.0)));
        }

        [Test]
        public void TestCalculateDistance2()
        {
            TSPData data = new TSPData("./Resources/valid_0.tsp");

            Assert.That(Math.Round(data.CalculateDistance(0, 1), 3), Is.EqualTo(994.548));
            Assert.That(Math.Round(data.CalculateDistance(3, 0), 2), Is.EqualTo(172.15));
        }

    }
}
