namespace Toy.Robot.Tests.TestData
{
    class LocationTestData
    {
        static object[] InvalidInputs =
            {
                new object[] { -1, -1 },
                new object[] { -1, 0 },
                new object[] { 0, -1 },
                new object[] { 0, Constants.TableLengthUnits +1 },
                new object[] { Constants.TableWidthUnits+1, 0 },
                new object[] { Constants.TableWidthUnits + 1, Constants.TableLengthUnits +1 }
            };

        static object[] ValidInputs =
        {
                new object[] { 0, 0 },
                new object[] { 1, 0 },
                new object[] { 0, 1 },
                new object[] { 5, 5 }
            };
    }
}
