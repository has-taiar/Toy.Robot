using Toy.Robot.Domain.Enums;
using Toy.Robot.ErrorHandling;

namespace Toy.Robot.Tests.TestData
{
    class InputParserTestData
    {
        static object[] ShouldExitTestScenarios =
        {
            new object[] { "exit", true },
            new object[] { "EXIT", true },
            new object[] { "eXIt", true },
            new object[] { "", false },
            new object[] { " ", false },
            new object[] { "something else", false }
        };

        static object[] ValidMoveCommandsOtherThanPlaceScenarios =
        {
            new object[] { "move" , CommandType.MOVE},
            new object[] { "MOVE", CommandType.MOVE},
            new object[] { "mOVe", CommandType.MOVE},
            new object[] { "left" , CommandType.LEFT},
            new object[] { "LEFT", CommandType.LEFT},
            new object[] { "leFT" , CommandType.LEFT},
            new object[] { "right", CommandType.RIGHT },
            new object[] { "RIGHT", CommandType.RIGHT},
            new object[] { "riGHt", CommandType.RIGHT},
            new object[] { "report", CommandType.REPORT },
            new object[] { "REPORT", CommandType.REPORT},
            new object[] { "rePOrt", CommandType.REPORT }
        };

        static object[] ValidPlaceCommandScenarios =
        {
            new object[] { "PLACE 1,1,North", 1, 1, Direction.NORTH},
            new object[] { "place 1,1,east" , 1, 1, Direction.EAST},
            new object[] { "pLACe 0,0,West" , 0,0, Direction.WEST},
            new object[] { "place 0 , 0 , WEST",0,0, Direction.WEST }
        };

        static object[] InvalidCommandScenarios =
        {
            new object[] { "PLACE 1-1,North", ErrorMessages.SupportedCommandsListMessage },
            new object[] { "plac 1,1,east", ErrorMessages.SupportedCommandsListMessage},
            new object[] { "pLACe0,0,West" , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "pLACe x,0,West" , ErrorMessages.InvalidLocationParameters},
            new object[] { "pLACe 0,y,West" , ErrorMessages.InvalidLocationParameters},
            new object[] { "pLACe 0,0,West-ish" , ErrorMessages.InvalidDirectionMessage},
            new object[] { "PLACE 0,0,1" , ErrorMessages.InvalidDirectionMessage},
            new object[] { "PLACE 0,0,9" , ErrorMessages.InvalidDirectionMessage},
            new object[] { "place  ", ErrorMessages.SupportedCommandsListMessage},
            new object[] { "place 0 , 0 " , ErrorMessages.SupportedCommandsListMessage},
            new object[] { " " , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "place 0 , " , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "mov" , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "m0v" , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "lef" , ErrorMessages.SupportedCommandsListMessage},
            new object[] { "rigt", ErrorMessages.SupportedCommandsListMessage }
        };
    }
}
