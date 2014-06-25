using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    class StringHelper
    {
        
        //Pluralize the passed in word based on the number passed in.  For example,
        // if the function is called with (3, "apple"), the return is apples.
        static public String PluralizeStringBasedOnNumber(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
