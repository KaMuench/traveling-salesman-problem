using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Service;

namespace TSP.Tests
{
    [TestFixture]
    public class CityTests
    {
        [Test]
        public void TestCityNegativeValues()
        {
            Assert.Throws<ArgumentException>(() => new City(2.0f,-1.0f));
            Assert.Throws<ArgumentException>(() => new City(-2.0f,1.0f));
            Assert.Throws<ArgumentException>(() => new City(-2.0f,-1.0f));
        }

        [Test]
        public void TestUpdatingCoordinates()
        {
            City a = new City(1.1f, 2.0f);

            Assert.That(a.X, Is.EqualTo(1.1f));
            Assert.That(a.Y, Is.EqualTo(2.0f));

            a.X = 4.42f;
            a.Y = 20.123f;

            Assert.That(a.X, Is.EqualTo(4.42f));
            Assert.That(a.Y, Is.EqualTo(20.123f));

            a.SetCoordinates(0.0f, 2.0f);

            Assert.That(a.X, Is.EqualTo(0.0f));
            Assert.That(a.Y, Is.EqualTo(2.0f));
        }
    }
}
