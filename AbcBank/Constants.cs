using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Constants
    {
        public const Double CompareAmount = 1000;
        public const Double MultipleAmount_1 = 0.001;
        public const Double MultipleAmount_2 = 0.002; 
        public const string  AmountGreaterThanZero="amount must be greater than zero";
        public const string CONST_WITHDRAWAL = "withdrawal";
        public const string CONST_DEPOSIT = "deposit";
    }

    public enum AccountType
    { 
        Checking=0,
        Savings =1,
        MaxiSavings=2
    }

    public enum TransactionType
    {
        Deposit = 1,
        WithDrawal = 1,
        Transfer = 2
    }
}
