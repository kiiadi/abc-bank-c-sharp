using System;

namespace AbcBank
{
    public static class Utility
    {
        public static readonly double DOUBLE_DELTA = 1e-15;

        public static String ToDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}