using NUnit.Framework;
using Toy.Robot.ErrorHandling;
using Toy.Robot.Helpers;
using Toy.Robot.Domain;
using Toy.Robot.Domain.Enums;
using Toy.Robot.Tests.TestData;
using System;

namespace Toy.Robot.Tests.HelpersTests
{
    [TestFixture]
    public class InputParserTests
    {
        [Test, TestCaseSource(typeof(InputParserTestData), "ShouldExitTestScenarios")]
        public void ShouldExit_ShouldParseInputAndReturnBool(string input, bool expectedBool)
        {
            var parser = new InputParser();
            var shouldExit = parser.ShouldExit(input);
            Assert.That(shouldExit, Is.EqualTo(expectedBool));
        }

        [Test, TestCaseSource(typeof(InputParserTestData), "ValidMoveCommandsOtherThanPlaceScenarios")]
        public void ParseCommand_WhenTheInputIsAValidCommand_ShouldReturnTheExpectedCommand(string input, CommandType expectedCommandType)
        {
            var parser = new InputParser();
            var command = parser.ParseCommand(input);

            Assert.That(command, Is.Not.Null);
            Assert.That(command.Type, Is.EqualTo(expectedCommandType));
        }

        [Test, TestCaseSource(typeof(InputParserTestData), "InvalidCommandScenarios")]
        public void ParseCommand_WhenTheInputIsInvalidCommands_ShouldThrowException(string input, string expectedErrorMessage)
        {
            var parser = new InputParser();
            SimulationException exception = null;
            try
            {
                var command = parser.ParseCommand(input);
            }
            catch(SimulationException e)
            {
                exception = e;
            }

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(expectedErrorMessage));
        }

        [Test, TestCaseSource(typeof(InputParserTestData), "ValidPlaceCommandScenarios")]
        public void ParseCommand_WhenTheInputIsAValidPlaceCommand_ShouldReturnPlaceCommandWithTheRequestedPlacementPosition(string input, int expectedX, int expectedY, Direction expectedDirection)
        {
            var parser = new InputParser();
            var command = parser.ParseCommand(input);

            Assert.That(command, Is.Not.Null);
            Assert.That(command.Type, Is.EqualTo(CommandType.PLACE));

            var placementPosition = command.CommandParameter as PlaceCommandParameter;
            Assert.That(placementPosition, Is.Not.Null);
            Assert.That(placementPosition.X, Is.EqualTo(expectedX));
            Assert.That(placementPosition.Y, Is.EqualTo(expectedY));
            Assert.That(placementPosition.Direction.ToString().Equals(expectedDirection.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
