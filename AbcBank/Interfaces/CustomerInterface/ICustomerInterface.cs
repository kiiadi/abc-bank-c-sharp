using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Implementation;
using AbcBank.AccountsInterface;

namespace AbcBank.CustomerInterface
{
    public interface ICustomerInterface
    {
        String CustomerName { get; }
        //Accounts CurrentCustomerAccount { get; set; }
        void AddAccount(IAccountsInterface account);
        void Deposit(IAccountsInterface Account, double Amount);
        void Withdraw(IAccountsInterface Account, double Amount);
        void Transfer(IAccountsInterface SourceAccount, IAccountsInterface DestinationAccount, double Amount);
        String GetAccountStatementforCustomer();
        int TotalAccounts { get; }
        //double totalInterestEarned();
    }
}
