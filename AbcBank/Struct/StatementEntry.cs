using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Enums;

namespace AbcBank.Struct
{
    public class StatementEntry
    {
        public DateTime TransactionDate;
        public TransactionType TransactionType;
        public double Amount;
        public double PreviousBalance;
        public double Principal;
        public double DailyInterest;
    }
}
