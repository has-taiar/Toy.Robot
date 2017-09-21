using Toy.Robot.Domain.Enums;

namespace Toy.Robot.Domain
{
    public class CardinalPoint
    {
        public CardinalPoint(Direction direction, Direction left, Direction right)
        {
            Direction = direction;
            Left = left;
            Right = right;
        }

        public Direction Direction { get;  }
        public Direction Left { get; }
        public Direction Right { get;  }
    }
}