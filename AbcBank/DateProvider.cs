using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;

namespace AbcBank
{
    public class DateProvider: iDateProvider
    {
        private static DateProvider instance = new DateProvider();

        public static DateProvider GetInstance()
        {
            return instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
