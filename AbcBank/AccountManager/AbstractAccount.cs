using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AccountManager
{
    public abstract class AbstractAccount:IAccount
    {
        protected AccountType accountType { get; set; }
        public ArrayList transactions;
        protected double accountBalance { get; set; }

        public AbstractAccount()
        {
            this.transactions = new ArrayList();
            this.accountBalance = 0.0;

        }

        public abstract double calculateInterestEarned(double amount);

        public double getInterest()
        {
            var interest = calculateInterestEarned(this.accountBalance);

            return interest;
        }

        public void deposit(double amount)
        {
            try
            {
                if (isValidAmount(amount) == true)
                {
                    transactions.Add(new Transaction(amount, AccountAction.Deposit));
                    accountBalance += amount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void withdraw(double amount)
        {
            try
            {
                if (amount > this.accountBalance)
                    throw new ArgumentException("Insufficient funds for withdrawal");

                if (isValidAmount(amount) == true)
                {
                    transactions.Add(new Transaction(amount, AccountAction.Withdraw));
                    accountBalance -= amount;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual bool isValidAmount(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
                return true;
        }

        public AccountType getAccountType()
        {
            return this.accountType;
        }

        public void setAccountBalance(double amount)
        {
            this.accountBalance = amount;
        }

        //Call Some dataprovider method
        public double getAccountBalance()
        {
            return this.accountBalance;
        }

    }

}
