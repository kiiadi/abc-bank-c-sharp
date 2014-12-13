using System;
using System.Collections.Generic;
using System.Collections;

namespace AbcBank
{

    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;        

        private int accountType;
        public List<Transaction> transactions;
        

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }


        public void TransferToAccount(double amount, Account ToAccount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else if (amount > sumTransactions()) 
            {
                throw new ArgumentException("amount must be in account to transfer");
            }
            else
            {
                try
                {
                    transactions.Add(new Transaction(-amount));                    
                    ToAccount.transactions.Add(new Transaction(amount));
                }
                catch
                {
                    throw new ArgumentException("rollback implementation");
                }

            }
        }

        public void deposit(double amount)
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

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                // case SUPER_SAVINGS:
                //     if (amount <= 4000)
                //         return 20;

                //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000) * 0.05;
                    //Keep existing logic above
                    foreach (Transaction t in transactions) {
                        if (t.amount < 0 && t.transactionDate >= DateTime.Now.AddDays(-10))
                        {
                            return 70 + (amount - 2000) * 0.05; //Maxi-Savings interest rate of 5% assuming no withdrawals in the past 10 days 
                        }                        
                    }
                    return 70 + (amount - 2000) * 0.001; //otherwise change to 0.1%
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
        {
            return checkIfTransactionsExist(true);
        }

        public double checkIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int getAccountType()
        {
            return accountType;
        }
                

    }
}
