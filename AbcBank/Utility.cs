using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public static class Utility
    {
        public static string ToDollars(double value)
        {
            return String.Format("${0:N2}", Math.Abs(value));
        }
        public static DateTime GetCurrentDate()
        {
            return DateTime.Today;
        }
    }
}
