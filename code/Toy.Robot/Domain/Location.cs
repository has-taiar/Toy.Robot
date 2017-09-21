using Toy.Robot.ErrorHandling;

namespace Toy.Robot.Domain
{
    public class Location
    {
        public Location(int x, int y)
        {
            if (x < 0 || x > Constants.TableWidthUnits || 
                y < 0 || y > Constants.TableLengthUnits)
                throw new SimulationException(ErrorMessages.InvalidLocationParameters);

            X = x;
            Y = y;
        }

        public int X { get;  }
        public int Y { get; }
    }
}