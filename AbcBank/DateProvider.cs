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
        private static DateProvider _instance = new DateProvider();

        public static DateProvider GetInstance()
        {
            return _instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
