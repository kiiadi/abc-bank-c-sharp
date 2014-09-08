using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class Customer
    {
        private String fName;
        public String Name { get { return fName; } set { fName = value; } }

        private List<Account> Accounts;

        public int NumberOfAccounts
        {
            get { return Accounts.Count; }
        }

        public Customer(String name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public void OpenAccount(Account account)
        {
            Accounts.Add(account);
        }

        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (Account a in Accounts)
                total += a.InterestEarned();
            return total;
        }

        ///
        /// <summary>
        /// Gets statement that shows transactions and totals for each of this customer's accounts
        /// </summary>
        /// <returns>
        /// String containig the statement
        /// </returns>
        public String GetStatement()
        {
            String statement = null; //reset statement to null here
            statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts)
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.Balance;
            }
            statement += "\nTotal In All Accounts " + Utility.ToDollars(total);
            return statement;
        }

        private String StatementForAccount(Account a)
        {
            String s = "";

            s = a.Type.ToString().Replace('_', '-') + " Account\n";

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + Utility.ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + Utility.ToDollars(total);
            return s;
        }
    }
}
