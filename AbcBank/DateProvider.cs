using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    /// <summary>
    /// Gets the date time class is used so that in future can be used to format the date and time and send it.
    /// </summary>
    public class DateProvider
    {
        DateProvider()
        {
           
        }

        private static DateProvider instance = null;

        /// <summary>
        /// get the new dataprovider instance 
        /// </summary>
        /// <returns></returns>
        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public DateTime now()
        {
            return DateTime.Now;
        }
    }
}
