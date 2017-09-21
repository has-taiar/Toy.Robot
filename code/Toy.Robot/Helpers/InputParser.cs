using Toy.Robot.ErrorHandling;
using Toy.Robot.Domain;
using Toy.Robot.Domain.Enums;
using System;
using Toy.Robot.Contracts;
using System.Linq;

namespace Toy.Robot.Helpers
{
    public class InputParser : IInputParser
    {
        public bool ShouldExit(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            return input.Trim().Equals(Constants.ExitSimulationCommandText, StringComparison.CurrentCultureIgnoreCase);
        }

        public Command ParseCommand(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new SimulationException(ErrorMessages.SupportedCommandsListMessage);
            try
            {
                var segments = input.Trim().Split(Constants.DefaultCommandAndParametersSpliterChar, 2);
                var commandType = (CommandType)Enum.Parse(typeof(CommandType), segments[0], true);
                var command = new Command(commandType, EnsureParsingPlaceCommandParametersIfNeeded(commandType, segments));
                return command;
            }
            catch (SimulationException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw new SimulationException(ErrorMessages.SupportedCommandsListMessage);
            }
        }

        private object EnsureParsingPlaceCommandParametersIfNeeded(CommandType type, string[] segments)
        {
            if (type != CommandType.PLACE) return null;

            if (segments.Length < 2)
                throw new SimulationException(ErrorMessages.SupportedCommandsListMessage);

            var placementPositionSegments = segments[1].Trim().Split(Constants.DefaultCommandParametersSpliterChar);
            if (placementPositionSegments.Length < Constants.NoOfArgumentsExpectedForPlaceCommand)
                throw new SimulationException(ErrorMessages.SupportedCommandsListMessage);

            var isValidDirection = Enum.GetNames(typeof(Direction)).Any(d => d.Equals(placementPositionSegments[2].Trim(), StringComparison.CurrentCultureIgnoreCase));
            if (!isValidDirection)
                throw new SimulationException(ErrorMessages.InvalidDirectionMessage);

            int x =0, y =0;
            var isXLocationValid = int.TryParse(placementPositionSegments[0], out x);
            var isYLocationValid = int.TryParse(placementPositionSegments[1], out y);
            if (!isXLocationValid || !isYLocationValid)
                throw new SimulationException(ErrorMessages.InvalidLocationParameters);

            return new PlaceCommandParameter(x,y, placementPositionSegments[2].Trim());
        }
    }
}
