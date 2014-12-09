using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountManager;
using System.Collections;
using System.Linq;

namespace AbcBank.AccountManager
{
    public class MaxiSavingsAccount : AbstractAccount
    {

        public MaxiSavingsAccount()
        {
            this.accountType = AccountType.MaxiSavings;
        }

        public override double calculateInterestEarned(double amount)
        {
            if ((DateTime.Now - getLastWithdrawalTransactionDate()).TotalDays > 10)
                return amount * .05;
            else
                return amount * .01;
        }

        private DateTime getLastWithdrawalTransactionDate()
        {
            List<Transaction> withdrawals = new List<Transaction>();

            foreach(Transaction t in transactions)
            {
                
                if (t.transactionType.Equals(Transaction.TransactionType.Withdrawal))
                {
                    withdrawals.Add(t);
                }
            }

            if (withdrawals.Count > 0)
            {
                withdrawals.OrderByDescending(t => t.getTransactionDate());
                return withdrawals.First().getTransactionDate();
            }
            

            return DateTime.Now.AddDays(-11);

        }
    }
}
