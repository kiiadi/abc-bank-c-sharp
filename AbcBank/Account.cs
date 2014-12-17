using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        public List<Transaction> transactions;
        public double Balance;

        public Account()
        {
            this.transactions = new List<Transaction>();
        }

        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
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
                if (amount <= SumTransactions())
                {
                    transactions.Add(new Transaction(-amount));
                }
                else
                {
                    throw new ArgumentException("amount to withdraw is more than your current balance");
                }
            }
        }

        public virtual double InterestEarned()
        {
            double amount = SumTransactions();
            return amount * 0.001;
        }

        public double SumTransactions()
        {
            double amount = 0.0;
            amount = this.transactions.Sum(t => t.amount);
            return amount;
        }

        public virtual AccountType GetAccountType()
        {
            return AccountType.INVALID;
        }

        public bool CheckIfAnyWithDrawls(int numberofDays)
        {
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(numberofDays, 0, 0, 0));

            foreach (var item in this.transactions.Where(s => s.TranscationDate >= startDate && s.TranscationDate < DateTime.Now))
            {
                if (item.amount < 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
