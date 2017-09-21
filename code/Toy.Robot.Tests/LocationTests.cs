using NUnit.Framework;
using Toy.Robot.ErrorHandling;
using Toy.Robot.Domain;
using Toy.Robot.Tests.TestData;

namespace Toy.Robot.Tests
{
    [TestFixture]
    public class LocationTests
    {
        [Test, TestCaseSource(typeof(LocationTestData), "InvalidInputs")]
        public void WhenLocationIsConstructedWithInvalidValues_ShouldThrowException(int x, int y)
        {
            SimulationException ex = null;
            try
            {
                var location = new Location(x, y);
            }
            catch(SimulationException e)
            {
                ex = e;
            }
            Assert.That(ex, Is.Not.Null);

        }

        [Test, TestCaseSource(typeof(LocationTestData),"ValidInputs")]
        public void WhenLocationIsConstructedWithValidValues_ShouldConstructValidLocationObjects(int x, int y)
        {
            var location = new Location(x, y);
            Assert.That(location, Is.Not.Null);
            Assert.That(location.X, Is.EqualTo(x));
            Assert.That(location.Y, Is.EqualTo(y));
        }
    }
}
