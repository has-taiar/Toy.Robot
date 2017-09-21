using Moq;
using NUnit.Framework;
using Toy.Robot.Contracts;
using Toy.Robot.Domain;
using Toy.Robot.Helpers;
using System.Collections.Generic;

namespace Toy.Robot.Tests
{
    [TestFixture]
    class SimulationTests
    {
        [Test]
        public void Start_WhenGivenInvalidCommands_ContiunesToAskForValidInputUntilStopped()
        {
            var ioHelperMock = new Mock<IIoHelper>();
            var errorMessages = new List<string>();

            ioHelperMock.SetupSequence(m => m.Read())
                .Returns("invalid command")
                .Returns("place x,y,wrong-direction")
                .Returns("")
                .Returns("mov")
                .Returns("EXIT");

            ioHelperMock.Setup(m => m.Write(It.IsAny<string>()))
                .Callback((string s) => errorMessages.Add(s));

            Simulation simulation = new Simulation(new Domain.Robot(new RobotBrain()), new InputParser(), ioHelperMock.Object);
            simulation.Start();

            Assert.That(errorMessages.Count, Is.EqualTo(4), "The list of supported commands should be displayed 4 times");
        }

        [Test]
        public void Start_WhenGivenAValidSequenceOfCommands_ShouldHandleAllValidCommands()
        {
            var ioHelperMock = new Mock<IIoHelper>();
            var expectedReport = string.Empty;

            ioHelperMock.SetupSequence(m => m.Read())
                .Returns("PLACE 1,2,EAST")
                .Returns("MOVE")
                .Returns("MOVE")
                .Returns("LEFT")
                .Returns("MOVE")
                .Returns("REPORT")
                .Returns("EXIT");

            ioHelperMock.Setup(m => m.Write(It.IsAny<string>()))
                .Callback((string s) => expectedReport = s);

            Simulation simulation = new Simulation(new Domain.Robot(new RobotBrain()), new InputParser(), ioHelperMock.Object);
            simulation.Start();

            Assert.That(expectedReport, Is.EqualTo("3,3,NORTH"));
        }

        [Test]
        public void Start_WhenRobotIsNearTheEdgeOfTheTableAndGivenAMoveCommand_RobotShouldNotFall()
        {
            var ioHelperMock = new Mock<IIoHelper>();
            var expectedReport = string.Empty;

            ioHelperMock.SetupSequence(m => m.Read())
                .Returns("PLACE 0,0,NORTH")
                .Returns("LEFT")
                .Returns("MOVE")
                .Returns("REPORT")
                .Returns("EXIT");

            ioHelperMock.Setup(m => m.Write(It.IsAny<string>()))
                .Callback((string s) => expectedReport = s);

            Simulation simulation = new Simulation(new Domain.Robot(new RobotBrain()), new InputParser(), ioHelperMock.Object);
            simulation.Start();

            Assert.That(expectedReport, Is.EqualTo("0,0,WEST"));
        }
    }
}
