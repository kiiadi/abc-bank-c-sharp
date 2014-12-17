using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class SavingsAccount : Account
    {
        public SavingsAccount()
        {

        }

        public override double InterestEarned()
        {
            double amount = SumTransactions();
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }
    }
}
