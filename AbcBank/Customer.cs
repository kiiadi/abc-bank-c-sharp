using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;
using System.Linq.Expressions;


namespace AbcBank
{
    public class Customer: ICustomer
    {
        private String _name;
        private List<IAccount> _accounts;

        public String Name
        {
            get { return _name; }
        }

        public Customer(String name)
        {
            this._name = name;
            this._accounts = new List<IAccount>();
        }

        public void OpenAccount(IAccount account)
        {
            if (account is Account)
            {
                _accounts.Add(account);         
            }                 
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public double TotalInterestEarned()
        {
            return _accounts.Sum(x => x.InterestEarned());
        }

        public double TotalInterestEarned(DateTime now)
        {
            return -1;
            //return accounts.Sum(x => x.InterestEarned(now));
        }

        public TransferResult TransferFunds(IAccount accountFrom, IAccount accountTo, double amount)
        {
            try
            {
                if ((accountFrom.Balance() - amount) < 0)
                    return TransferResult.InvalidFunds;

                accountFrom.Withdraw(amount);
                accountTo.Deposit(amount);

                return TransferResult.Transferred;
            }
            catch
            {
                return TransferResult.Error;
            }
        }

        /*******************************
         * This method gets a statement
         *********************************/
        public String GetStatement()
        {
            var statement = new StringBuilder();
         
            statement.AppendLine("Statement for " + _name );
            double total = _accounts.Sum(x=> x.Balance());
            foreach (Account a in _accounts)
            {
                statement.AppendLine("\r\n" + StatementForAccount(a));
            }
            statement .Append ("\r\nTotal In All Accounts " + ToDollars(total));
            return statement.ToString();
        }

        private String StatementForAccount(Account account)
        {
            var statement = new StringBuilder();

            //Translate to pretty account type
            statement.AppendLine(account.AccountType());

            //Now total up all the transactions
            foreach (Transaction t in account.Transactions)
            {
                statement.AppendLine("  " + (t.TransactionType == TransactionType.Withdrawal ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount));
            }

            statement.Append("Total " + ToDollars(account.Balance()));
            return statement.ToString();
        }

        private String ToDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
