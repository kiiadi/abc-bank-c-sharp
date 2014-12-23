using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class MaxiSavingsAccount : Account
    {
        private readonly string accountType;

        public MaxiSavingsAccount()
        {
            this.accountType = "Maxi Savings Account";

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

        public double iterestEarnedPastTenDays()
        {
            double amount = sumTransactions();
            // interest rate of 5% assuming no withdrawals in the past 10 days 
            foreach (Transaction t in transactions)
            {
                if (t.amount < 0 && t.getTransactionDate() >= DateTime.Now.AddDays(-10))
                {
                    return 70 + (amount - 2000) * 0.05; 
                }
            }
            return 70 + (amount - 2000) * 0.001; //else change to 0.1%
        }
    }
}
