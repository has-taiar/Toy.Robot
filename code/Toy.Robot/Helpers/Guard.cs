using System;

namespace Toy.Robot.Helpers
{
    public static class Guard
    {
        public static void ThrowIfNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException($"{argumentName} cannot be null");
            }
        }
    }
}
