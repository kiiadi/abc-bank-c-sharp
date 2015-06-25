using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AbcAccount
{
    public class AccountManager
    {
        //get interest Earned.
        public Double getInterestEarned(AccountType accountType, Double amount) 
        {

            return GetAccountInstance(accountType, amount).InterestEarned();
        }
        /// <summary>
        /// return account instance based on the account type 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public AccountBase GetAccountInstance(AccountType accountType, Double amount)
        {
            AccountBase accountinstance = null;
            switch (accountType)
            {
                case AccountType.Checking:
                    accountinstance = new CheckingAccount(amount);
                    break;
                case AccountType.Savings:
                    accountinstance = new SavingAccount(amount);
                    break;
                case AccountType.MaxiSavings:
                    accountinstance = new MaxiSavingsAccount(amount);
                    break;
                default:
                    break;
            }
            return accountinstance;
        }
    }
}
