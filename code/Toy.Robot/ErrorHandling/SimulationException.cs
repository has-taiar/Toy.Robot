using System;
namespace Toy.Robot.ErrorHandling
{
    public class SimulationException : Exception
    {
        public SimulationException(string message) : base(message)
        {

        }
    }
}
