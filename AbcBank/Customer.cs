using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank
{
    public class Customer : iCustomer
    {
        private String name;
        private List<iAccount> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<iAccount>();
        }

        public String GetName()
        {
            return name;
        }

        public void OpenAccount(iAccount account)
        {
            if (account is Account)
            {
                accounts.Add(account);         
            }                 
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public TransferResult TransferFunds(iAccount accountFrom, iAccount accountTo, double amount)
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
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            //String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement.AppendLine("Statement for " + name );
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement.AppendLine("\r\n" + StatementForAccount(a));
                total += a.Balance();
            }
            statement .Append ("\r\nTotal In All Accounts " + ToDollars(total));
            return statement.ToString();
        }

        private String StatementForAccount(Account account)
        {
            var statement = new StringBuilder();

            //Translate to pretty account type
            switch (account.GetAccountType())
            {
                case AccountType.Checking:
                    statement.AppendLine("Checking Account");
                    break;
                case AccountType.Savings:
                    statement.AppendLine("Savings Account");
                    break;
                case AccountType.MaxiSavings:
                    statement.AppendLine("Maxi Savings Account");
                    break;
            }

            //Now total up all the transactions
            foreach (Transaction t in account.transactions)
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
