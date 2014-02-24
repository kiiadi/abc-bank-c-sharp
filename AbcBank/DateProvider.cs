using System;

namespace AbcBank
{
    public class DateProvider
    {
        public static Func<DateTime> now = () => DateTime.Now;
        public static void reset()
        {
            now = () => DateTime.Now;
        }
    }
}
