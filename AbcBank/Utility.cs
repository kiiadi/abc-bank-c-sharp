using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    static class Utility
    {
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        public static String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
        public static String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
