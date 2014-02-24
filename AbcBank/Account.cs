using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{

    public enum AccountType
    {
        Checking = 0,
        Savings = 1,
        MaxiSavings = 2
    }

    public interface IAccount
    {
        void deposit(double amount);
        void withdraw(double amount);
        double interestEarned();
        double sumTransactions();
        AccountType getAccountType();
        string getAccountNumber();
        IList<Transaction> getTransactions();
    }

    public class Account : IAccount
    {
        private readonly AccountType accountType;
        private readonly string accountNumber;
        private readonly List<Transaction> transactions;

        public Account(AccountType accountType, string accountNumber)
        {
            this.accountNumber = accountNumber;
            this.accountType = accountType;
            transactions = new List<Transaction>();
        }

        public string getAccountNumber()
        {
            return accountNumber;
        }

        public AccountType getAccountType()
        {
            return accountType;
        }

        public void deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            
            transactions.Add(new Transaction(amount));
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            
            transactions.Add(new Transaction(-amount));
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            double result = 0;
            switch (accountType)
            {
                case AccountType.Savings:
                    if (amount <= 1000)
                        result = amount*0.001;
                    else
                        result = (amount - 1000)*0.002 + 1000*0.001;
                    break;
                case AccountType.MaxiSavings:
                    if (amount <= 1000)
                        result = amount * 0.02;
                    else if (amount <= 2000)
                        result = (amount - 1000) * 0.05 + 1000 * 0.02;
                    else
                        result = (amount - 2000) * 0.1 + 1000 * 0.05 + 1000 * 0.02;
                    break;
                case AccountType.Checking:
                    result = amount * 0.001 ;
                    break;
            }
            return result;
        }

        public virtual double sumTransactions()
        {
            return transactions.Sum(t => t.Amount);
        }
        public IList<Transaction> getTransactions()
        {
            return transactions;
        }


    }
}
