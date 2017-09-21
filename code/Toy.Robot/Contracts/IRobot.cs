using Toy.Robot.Domain;

namespace Toy.Robot.Contracts
{
    public interface IRobot
    {
        Location Location { get; }
        Orientation Orientation { get; }

        string Process(Command command);
    }
}