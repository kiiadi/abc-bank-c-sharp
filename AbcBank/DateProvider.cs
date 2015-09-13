using System;

namespace AbcBank
{
    public class DateProvider
    {
        private static DateProvider instance = null;

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
