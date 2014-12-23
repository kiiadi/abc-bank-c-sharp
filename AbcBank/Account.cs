using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public abstract class Account
    {
        public List<Transaction> transactions;
        protected Account()  //to avoid instantiation of base class
        {
            this.transactions = new List<Transaction>();
        }

        public bool deposit(double amount)
        {
            bool v_success = false;
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
                v_success = true;
            }
            return v_success;
        }

        public bool withdraw(double amount)
        {
            bool v_success = false;
            if (amount <= 0 || amount > sumTransactions())
            {
                throw new ArgumentException("Amount must be greater than zero and shouldn't exceed the amount in your account");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
                v_success = true;
            }
            return v_success;
        }
        public virtual double interestEarned()
        {
            double amount = sumTransactions();
            return amount * 0.01;
        }
        public double sumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;

        }
        public abstract  String getAccountType();
    }
}
