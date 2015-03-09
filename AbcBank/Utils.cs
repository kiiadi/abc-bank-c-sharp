using System;

namespace AbcBank
{
    public class Utils
    {
        static public bool DateDiffLessThan10Days(DateTime from_, DateTime to_)
        {
            return (int)(to_ - from_).TotalDays < 10;
        }

        static public String Dollars(double d_)
        {
            return String.Format("${0:N2}", Math.Abs(d_));
        }

        public static String Dollars(decimal d_)
        {
            return String.Format("${0:N2}", Math.Abs(d_));
        }
    }
}
