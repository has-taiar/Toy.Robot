namespace Toy.Robot.Tests.TestData
{
    class OrientationTestData
    {
        static object[] InvalidDirections =
            {
                new object[] { "something" },
                new object[] { " invalid " },
                new object[] { "" }
            };

        static object[] ValidDirections =
        {
                new object[] { "north" },
                new object[] { "East" },
                new object[] { "South" },
                new object[] { "wEst" }
            };
    }
}
