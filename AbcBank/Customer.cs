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
        public String Name
        {
            get { return name; }
        }

        private List<Account> accounts;
        public List<Account> Accounts
        {
            get { return accounts; }
        }

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

    
        public Customer openAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in Accounts)
                total += a.interestEarned();
            return total;
        }

        public String getStatement()
        {
            String statement;
            statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = a.getStringRepresentationForAccount(a);

            //Total all transactions for the customer
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + toDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
