using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Application;
using System.Runtime.CompilerServices;

namespace AbcBank.Business
{
    public interface ICustomer
    {
        string Name { get; }
        int NumberOfAccounts { get; }
        IList<string> Accounts { get; }
        double TotalInterestEarned { get; }
        string AccountsAndTotalsStatement { get; }

        string OpenAccount(AccountTypes accountType);
        void Deposit(string accountName, double amount);
        void Withdraw(string accountName, double amount);
        void Transfer(string accountNameFrom, string accountNameTo, double amount);
    }

    public class Customer : ICustomer
    {
        IBank bank;
        ICustomerStatement customerStatement;
        ICustomerInterestCalculator customerInterestCalculator;

        private readonly Dictionary<string, IAccount> accounts = new Dictionary<string, IAccount>();

        #region Properties
        public String Name { get; private set; }
        
        public int NumberOfAccounts
        {
            get { return accounts.Count; }
        }
        
        public IList<string> Accounts
        {
            get { return accounts.Keys.ToList(); }
        }

        public double TotalInterestEarned
        {
            get { return customerInterestCalculator.Calculate(accounts.Values); }
        }

        public String AccountsAndTotalsStatement
        {
            get { return customerStatement.Generate(Name, accounts.Values.ToList()); }
        }
        #endregion

        public Customer(String name, IBank bank, ICustomerStatement customerStatement, ICustomerInterestCalculator customerInterestCalculator)
        {
            Name = name;
            this.bank = bank;
            this.customerStatement = customerStatement;
            this.customerInterestCalculator = customerInterestCalculator;
        }

        #region Methods
        public string OpenAccount(AccountTypes accountType)
        {
            IAccount account = bank.OpenAccount(this, accountType);
            accounts[account.Name] = account;
            return account.Name;
        }

        public void Deposit(string accountName, double amount)
        {
            accounts[accountName].Deposit(amount);
        }

        public void Withdraw(string accountName, double amount)
        {
            accounts[accountName].Withdraw(amount);
        }

        public void Transfer(string accountNameFrom, string accountNameTo, double amount)
        {
            IAccount accountFrom;
            IAccount accountTo;
            if (accounts.TryGetValue(accountNameFrom, out accountFrom) && accounts.TryGetValue(accountNameTo, out accountTo))
            {
                accountFrom.Withdraw(amount);
                accountTo.Deposit(amount);
            }
            else 
            {
                throw new KeyNotFoundException();
            }
        }
        #endregion Methods
    }
}
