using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountsInterface;
using AbcBank.utilities;

namespace AbcBank.Implementation
{
    public class MaxiSavingsAccount : Transactions, IAccountsInterface
    {
        private const int _accountType = 2;
        private String _accountName = "Maxi Savings Account";

        public MaxiSavingsAccount()
        {
        }

        public int AccountType
        {
            get
            {
                return _accountType;
            }
        }

        public String Name
        {
            get
            {
                return _accountName;
            }
        }  

        public double PerformInterestCalculations(double values)
        {
            DateTime EarliestDepostiDatetime = AccountTransactions.Keys.Min();
            int LatestWithdrawalDate = ((from j in AccountTransactions.Where(c => c.Value < 0.00) select j.Key.DayOfYear).Count() > 0) ? (from j in AccountTransactions.Where(c => c.Value < 0.00) select j.Key.DayOfYear).Max() : 0;
            int Daysback = DateTime.Today.DayOfYear - 10;
            if (Daysback > LatestWithdrawalDate)
            {
                return (DateTime.Today.DayOfYear - EarliestDepostiDatetime.DayOfYear) * .05 / 365 * values;
            }
            else
            {
                return (DateTime.Today.DayOfYear - EarliestDepostiDatetime.DayOfYear) * .001 / 365 * values;
                //return (values <= 1000) ? values * .02 : (Enumerable.Range(1000, 2000).Contains((int)values)) ? 20 + (values - 1000) * .05 : 70 + (values - 2000) * .01;
            }

        }

            
    }
}
