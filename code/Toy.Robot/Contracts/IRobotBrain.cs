using Toy.Robot.Domain;
using Toy.Robot.Domain.Enums;

namespace Toy.Robot.Contracts
{
    public interface IRobotBrain
    {
        (Location location, Orientation orientation) Place(PlaceCommandParameter param);
        Orientation Left(Direction currentDirection);
        Orientation Right(Direction currentDirection);
        string Report(Location currentLocation, Direction currentDirection);
        Location Move(Location currentLocation, Direction currentDirection);
        bool CanProcessCommand(Command command, Location location, Orientation orientation);
    }
}