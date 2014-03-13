using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountsInterface;
using AbcBank.utilities;

namespace AbcBank.Implementation
{
    public class SavingsAccount : Transactions, IAccountsInterface
    {
        private const int _accountType = 1;
        private String _accountName = "Savings Account";

        public SavingsAccount()
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
            return (values <= 1000) ? values * .001 : 1 + ((values - 1000) * .002);
        }

       
    }
}
