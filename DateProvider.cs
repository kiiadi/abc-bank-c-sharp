using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class DateProvider
    {
        private static volatile DateProvider _instance = null;
        private static object sync = new object();

        public static DateProvider Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(sync)
                    {
                        if(_instance ==null)
                        {
                            _instance= new DateProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        public DateTime now()
        {
            return DateTime.Now;
        }
    }
}
