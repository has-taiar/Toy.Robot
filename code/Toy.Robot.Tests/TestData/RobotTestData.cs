using Toy.Robot.Domain.Enums;

namespace Toy.Robot.Tests.TestData
{
    class RobotTestData
    {
        static object[] SafeMovementScenarios =
           {
                new object[] { 1, 1, Direction.NORTH.ToString(), 1, 2 },
                new object[] { 1, 1, Direction.EAST.ToString(), 2, 1 },
                new object[] { 1, 1, Direction.WEST.ToString(), 0, 1},
                new object[] { 1, 1, Direction.SOUTH.ToString(), 1, 0}
            };

        static object[] RobotFallingScenarios =
            {
                new object[] { 0, 1, Direction.WEST.ToString()},
                new object[] { 1, 0, Direction.SOUTH.ToString()},
                new object[] { Constants.TableWidthUnits, 1, Direction.EAST.ToString()},
                new object[] { 1, Constants.TableLengthUnits, Direction.NORTH.ToString()}
            };
    }
}
