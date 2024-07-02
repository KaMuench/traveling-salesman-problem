using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    internal class TSPDataTests
    {

        [Test]
        public void TestLoadValid()
        {
            TSPData data = TSPData.LoadData("./Resources/att48.tsp");

            Assert.That(data, !Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidNoFile()
        {
            TSPData? data = null;
            Assert.Throws<FileNotFoundException>(() => data = TSPData.LoadData("./att48.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionMissing()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = TSPData.LoadData("./Resources/invalid_0.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionToHigh()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = TSPData.LoadData("./Resources/invalid_1.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadInvalidDimensionToLow()
        {
            TSPData? data = null;
            Assert.Throws<InvalidDataException>(() => data = TSPData.LoadData("./Resources/invalid_2.tsp"));
            Assert.That(data, Is.EqualTo(null));
        }

        [Test]
        public void TestLoadCheckValues()
        {
            TSPData? data = TSPData.LoadData("./Resources/valid_0.tsp");
            Assert.That(data, !Is.EqualTo(null));
            Assert.That(data.XSmallest, Is.EqualTo(3.0));
            Assert.That(data.XLargest, Is.EqualTo(100.0));
            Assert.That(data.YSmallest, Is.EqualTo(10.0));
            Assert.That(data.YLargest, Is.EqualTo(1000));
            Assert.That(data.XDimension, Is.EqualTo(97.0));
            Assert.That(data.YDimension, Is.EqualTo(990.0));

        }

    }
}
