using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank
{
    public class Account: iAccount
    {
        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, DateProvider.GetInstance()));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, DateProvider.GetInstance()));
            }
        }


        /*
            Checking accounts have a flat rate of 0.1%
            Savings accounts have a rate of 0.1% for the first $1,000 then 0.2%
            Maxi-Savings accounts have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10%
         * 
         * 
         *  Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
         */
        public double InterestEarned()
        {
            double amount = Balance();
            switch (accountType)
            {
                case AccountType.Savings:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                // case SUPER_SAVINGS:
                //     if (amount <= 4000)
                //         return 20;
                case AccountType.MaxiSavings:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000) * 0.05;
                    return 70 + (amount - 2000) * 0.1;
                case AccountType.Checking:
                    return amount * 0.001;
                default:
                    return amount * 0.001;
            }
        }

        public double Balance()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }

        public AccountType GetAccountType()
        {
            return accountType;
        }

    }
}
