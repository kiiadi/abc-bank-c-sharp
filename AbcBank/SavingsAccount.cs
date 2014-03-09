using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class SavingsAccount: Account
    {
        public override string AccountType()
        {
            return "Savings Account";
        }

        public override double InterestEarned()
        {
            double amount = Balance();
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }

        public override double DailyInterest(double principal, int withdrawalCount)
        {
            if (principal <= 1000)
                return (principal * 0.001) / 365.0;
            else
                return (1 + (principal - 1000) * 0.002) / 365.0;
        }
    }
}
