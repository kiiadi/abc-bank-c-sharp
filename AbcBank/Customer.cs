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

        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.interestEarned();
            return total;
        }
        public bool TransferFunds(Account fromAccount, Account toAccount, double amount)
        {
            bool v_success = false;
            try
            {
                if (amount > 0 && fromAccount != null && toAccount != null)
                {
                    double previousTransaction_fromAccount = fromAccount.sumTransactions();
                    double previousTransaction_toAccount = toAccount.sumTransactions();

                    fromAccount.withdraw(amount);
                    toAccount.deposit(amount);

                    double currentTransaction_fromAccount = fromAccount.sumTransactions();
                    double currentTransaction_toAccount = toAccount.sumTransactions();

                    v_success = (previousTransaction_fromAccount - currentTransaction_fromAccount == currentTransaction_toAccount - previousTransaction_toAccount);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error transfering funds of amount: " + Utility.toDollars(amount) ,ex));

            }
            return v_success; 
            
        }

        /*******************************
         * This method gets a statement
         *********************************/
        public String getStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            StringBuilder statement = new StringBuilder();
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement.AppendLine("Statement for " + name);
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement.Append("\n" + statementForAccount(a) + "\n");
                total += a.sumTransactions();
            }
            statement.Append("\n" + "Total In All Accounts " + Utility.toDollars(total));
            return statement.ToString();
        }

        private String statementForAccount(Account a)
        {
           StringBuilder sbr = new StringBuilder();
           sbr.AppendLine(a.getAccountType());

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                sbr.Append("  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + Utility.toDollars(t.amount) + "\n");
                total += t.amount;
            }
            sbr.Append("Total " + Utility.toDollars(total));
            return sbr.ToString();;
        }

    }
}
