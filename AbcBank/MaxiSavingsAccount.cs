using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class MaxiSavingsAccount: Account
    {
        public override string AccountType()
        {
            return "Maxi Savings Account";
        }

        public override double InterestEarned()
        {
            double amount = Balance();
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }

        public override double DailyInterest(double principal, int withdrawalCount)
        {
            if (withdrawalCount <= 0)
                return (principal * 0.05) / 365.0;
            else return (principal * 0.001) / 365.0;            
        }

    }
}
