using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank.Test
{
    public class DummyDateProvider : IDateProvider
    {
        private static DummyDateProvider instance = new DummyDateProvider();
        private static DateTime _date;

        public static DummyDateProvider GetInstance()
        {
            return instance;
        }

        public static void SetNow(string date) {
            _date = DateTime.Parse(date);
        }

        public DateTime Now()
        {
            return _date;
        }
    }
}
