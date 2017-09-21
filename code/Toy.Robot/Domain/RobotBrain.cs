using Toy.Robot.ErrorHandling;
using Toy.Robot.Helpers;
using Toy.Robot.Domain.Enums;
using System.Collections.Generic;
using Toy.Robot.Contracts;

namespace Toy.Robot.Domain
{
    public class RobotBrain : IRobotBrain
    {
        public bool CanProcessCommand(Command command, Location location, Orientation orientation)
        {
            bool robotHasStarted = location != null && orientation != null;
            return robotHasStarted || command.Type == CommandType.PLACE;
        }

        public Orientation Left(Direction currentDirection)
        {
            var direction = _directions[currentDirection].Left;
            return new Orientation(direction);
        }

        public Orientation Right(Direction currentDirection)
        {
            var direction = _directions[currentDirection].Right;
            return new Orientation(direction);
        }

        public Location Move(Location currentLocation, Direction currentDirection)
        {
            Guard.ThrowIfNull(currentLocation, nameof(currentLocation));
            switch (currentDirection)
            {
                case Direction.NORTH:
                    var northY = currentLocation.Y + Constants.DefaultNumberOfStepsInAMove;
                    return IsItSafeToMove(northY, Constants.TableLengthUnits) ? new Location(currentLocation.X, northY) : currentLocation;
                case Direction.EAST:
                    var eastX = currentLocation.X + Constants.DefaultNumberOfStepsInAMove;
                    return IsItSafeToMove(eastX, Constants.TableWidthUnits) ? new Location(eastX, currentLocation.Y) : currentLocation;
                case Direction.WEST:
                    var westX = currentLocation.X - Constants.DefaultNumberOfStepsInAMove;
                    return IsItSafeToMove(westX, Constants.TableWidthUnits) ? new Location(westX, currentLocation.Y) :  currentLocation;
                case Direction.SOUTH:
                    var southY = currentLocation.Y - Constants.DefaultNumberOfStepsInAMove;
                    return IsItSafeToMove(southY, Constants.TableLengthUnits) ? new Location(currentLocation.X, southY) : currentLocation;

                default:
                    throw new SimulationException("unknown cardinal direction");
            }
        }

        public (Location location, Orientation orientation) Place(PlaceCommandParameter requestedPosition)
        {
            Guard.ThrowIfNull(requestedPosition, nameof(requestedPosition));

            var location = new Location(requestedPosition.X, requestedPosition.Y);
            var orientation = new Orientation(requestedPosition.Direction);
            return (location, orientation);
        }

        public string Report(Location currentLocation, Direction currentDirection)
        {
            if (currentLocation == null) return string.Empty;

            var report = string.Format(Constants.DefaultReportFormat, currentLocation.X, currentLocation.Y, currentDirection.ToString());
            return report;
        }

        private bool IsItSafeToMove(int newLocation, int tableDimension)
        {
            return (newLocation < 0 || newLocation > tableDimension) ? false : true;
        }

        private Dictionary<Direction, CardinalPoint> _directions = new Dictionary<Direction, CardinalPoint>()
        {
            { Direction.NORTH ,  new CardinalPoint(Direction.NORTH, Direction.WEST, Direction.EAST) },
            { Direction.EAST,  new CardinalPoint(Direction.EAST, Direction.NORTH, Direction.SOUTH) },
            { Direction.SOUTH,  new CardinalPoint(Direction.SOUTH, Direction.EAST, Direction.WEST) },
            { Direction.WEST, new CardinalPoint(Direction.WEST, Direction.SOUTH, Direction.NORTH) }
        };
    }
}
