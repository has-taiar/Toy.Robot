using Toy.Robot.Domain;

namespace Toy.Robot.Contracts
{
    public interface IInputParser
    {
        Command ParseCommand(string input);
        bool ShouldExit(string input);
    }
}