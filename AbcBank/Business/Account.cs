using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface IAccount
    {
        string Name { get; }
        double CurrentAmount { get; }
        string TransactionsStatement { get; }
        double InterestEarned { get; }

        void Deposit(double amount);
        void Withdraw(double amount);
    }

    public class Account : IAccount
    {
        private static int NEXT_ACCOUNT_NUMBER = 10000001;

        ITransactionFactory transactionFactory;
        IAccountStatement accountStatement;
        IAccountRateCalculator rateCalculator;

        private readonly List<ITransaction> transactions = new List<ITransaction>();

        #region Properties
        public string Name { get; private set; }

        public double CurrentAmount { get; private set; }

        public string TransactionsStatement
        {
            get { return accountStatement.Generate(transactions); }
        }

        public double InterestEarned
        {
            get { return rateCalculator.Calculate(CurrentAmount); }
        }
        #endregion


        public Account(AccountTypes accountType, ITransactionFactory transactionFactory, IAccountStatement accountStatement, IAccountRateCalculator rateCalculator)
        {
            this.transactionFactory = transactionFactory;
            this.accountStatement = accountStatement;
            this.rateCalculator = rateCalculator;

            Name = string.Format("{0}_{1}", accountType, NEXT_ACCOUNT_NUMBER++);
        }


        #region Methods
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                CurrentAmount += amount;
                transactions.Add(transactionFactory.GetNewTransaction(amount));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else if (amount > CurrentAmount)
            {
                throw new ArgumentException("can't withdraw more than available");
            }
            else
            {
                CurrentAmount -= amount;
                transactions.Add(transactionFactory.GetNewTransaction(-amount));
            }
        }
        #endregion

    }
}
