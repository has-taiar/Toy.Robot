using Toy.Robot.Domain;
using Toy.Robot.Domain.Enums;
using System;

namespace Toy.Robot.ErrorHandling
{
    public static class ErrorMessages
    {
        public static readonly string InvalidLocationParameters = $"X and Y must be non-negative integers and cannot be greater than the dimensions of the table [{Constants.TableWidthUnits} x {Constants.TableLengthUnits}]";
        public static readonly string InvalidDirectionMessage = $"Invalid direction. Supported values are: {string.Join(",", Enum.GetNames(typeof(Direction)))}";
        public const string SavingTheRobotByNotMoving = "Cannot move. Moving could cause the Robot to fall";
        public static string SupportedCommandsListMessage = $"Please enter one of the following supported commands:{Environment.NewLine}    {CommandType.PLACE.ToString()} X,Y,F{Environment.NewLine}    {CommandType.MOVE.ToString()}{Environment.NewLine}    {CommandType.LEFT.ToString()}{Environment.NewLine}    {CommandType.RIGHT.ToString()}{Environment.NewLine}    {CommandType.REPORT.ToString()}{Environment.NewLine}    EXIT";
    }
}