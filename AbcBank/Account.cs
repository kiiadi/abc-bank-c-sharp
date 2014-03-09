using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank
{
    public abstract class Account: IAccount
    {
        public List<Transaction> Transactions;

        public Account()
        {
            this.Transactions = new List<Transaction>();
        }

        public abstract string AccountType();

        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Deposit(amount, DateProvider.GetInstance().Now());
            }
        }

        public virtual void Deposit(double amount, DateTime depositDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transactions.Add(new Transaction(amount, depositDate));
            }
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Withdraw(amount, DateProvider.GetInstance().Now());
            }
        }


        public virtual void Withdraw(double amount, DateTime withdrawalDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Transactions.Add(new Transaction(-amount, withdrawalDate));
            }
        }

        public virtual double InterestEarned()
        {
            double amount = Balance();
            return amount * 0.001;

        }

        public virtual double InterestEarned(DateTime now)
        {
            return CalculationEngine.CalculateInterestEarned(this, now);
        }

        public virtual double Balance()
        {
            double amount = 0.0;
            foreach (Transaction t in Transactions)
                amount += t.Amount;
            return amount;
        }
        
        public virtual double DailyInterest(double principal, int withdrawalCount)
        {
            return (principal * 0.0001) / 365.0;
        }

    }
}
