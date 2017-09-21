using Toy.Robot.ErrorHandling;
using Toy.Robot.Helpers;
using Toy.Robot.Domain.Enums;
using System;

namespace Toy.Robot.Domain
{
    public class Orientation
    {
        public Orientation(string dir)
        {
            var direction = ParseGivenDirection(dir);
            this.Direction = direction; 
        }

        public Orientation(Direction direction)
        {
            Guard.ThrowIfNull(direction, nameof(direction));
            this.Direction = direction;
        }

        public Direction Direction { get; private set; }

        private Direction ParseGivenDirection(string dir)
        {
            try
            {
                return (Direction) Enum.Parse(typeof(Direction), dir, true);
            }
            catch (Exception)
            {
                throw new SimulationException(ErrorMessages.InvalidDirectionMessage); 
            }
        }        
    }
}