using NUnit.Framework;
using Toy.Robot.ErrorHandling;
using Toy.Robot.Domain;
using Toy.Robot.Domain.Enums;
using System;
using Toy.Robot.Tests.TestData;

namespace Toy.Robot.Tests
{
    [TestFixture]
    public class RobotBrainTests
    {
        [Test]
        public void CanProcessCommand_WhenRobotIsNotOnTheTableAndCommandIsPlace_ShouldReturnTrue()
        {
            var brain = new RobotBrain();
            var canProcess = brain.CanProcessCommand(new Command(CommandType.PLACE), null, null);
            Assert.That(canProcess, Is.True);
        }

        [Test]
        public void CanProcessCommand_WhenRobotIsNotOnTheTableAndCommandIsNotPlace_ShouldReturnFalse()
        {
            var brain = new RobotBrain();

            var canMove = brain.CanProcessCommand(new Command(CommandType.MOVE), null, null);
            var canTurnLeft = brain.CanProcessCommand(new Command(CommandType.LEFT), null, null);
            var canTurnRight = brain.CanProcessCommand(new Command(CommandType.RIGHT), null, null);
            var canReport = brain.CanProcessCommand(new Command(CommandType.REPORT), null, null);

            Assert.That(canMove || canTurnLeft || canTurnRight || canReport, Is.False);
        }

        [Test]
        public void Place_WhenGivenLocationIsInvalid_ShouldThrownException()
        {
            var brain = new RobotBrain();
            SimulationException exception = null;
            try
            {
                PlaceCommandParameter placementParam = new PlaceCommandParameter(-1, -1, "north");
                brain.Place(placementParam);
            }
            catch (SimulationException e)
            {
                exception = e;
            }

            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void Place_WhenGivenDirectionIsInvalid_ShouldThrownException()
        {
            var brain = new RobotBrain();
            SimulationException exception = null;
            try
            {
                PlaceCommandParameter placementParam = new PlaceCommandParameter(1, 1, "north-ish");
                brain.Place(placementParam);
            }
            catch (SimulationException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
        }


        [Test]
        public void Place_WhenGivenPositionAndDirectionAreCorrect_ProcessCommand()
        {
            var brain = new RobotBrain();
            PlaceCommandParameter placementParam = new PlaceCommandParameter(1, 1, "north");
            var position = brain.Place(placementParam);

            Assert.That(position, Is.Not.Null);
            Assert.That(position.location.X, Is.EqualTo(placementParam.X));
            Assert.That(position.location.Y, Is.EqualTo(placementParam.Y));
            Assert.That(position.orientation.Direction.ToString().Equals(placementParam.Direction, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void Left_WhenGivenAValidCurrentDirection_ShouldReturnTheNewDirectionOnTheLeft()
        {
            var brain = new RobotBrain();
            var orientation = brain.Left(Direction.NORTH);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.WEST));

            orientation = brain.Left(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.SOUTH));

            orientation = brain.Left(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.EAST));

            orientation = brain.Left(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        public void Right_WhenGivenAValidCurrentDirection_ShouldReturnTheNewDirectionOnTheRight()
        {
            var brain = new RobotBrain();
            var orientation = brain.Right(Direction.NORTH);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.EAST));

            orientation = brain.Right(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.SOUTH));

            orientation = brain.Right(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.WEST));

            orientation = brain.Right(orientation.Direction);
            Assert.That(orientation.Direction, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        public void Move_WhenGivenAnInvalidCurrentLocation_ShouldThrowException()
        {
            var brain = new RobotBrain();
            ArgumentNullException exception = null;
            try
            {
                brain.Move(null, Direction.EAST);
            }
            catch(ArgumentNullException e)
            {
                exception = e;
            }
            Assert.That(exception, Is.Not.Null);
        }

        [Test, TestCaseSource(typeof(RobotBrainTestData),"RobotFallingPossiblities")]
        public void Move_WhenRobotIsPlacedAtTheEdgeOfTheTable_ShouldNotMoveOutsideOfTheTable(int initialX, int initialY, Direction facing)
        {
            var brain = new RobotBrain();
            var initialLocation = new Location(initialX, initialY);

            var location = brain.Move(initialLocation, facing);
            
            // should not move
            Assert.That(location.X, Is.EqualTo(initialLocation.X));
            Assert.That(location.Y, Is.EqualTo(initialLocation.Y));
        }

        [Test, TestCaseSource(typeof(RobotBrainTestData), "RobotSafeMovingPossiblities")]
        public void Move_WhenItIsSafeToMove_ShouldMove(int initialX, int initialY, Direction facing, int expectedX, int expectedY)
        {
            var brain = new RobotBrain();
            var initialLocation = new Location(initialX, initialY);

            var location = brain.Move(initialLocation, facing);

            // should not move
            Assert.That(location.X, Is.EqualTo(expectedX));
            Assert.That(location.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void Report_WhenTheGivenLocationIsInvalid_ShouldRetrunEmptryReport()
        {
            var brain = new RobotBrain();
            var report = brain.Report(null, Direction.NORTH);
            Assert.That(report, Is.Empty);
        }

        [Test]
        public void Report_WhenTheGivenLocationIsValid_ShouldRetrunTheReport()
        {
            var brain = new RobotBrain();
            Location currentLocation = new Location(1, 1);
            const Direction direction = Direction.NORTH;

            var report = brain.Report(currentLocation, direction);
            Assert.IsFalse(string.IsNullOrEmpty(report));

            var expectedReport = string.Format(Constants.DefaultReportFormat, currentLocation.X, currentLocation.Y, direction);
            Assert.That(report.Equals(expectedReport, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
