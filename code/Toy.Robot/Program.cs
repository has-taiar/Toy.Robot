using Toy.Robot.ErrorHandling;
using Toy.Robot.Helpers;
using Toy.Robot.Domain;
using System;

namespace Toy.Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Toy Robot Simulation");
            Console.WriteLine(ErrorMessages.SupportedCommandsListMessage);

            using (Simulation simulation = new Simulation(new Domain.Robot(new RobotBrain()), new InputParser(), new IoHelper()))
            {
                simulation.Start();
            }

            Console.WriteLine("Thanks for trying our Toy Robot :)");
            Console.WriteLine("Press any key to exit");
            Console.WriteLine("Good Bye");
            Console.ReadKey();
        }
    }
}
