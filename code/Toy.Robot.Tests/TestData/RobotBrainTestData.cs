using Toy.Robot.Domain.Enums;

namespace Toy.Robot.Tests.TestData
{
    class RobotBrainTestData
    {
        static object[] RobotFallingPossiblities =
            {
                new object[] { Constants.TableWidthUnits, 1, Direction.EAST},      // facing EAST and sitting at EASTERN end of the table
                new object[] { 0, 1, Direction.WEST},                               // facing WEST and sitting at WESTERN end of the table
                new object[] { 1, Constants.TableLengthUnits, Direction.NORTH},     // facing NORTH and sitting at NORTHERN end of the table
                new object[] { Constants.TableWidthUnits, 0, Direction.SOUTH }      // facing SOUTH and sitting at SOUTHERN end of the table
            };

        static object[] RobotSafeMovingPossiblities =
        {
                // safe - middle of the table possiblities
                new object[] { 1, 1, Direction.NORTH, 1, 1+Constants.DefaultNumberOfStepsInAMove},
                new object[] { 1, 1, Direction.EAST, 1+Constants.DefaultNumberOfStepsInAMove, 1},
                new object[] { 1, 1, Direction.SOUTH, 1, 1-Constants.DefaultNumberOfStepsInAMove },
                new object[] { 1, 1, Direction.WEST, 1-Constants.DefaultNumberOfStepsInAMove,1},

                // close to edges possiblities
                new object[] { Constants.TableWidthUnits, 1, Direction.WEST, Constants.TableWidthUnits - Constants.DefaultNumberOfStepsInAMove,1},
                new object[] { Constants.TableWidthUnits, 1, Direction.NORTH, Constants.TableWidthUnits, 1+Constants.DefaultNumberOfStepsInAMove},
                new object[] { Constants.TableWidthUnits, 1, Direction.SOUTH, Constants.TableWidthUnits, 1-Constants.DefaultNumberOfStepsInAMove},
                new object[] { 1, Constants.TableLengthUnits, Direction.WEST, 1 - Constants.DefaultNumberOfStepsInAMove,Constants.TableLengthUnits},
                new object[] { 1, Constants.TableLengthUnits, Direction.EAST, 1 + Constants.DefaultNumberOfStepsInAMove, Constants.TableLengthUnits},
                new object[] { 1, Constants.TableLengthUnits, Direction.SOUTH, 1, Constants.TableLengthUnits-Constants.DefaultNumberOfStepsInAMove},
                new object[] { 0, 1, Direction.EAST, 0+Constants.DefaultNumberOfStepsInAMove,1},
                new object[] { 0, 1, Direction.NORTH, 0, 1+Constants.DefaultNumberOfStepsInAMove},
                new object[] { 0, 1, Direction.SOUTH, 0, 1-Constants.DefaultNumberOfStepsInAMove},
                new object[] { 1, 0, Direction.WEST, 1-Constants.DefaultNumberOfStepsInAMove, 0},
                new object[] { 1, 0, Direction.EAST, 1+Constants.DefaultNumberOfStepsInAMove, 0},
                new object[] { 1, 0, Direction.NORTH, 1, 0+Constants.DefaultNumberOfStepsInAMove}
            };
    }
}
