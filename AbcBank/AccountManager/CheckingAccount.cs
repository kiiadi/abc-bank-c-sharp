using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountManager;

namespace AbcBank.AccountManager
{
    public class CheckingAccount : AbstractAccount
    {
        public CheckingAccount()
        {
            this.accountType = AccountType.Checking;
        }

        public override double calculateInterestEarned(double amount)
        {
            return amount * 0.001;
        }
    }
}
