using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class SavingsAccount:Account
    {
        private readonly string accountType;

        public SavingsAccount()
        {
            this.accountType = "Savings Account";
        }
        
        public override double interestEarned()
        {
            double amount = sumTransactions();
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;

        }
        public override string getAccountType()
        {
            return accountType;
        }

    }
}
