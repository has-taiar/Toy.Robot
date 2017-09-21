namespace Toy.Robot.Domain
{
    public class PlaceCommandParameter
    {
        public PlaceCommandParameter(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get;  }
        public int Y { get; }
        public string Direction { get; }
    }
}