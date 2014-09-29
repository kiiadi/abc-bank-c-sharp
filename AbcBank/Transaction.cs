using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public Transaction(DateTime date, double amount)
        {
            Date = date;
            Amount = amount;
        }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }
    }
}
