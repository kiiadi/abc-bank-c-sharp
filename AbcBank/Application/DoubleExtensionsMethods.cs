using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Application
{
    public static class DoubleExtensionsMethods
    {
        public static string ToDollarString(this double amount)
        {
            return String.Format("${0:N2}", Math.Abs(amount));
        }
    }
}
