using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Customer
    {
        /** Private member that represents the account type of the current/instant account object **/
        private string name;

        /** Generic list collection of the accounts opened by the current/instant customer object **/
        private List<Account> accounts;

        /** Constructor a customer object and initalize the name member using the value of the passed parameter 
         * @param name - The name of the customer represented by the instant customer object
         **/
        public Customer(string name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        /** Return the name of the current/instant customer object **/
        public string getName()
        {
            return name;
        }

        /** Return the name of the current/instant customer object **/
        public Customer openAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        /** Return the number of accounts associated with the current/instant customer object **/
        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }

        /** Return the total amount of interest earned for all the accounts associated with 
         * the current/instant customer object 
         **/
        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
            {
                Console.WriteLine(a.interestEarned());
                total += a.interestEarned();
            }

            return total;
        }

        /** Trigger for functions that will produce the bank statement for the current/instant customer object **/
        public string getStatement()
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

        /** Return a bank statement for the current/instant customer object **/
        private string statementForAccount(Account a)
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

        /** Return the dollar amount of the passed parameter that represents the total funds/transaction of an account **/
        private string toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }

        /** Transfer/move a specified amount of funds/money between various accounts of a customer
         * @ param FromAccount - the source account of the funds being transferred 
         * @ param ToAccount - the destination account of the funds being transferred
         * @ param amount - the total amount of funds to be transferred
         **/
        public void transferFunds(ref Account FromAccount, ref Account ToAccount, double amount)
        {
            if (amount > FromAccount.sumTransactions())
            {
                throw new ArgumentException("insufficient available funds for the specified transfer amount");
            }
            else
            {
                FromAccount.withdraw(amount);
                ToAccount.deposit(amount);
            }
        }
    }
}
