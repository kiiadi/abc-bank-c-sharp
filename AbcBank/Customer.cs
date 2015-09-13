using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class Customer
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer(String name)
        {
            this.Name = name;
            this.Accounts = new List<Account>();
        }

        public Customer openAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public double totalInterestEarned()
        {
            double total = 0;

            total = Accounts.Sum(x => x.interestEarned());

            return total;
        }

        /*******************************
         * This method gets a statement
         *********************************/
        public String getStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            //Ishmael Sessions - String builder is much more efficient.
            StringBuilder statement = new StringBuilder();
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement.Append("Statement for " + Name + "\n");
            double total = 0.0;
            
            //Linq foreach is easier to read/maintain.
            Accounts.ForEach(x => {
                statement.Append(string.Format("\n{0}\n", statementForAccount(x)));
                total += x.sumTransactions();
            });

            statement.Append(string.Format("\nTotal In All Accounts {0}", toDollars(total)));
            return statement.ToString();
        }

        private String statementForAccount(Account a)
        {
            //IS - StringBuilder is more efficient.
            StringBuilder s = new StringBuilder();

            //Translate to pretty account type
            switch (a.AccountType)
            {
                case AccountType.CHECKING:
                    s.Append("Checking Account\n");
                    break;
                case AccountType.SAVINGS:
                    s.Append("Savings Account\n");
                    break;
                case AccountType.MAXI_SAVINGS:
                    s.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            //Linq foreach is easier to read/maintain.
            a.Transactions.ForEach(x => {
                s.Append(string.Format(" {0} {1}\n", (x.Amount < 0 ? " withdrawal" : " deposit"), toDollars(x.Amount)));
                total += x.Amount;
            });

            s.Append(string.Format("Total {0}", toDollars(total)));
            return s.ToString();
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
