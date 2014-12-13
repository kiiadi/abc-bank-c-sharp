using System;
using System.Collections.Generic;
using System.Text;

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

        public String getName()
        {
            return name;
        }

        public Customer openAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }

        public List<Account> getAccounts()
        {
            return accounts;
        }

        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.interestEarned();
            return total;
        }

        /*******************************
         * This method gets a statement
         *********************************/
        public String getStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.getAccountType())
            {
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + toDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }

        public String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }

        public double getAccruedInterest()
        {
            bool transactionsExist = false;
            foreach (Account a in accounts)
            {
                if (a.transactions.Count > 0) 
                {
                    transactionsExist = true;
                }
            }            
            if (!transactionsExist)
            {
                throw new ArgumentException("Cannot get accrued interest. There is nothing in the bank");
            }
            float annualInterest = 0.05f;
            DateTime min_date = DateTime.Now;
            double accrIntr = 0;
            double total = 0.0;
            foreach (Account a in accounts)
            {
                foreach (Transaction t in a.transactions)
                {
                    if (DateTime.Compare(t.transactionDate, min_date) < 0)
                    {
                        min_date = t.transactionDate;
                    }
                }
                total += a.sumTransactions();
            }                        



            accrIntr = Math.Ceiling((DateTime.Now - min_date).TotalDays) / 365 * total * annualInterest;            
            return accrIntr;
        }
    }
}
