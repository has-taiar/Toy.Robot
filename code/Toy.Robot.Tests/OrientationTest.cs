using NUnit.Framework;
using Toy.Robot.ErrorHandling;
using Toy.Robot.Domain;
using System;
using Toy.Robot.Tests.TestData;

namespace Toy.Robot.Tests
{
    [TestFixture]
    public class OrientationTest
    {
        [Test, TestCaseSource(typeof(OrientationTestData),"InvalidDirections")]
        public void WhenOrientationIsConstructedWithInvalidDirection_ShouldThrowException(string input)
        {
            SimulationException exception = null;
            try
            {
                var orientation = new Orientation(input);
            }
            catch(SimulationException e)
            {
                exception = e;
            }

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(ErrorMessages.InvalidDirectionMessage));
        }

        [Test, TestCaseSource(typeof(OrientationTestData), "ValidDirections")]
        public void WhenOrientationIsConstructedWithValidDirection_ShouldCreateOrientationObject(string input)
        {
            var orientation = new Orientation(input);
            Assert.That(orientation, Is.Not.Null);
            Assert.That(orientation.Direction.ToString().Equals(input, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
