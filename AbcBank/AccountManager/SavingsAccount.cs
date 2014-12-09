using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountManager;

namespace AbcBank.AccountManager
{
    public class SavingsAccount : AbstractAccount
    {
        public SavingsAccount()
        {
            this.accountType = AccountType.Savings;
        }

        public override double calculateInterestEarned(double amount)
        {
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }
    }
}
