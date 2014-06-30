using System;

namespace AbcBank
{
    public static class Extentions
    {
        public static String ToDollars(this double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
