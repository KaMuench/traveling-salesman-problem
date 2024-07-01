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
        public void TestUpdatingCoordinates()
        {
            City a = new City(1.1, 2.0);

            Assert.That(a.X, Is.EqualTo(1.1));
            Assert.That(a.Y, Is.EqualTo(2.0));

            a.X = 4.42;
            a.Y = 20.123;

            Assert.That(a.X, Is.EqualTo(4.42));
            Assert.That(a.Y, Is.EqualTo(20.123));

            a.SetCoordinates(0.0, 2.0);

            Assert.That(a.X, Is.EqualTo(0.0));
            Assert.That(a.Y, Is.EqualTo(2.0));
        }
    }
}
