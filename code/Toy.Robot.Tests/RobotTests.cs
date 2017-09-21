using NUnit.Framework;
using Toy.Robot.ErrorHandling;
using Toy.Robot.Domain.Enums;
using System;
using Toy.Robot.Tests.TestData;
using Toy.Robot.Domain;

namespace Toy.Robot.Tests
{
   [TestFixture]
   public class RobotTests
   {
        [Test]
        public void WhenRobotGetsANullCommand_ShouldThrowException()
        {
            ArgumentNullException exception = null;
            var robot = new Domain.Robot(new Domain.RobotBrain());
            try
            {
                robot.Process(null);
            }
            catch(ArgumentNullException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void WhenRobotIsNotOnTheTable_ShouldIgnoreAllCommandsOtherThanPlace()
        {
            var robot = new Domain.Robot(new Domain.RobotBrain());
            Exception ex = null;
            try
            {
                robot.Process(new Domain.Command(CommandType.MOVE));
                robot.Process(new Domain.Command(CommandType.LEFT));
                robot.Process(new Domain.Command(CommandType.RIGHT));
                var report = robot.Process(new Domain.Command(CommandType.REPORT));
                Assert.That(report, Is.EqualTo(string.Empty));
            }
            catch(Exception e)
            {
                ex = e;
            }
            Assert.That(ex, Is.Null);
        }

        [Test]
        public void WhenRobotIsNotOnTheTableAndAValidPlaceCommandIsGiven_ShouldProcessPlaceCommand()
        {
            var robot = new Domain.Robot(new Domain.RobotBrain());
            Domain.PlaceCommandParameter placementParam = new Domain.PlaceCommandParameter(0,0,Direction.NORTH.ToString());
            robot.Process(new Domain.Command(CommandType.PLACE, placementParam));

            Assert.That(robot.Location, Is.Not.Null);
            Assert.That(robot.Orientation, Is.Not.Null);

            Assert.That(robot.Location.X, Is.EqualTo(placementParam.X));
            Assert.That(robot.Location.Y, Is.EqualTo(placementParam.Y));
            Assert.That(robot.Orientation.Direction.ToString().Equals(placementParam.Direction, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void ProcessPlaceCommand_WhenCommandParamIsInvalid_ShouldIgnorePlaceCommand()
        {
            var robot = new Domain.Robot(new Domain.RobotBrain());
            ArgumentNullException exception = null;
            try
            {
                robot.Process(new Domain.Command(CommandType.PLACE, null));
            }
            catch(ArgumentNullException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void ProcessPlaceCommand_WhenCommandHasInvalidLocation_ShouldIgnorePlaceCommand()
        {
            var robot = new Domain.Robot(new Domain.RobotBrain());
            SimulationException exception = null;
            try
            {
                Domain.PlaceCommandParameter placementParam = new PlaceCommandParameter(-1, -1, Direction.NORTH.ToString());
                robot.Process(new Domain.Command(CommandType.PLACE, placementParam));
            }
            catch (SimulationException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(ErrorMessages.InvalidLocationParameters));
        }

        [Test]
        public void ProcessPlaceCommand_WhenCommandHasInvalidDirection_ShouldIgnorePlaceCommand()
        {
            var robot = new Domain.Robot(new RobotBrain());
            SimulationException exception = null;
            try
            {
                PlaceCommandParameter placementParam = new PlaceCommandParameter(1, 1, "north-ish");
                robot.Process(new Command(CommandType.PLACE, placementParam));
            }
            catch (SimulationException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(ErrorMessages.InvalidDirectionMessage));
        }

        [Test]
        public void ProcessLeftCommand_WhenRobotIsPlacedOnTheTable_ShouldTurnTheRobotToTheLeft()
        {
            var robot = new Domain.Robot(new RobotBrain());
            PlaceCommandParameter initialPlacement = new PlaceCommandParameter(0, 0, Direction.NORTH.ToString());
            robot.Process(new Command(CommandType.PLACE, initialPlacement));

            robot.Process(new Command(CommandType.LEFT));
            Assert.That(robot.Orientation.Direction, Is.EqualTo(Direction.WEST));
        }

        [Test]
        public void ProcessRightCommand_WhenRobotIsPlacedOnTheTable_ShouldTurnTheRobotToTheRight()
        {
            var robot = new Domain.Robot(new RobotBrain());
            PlaceCommandParameter initialPlacement = new PlaceCommandParameter(0, 0, Direction.NORTH.ToString());
            robot.Process(new Command(CommandType.PLACE, initialPlacement));

            robot.Process(new Command(CommandType.RIGHT));
            Assert.That(robot.Orientation.Direction, Is.EqualTo(Direction.EAST));
        }

        [Test, TestCaseSource(typeof(RobotTestData),"SafeMovementScenarios")]
        public void ProcessMoveCommand_WhenItIsSafeToMove_ShouldMove(int initialX, int initialY, string facing, int expectedX, int expectedY)
        {
            var robot = new Domain.Robot(new RobotBrain());
            robot.Process(new Command(CommandType.PLACE, new PlaceCommandParameter(initialX, initialY, facing)));

            robot.Process(new Command(CommandType.MOVE));

            Assert.That(robot.Location.X, Is.EqualTo(expectedX));
            Assert.That(robot.Location.Y, Is.EqualTo(expectedY));
            Assert.That(robot.Orientation.Direction.ToString().Equals(facing, StringComparison.CurrentCultureIgnoreCase) , "Direction should not change in the movement");
        }

        [Test, TestCaseSource(typeof(RobotTestData),"RobotFallingScenarios")]
        public void ProcessMoveCommand_WhenTheMovementCouldCauseTheRobotToFall_ShouldNotMove(int initialX, int initialY, string facing)
        {
            var robot = new Domain.Robot(new RobotBrain());
            robot.Process(new Command(CommandType.PLACE, new PlaceCommandParameter(initialX, initialY, facing)));

            robot.Process(new Command(CommandType.MOVE));

            Assert.That(robot.Location.X, Is.EqualTo(initialX), "Robot should not move/fall");
            Assert.That(robot.Location.Y, Is.EqualTo(initialY), "Robot should not move/fall");
            Assert.That(robot.Orientation.Direction.ToString().Equals(facing, StringComparison.CurrentCultureIgnoreCase), "Direction should not change in the movement");
        }

        [Test]
        public void ProcessReportCommand_WhenTheRobotIsPlacedOnTheTable_ShouldReturnReport()
        {
            var robot = new Domain.Robot(new RobotBrain());
            robot.Process(new Command(CommandType.PLACE, new PlaceCommandParameter(1, 1, Direction.NORTH.ToString())));

            var report = robot.Process(new Command(CommandType.REPORT));

            var expectedReport = string.Format(Constants.DefaultReportFormat, robot.Location.X, robot.Location.Y, robot.Orientation.Direction);
            Assert.That(report.Equals(expectedReport, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
