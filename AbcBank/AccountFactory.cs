using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public enum AccountType
    {
        Checking = 0,
        Savings = 1,
        Maxi_Savings = 2,
        Super_Savings = 3

    }
    public static class AccountFactory
    {
       
        public static Account CreateAccount(AccountType accountType)
        {
            Account account = null;
            switch (accountType)
            {
                case AccountType.Checking:
                    account= new CheckingAccount();
                    break;
                case AccountType.Savings:
                    account= new SavingsAccount();
                    break;
                    
                case AccountType.Maxi_Savings:
                    account= new MaxiSavingsAccount();
                    break;
                case AccountType.Super_Savings:
                    account= new SuperSavingsAccount();
                    break;
                default:
                    throw new NotImplementedException("This account type does not exist");
            }
            return account;
          }
    }
}
