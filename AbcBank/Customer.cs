using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Customer
    {
        private String name;
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public bool TransferFunds(Account fromAccount, Account toAccount, double transferAmount)
        {
            try
            {
                fromAccount.Withdraw(transferAmount);
                toAccount.Deposit(transferAmount);
                return true;
            }
            catch (Exception ex)
            {                
                throw new Exception("Error occured when transferring funds");
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

        /*******************************
         * This method gets a statement
         *********************************/
        public String GetStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.SumTransactions();
            }            
            statement += "\nTotal In All Accounts " + total.ToDollars();
            return statement;
        }

        private String StatementForAccount(Account a)
        {
            StringBuilder sbld = new StringBuilder();
            switch (a.GetAccountType())
            {
                case AccountType.CHECKING:
                    sbld.Append("Checking Account\n");
                    break;
                case AccountType.SAVINGS:
                    sbld.Append("Savings Account\n");
                    break;
                case AccountType.MAXI_SAVINGS:
                    sbld.Append("Maxi Savings Account\n");
                    break;
            }
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                sbld.Append("  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + t.amount.ToDollars() + "\n");
                total += t.amount;
            }
            sbld.Append("Total " + total.ToDollars());
            return sbld.ToString();

        }
    }
}
