using Toy.Robot.ErrorHandling;
using Toy.Robot.Helpers;
using Toy.Robot.Domain.Enums;
using Toy.Robot.Contracts;

namespace Toy.Robot.Domain
{
    public class Robot : IRobot
    {
        private readonly IRobotBrain brain;
        public Location Location { get; private set; }
        public Orientation Orientation { get; private set; }

        public Robot(IRobotBrain brain)
        {
            this.brain = brain;
        }

        public string Process(Command command)
        {
            Guard.ThrowIfNull(command, nameof(command));
            if (!this.brain.CanProcessCommand(command, Location, Orientation))
                return string.Empty;

            switch (command.Type)
            {
                case CommandType.PLACE:
                    var position = brain.Place(command.CommandParameter as PlaceCommandParameter);
                    Location = position.location;
                    Orientation = position.orientation;
                    break;
                case CommandType.MOVE:
                    Location = brain.Move(Location, Orientation.Direction);
                    break;
                case CommandType.LEFT:
                    Orientation = brain.Left(Orientation.Direction);
                    break;
                case CommandType.RIGHT:
                    Orientation = brain.Right(Orientation.Direction);
                    break;
                case CommandType.REPORT:
                    return brain.Report(Location, Orientation.Direction); 
                default:
                    throw new SimulationException("Unknown command type");
            }
            return string.Empty;
        }
    }
}
