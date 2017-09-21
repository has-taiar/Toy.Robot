using Toy.Robot.Contracts;
using System;

namespace Toy.Robot.Domain
{
    public class Simulation : IDisposable
    {
        private IRobot Robot;
        private IInputParser InputParser;
        private IIoHelper IoHelper;

        public Simulation(IRobot robot, IInputParser inputParser, IIoHelper ioHelper) 
        {
            Robot = robot;
            InputParser = inputParser;
            IoHelper = ioHelper;
        }

        public void Start()
        {
            var input = IoHelper.Read();
            while (!InputParser.ShouldExit(input))
            {
                try
                {
                    Command command = InputParser.ParseCommand(input);
                    string result = Robot.Process(command);
                    if (!string.IsNullOrEmpty(result))
                        IoHelper.Write(result);
                }
                catch (Exception e)
                {
                    IoHelper.Write(e.Message);
                }

                input = IoHelper.Read();
            }
        }

        public void Dispose()
        {
            Robot = null;
            IoHelper = null;
            InputParser = null;
        }
    }
}
