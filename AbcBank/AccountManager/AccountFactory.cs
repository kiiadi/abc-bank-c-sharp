using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AccountManager
{
    public  class AccountFactory
    {
        public AccountFactory()
        {

        }

        public IAccount createAccount(AccountType accountType)
        {
            switch (accountType)
            { 
                case AccountType.Checking:
                    return new CheckingAccount();
                case AccountType.Savings:
                    return new SavingsAccount();
                case AccountType.MaxiSavings:
                    return new MaxiSavingsAccount();
                default: throw new ArgumentException("Invalid Account Type");
            }
        }
    }
}
