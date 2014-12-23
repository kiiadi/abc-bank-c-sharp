using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class SuperSavingsAccount: Account
    {
        private readonly string accountType;

        public SuperSavingsAccount()
        {
            this.accountType = "Super Savings Account";
        }
        public override double interestEarned()
        {
            double amount = sumTransactions();
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }
        public override string getAccountType()
        {
            return accountType;
        }
    }
}
