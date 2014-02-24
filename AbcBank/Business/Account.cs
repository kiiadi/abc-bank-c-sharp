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

    public interface IAccountForRateCalculators
    {
        double CurrentAmount { get; }
        List<ITransaction> Transactions { get; }
        List<InvestmentPeriod> InvestmentPeriods { get; }
    }

    public class Account : IAccount, IAccountForRateCalculators
    {
        private static int NEXT_ACCOUNT_NUMBER = 10000001;

        ITransactionFactory transactionFactory;
        IAccountStatement accountStatement;
        IAccountRateCalculator rateCalculator;
        ITransactionsToPeriodsConverter transactionsToPeriodsConverter;

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
            get { return rateCalculator.Calculate(this); }
        }

        public List<ITransaction> Transactions
        {
            get { return transactions; }
        }

        public List<InvestmentPeriod> InvestmentPeriods 
        {
            get { return transactionsToPeriodsConverter.Calculate(transactions); } 
        }
        #endregion


        public Account(AccountTypes accountType, ITransactionFactory transactionFactory, IAccountStatement accountStatement, IAccountRateCalculator rateCalculator, ITransactionsToPeriodsConverter transactionsToPeriodsConverter)
        {
            this.transactionFactory = transactionFactory;
            this.accountStatement = accountStatement;
            this.rateCalculator = rateCalculator;
            this.transactionsToPeriodsConverter = transactionsToPeriodsConverter;

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
