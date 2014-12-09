using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AccountManager
{
    public interface IAccount
    {
        AccountType getAccountType();
       
        double calculateInterestEarned(double amount);
        
        void deposit(double amount);
        
        void withdraw(double amount);

        double getInterest();

        double getAccountBalance();

        void setAccountBalance(double amount);
    }
}
