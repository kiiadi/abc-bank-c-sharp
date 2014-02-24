using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    public interface ICustomer
    {
        String getName();
        List<IAccount> getAccounts();
        ICustomer openAccount(IAccount account);
        void deposit(string accountNumber, double amount);
        void withdraw(string accountNumber, double amount);
        IAccount findAccount(string accountNumber);
        int getNumberOfAccounts();
        double totalInterestEarned();
        String getStatement();
    }

    public class Customer : ICustomer
    {
        private readonly String name;
        private readonly List<IAccount> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String getName()
        {
            return name;
        }

        public List<IAccount> getAccounts()
        {
            return accounts;
        }

        public ICustomer openAccount(IAccount account)
        {
            accounts.Add(account);
            return this;
        }
        
        public void deposit(string accountNumber, double amount)
        {
            var account = findAccount(accountNumber);
            if (account == null)
            {
                throw new ArgumentException("accountNumber is not a valid customer account");
            }
            account.deposit(amount);
        }

        public void withdraw(string accountNumber, double amount)
        {
            var account = findAccount(accountNumber);
            if (account == null)
            {
                throw new ArgumentException("accountNumber is not a valid customer account");
            }
            account.withdraw(amount);
        }

        public virtual IAccount findAccount(string accountNumber)
        {
            return accounts.FirstOrDefault(act => accountNumber == act.getAccountNumber());
        }

        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double totalInterestEarned()
        {
            return accounts.Sum(a => a.interestEarned());
        }

        public String getStatement()
        {
            return new AccountStatement().getStatement(name, accounts);
        }

         
    }
}
