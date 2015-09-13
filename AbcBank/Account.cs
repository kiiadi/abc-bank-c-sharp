using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public enum AccountType
    {
        CHECKING = 0,
        SAVINGS = 1,
        MAXI_SAVINGS = 2
    }

    public class Account
    {
        public AccountType AccountType { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Account(AccountType accountType)
        {
            this.AccountType = accountType;
            this.Transactions = new List<Transaction>();
        }

        public void deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("amount", "amount must be greater than zero");

            Transactions.Add(new Transaction(amount));
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("amount", "amount must be greater than zero");

            Transactions.Add(new Transaction(-amount));
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            switch (AccountType)
            {
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                // case SUPER_SAVINGS:
                //     if (amount <= 4000)
                //         return 20;
                case AccountType.MAXI_SAVINGS:
                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //if (amount <= 2000)
                    //    return 20 + (amount - 1000) * 0.05;
                    //return 70 + (amount - 2000) * 0.1;
                     
                    //bool for whether or not there was a withdraw in last 10 days.
                    bool last10Days = false;

                    Transactions.ForEach(x =>
                    {
                        //is it a withdraw.
                        if (x.Amount < 0)
                        {
                            //determin if it's in the last 10 dayas.
                            TimeSpan daysAgo = DateTime.Now - x.TransactionDate;
                            if (daysAgo.TotalDays <= 10)
                                last10Days = true;
                        }
                    });

                    if (!last10Days)
                        return amount * .05;
                    else
                        return amount * .01;

                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
        {
            if (Transactions.Count == 0)
                return 0.0;

            return Transactions.Sum(x => x.Amount);
        }

        public Account transferAccounts(Account from)
        {
            Account to = new Account(from.AccountType);

            to.AccountType = from.AccountType;
            to.Transactions.AddRange(from.Transactions);
            
            return to;
        }
    }
}
