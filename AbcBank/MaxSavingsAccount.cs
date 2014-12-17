using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class MaxSavingsAccount : Account
    {
        public MaxSavingsAccount()
        {
        }

        public override double InterestEarned()
        {
            double amount = SumTransactions();
            if (this.CheckIfAnyWithDrawls(10))
            {                
                if (amount <= 1000)
                    return amount * 0.02;
                if (amount <= 2000)
                    return 20 + (amount - 1000) * 0.05;
                return 70 + (amount - 2000) * 0.1;
            }
            else
            {
                return amount * 0.05;                
            }            
        }

        public override AccountType GetAccountType()
        {
            return AccountType.MAXI_SAVINGS;
        }
    }
}
