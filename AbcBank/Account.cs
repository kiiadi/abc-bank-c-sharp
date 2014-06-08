using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        private Guid AccountID; // Global UID helps distinguish between multiple accounts of the same type belonging to individual customer

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;

        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.AccountID = Guid.NewGuid();
        }

        // return account type 
        public int getAccountType()
        {
            return accountType;
        }

        // return account ID 
        public string getAccountID()
        {
            return AccountID.ToString();
        }

        // deposit funds within account
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

        // withdraw funds from account
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

        // calculate interest earned based on account type
        public double interestEarned()
        {
            // Different accounts have interest calculated in different ways:
            //    Checking accounts have a flat rate of 0.1% (0.001)
            //    Savings accounts have a rate of 0.1% (0.001) for the first $1,000 then 0.2% (0.002)
            //    Maxi-Savings accounts have a rate of 2% (0.02) for the first $1,000 then 5% (0.05) for the next $1,000 then 10% (0.1)

            double amount = sumTransactions();
            switch (accountType)
            {
                case Account.SAVINGS:       // SAVINGS
                    if (amount <= 1000)
                    {
                        return amount * 0.001;
                    }
                    else
                    {
                        return 1 + ((amount - 1000) * 0.002);
                    }
                case Account.MAXI_SAVINGS:  // MAXI_SAVINGS
                    if (amount <= 1000)
                    {
                        return amount * 0.02;
                    }
                    else if (amount <= 2000)
                    {
                        return 20 + ((amount - 1000) * 0.05);
                    }
                    else
                    {
                        return 70 + ((amount - 2000) * 0.1);
                    }
                //case Account.MAXI_SAVINGS:  
                //    // Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
                //    // It isn't clear whether this feature eliminates or includes 2% and 5% implementation above for first, second $1K
                //    if (checkWithdrawalsLast10days() == true)
                //    {
                //        return amount * 0.05;
                //    }
                //    else
                //    {
                //        return amount * 0.001;
                //    }
                default:
                    return amount * 0.001; // CHECKING
            }
        }

        // calculate transaction total for account
        public double sumTransactions() // refactored to suit method's purpose, based on name
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        // check whether withdrawals in last 10 days:
        public bool checkWithdrawalsLast10days()
        {
            bool AnyWithdrawalsLastTenDays = false;
            int count = 0;
            foreach (Transaction t in transactions)
            {
                // negative t.getAmount() value is a withdrawal
                if ((t.getAmount() < 0) && ((t.getTransactionDate() - DateTime.Now).TotalDays <= 10))
                {
                    count++;
                }
            }
            if (count > 0)
                AnyWithdrawalsLastTenDays = true;

            return AnyWithdrawalsLastTenDays;
        }

        // return transaction total + interest earned
        public double accountBalance()
        {
            double result = sumTransactions() + interestEarned();
            return result;
        }

        // determine whether transactions exist for account
        public bool checkIfTransactionsExist() // refactored to suit method's purpose, based on name
        {
            bool TransactionsExist = false;
            if (transactions.Count > 0)
            {
                TransactionsExist = true;
            }
            else
            {
                TransactionsExist = false;
            }
            return TransactionsExist;
        }



    }
}
