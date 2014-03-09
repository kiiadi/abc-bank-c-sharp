using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;
using System.Linq.Expressions;
using AbcBank.Struct;

namespace AbcBank
{
    public sealed class CalculationEngine
    {
        private static readonly CalculationEngine engine = new CalculationEngine();

        public static double CalculateInterestEarned(Account account, DateTime asofdate)
        {
            var a = (account as Account);
            var transactions = a.Transactions;

            if (transactions.Count == 0) return 0;

            var startDate = transactions.OrderBy(x => x.TransactionDate).Select(x=>x.TransactionDate).FirstOrDefault();

            Dictionary<DateTime, StatementEntry> balanceSheet = new Dictionary<DateTime, StatementEntry>();

            //TODO:  make sure all the transactions on the same date goes into one line and keep track if there is a withdrawal for the maxiAccount

            //First create a list of dates
            for (var dt = startDate; dt <= asofdate; dt = dt.AddDays(1))
            {
                balanceSheet.Add(dt.Date, new StatementEntry());
            }

            //Assign transaction amount to the dates
            foreach (var tran in transactions)
            {
                var entry = balanceSheet[tran.TransactionDate.Date];
                entry.TransactionType = tran.TransactionType;
                entry.Amount = tran.Amount;
            }


            double interestEarned = 0.0;
            int withdrawalCount = 0;
            //if first set previous balance to empty, else previous daily interest and principal
            // set principal to previous + transaction
            // daily interest based on type 
            foreach (var transactionDate in balanceSheet.Keys)
            {
                var previousDate = transactionDate.AddDays(-1);
                var entry = balanceSheet[transactionDate];
                
                if (entry.TransactionType == TransactionType.Withdrawal)
                {
                    withdrawalCount = 10;
                }

                if (balanceSheet.ContainsKey(previousDate))
                {
                    var previousEntry = balanceSheet[previousDate];
                    entry.PreviousBalance = previousEntry.Principal + previousEntry.DailyInterest;                
                }
                else
                {
                    //first entry
                    entry.PreviousBalance = 0;                    
                }

                entry.Principal = entry.Amount + entry.PreviousBalance;
                entry.DailyInterest = DailyInterest(account.DailyInterest, entry.Principal, withdrawalCount);

                interestEarned += entry.DailyInterest;

                withdrawalCount -= 1;
            }

            return interestEarned;
        }


        /*
         *  Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
         */

        private static double DailyInterest(Func<double, int, double> accountCalculation, double principal, int withdrawalCount)
        {
            return accountCalculation(principal, withdrawalCount);
        }


    }
}
