using Toy.Robot.Domain.Enums;

namespace Toy.Robot.Domain
{
    public class Command
    {
        public Command(CommandType type, object param = null)
        {
            this.Type = type;
            CommandParameter = param;
        }

        public CommandType Type { get; }
        public object CommandParameter { get; }
    }
}