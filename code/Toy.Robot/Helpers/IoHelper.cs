using Toy.Robot.Contracts;
using System;

namespace Toy.Robot.Helpers
{
    class IoHelper : IIoHelper
    {
        public string Read()
        {
            return Console.ReadLine().Trim();
        }

        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
