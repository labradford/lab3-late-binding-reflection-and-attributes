using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    [SpecialClass(4)]
    public class RecursiveStuff
    {
        public static long SumOfSquares(long value)
        {
            if (value > 1)
            {
                return value * value + SumOfSquares(value - 1);
            }
            return value;
        }

        public static long Factorial(long value)
        {
            if (value > 1)
            {
                return value * Factorial(value - 1);
            }
            return value;
        }
    }
}
