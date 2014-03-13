using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AccountsInterface;
using AbcBank.utilities;

namespace AbcBank.Implementation
{
    public class CheckingAccount: Transactions, IAccountsInterface
    {
        private const int _accountType = 0;
        private String _accountName = "Checking Account";

        public CheckingAccount()
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
            return values * 0.001;
        }

       
    }
}
