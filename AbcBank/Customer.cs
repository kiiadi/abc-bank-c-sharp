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
        private Guid CustomerID; // Global UID prevents collisions should Customer name be same/similar as that of other customer(s)
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.CustomerID = Guid.NewGuid();
            this.accounts = new List<Account>();
        }

        public String getName()
        {
            return name;
        }

        public String getCustomerID()
        {
            return CustomerID.ToString();
        }

        // instantiate account and associate with customer
        public Customer openAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        // calculate number of accounts belonging to same customer
        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }


        // transfer funds between two accounts belonging to same customer - returns true if successful
        public bool transferFundsBetweenAccounts(double amount, Account accountOut, Account accountIn)
        {
            bool result = false;

            // first check whether amount being transferred is positive
            if (amount <= 0)
            {
                result = false;
            }
            else
            {
                // check whether customer has possession of accountOut/accountIn and whether there are sufficient funds in accountOut
                bool accountOutExists = false;
                bool accountOutBalPos = false;
                bool accountInExists = false;
                foreach (Account a in accounts)
                {
                    if (a.getAccountID() == accountOut.getAccountID())
                    {
                        accountOutExists = true;
                        break;
                    }
                }
                if (accountOutExists == true)
                {
                    // check whether account has sufficient funds
                    if (accountOut.accountBalance() >= amount)
                    {
                        accountOutBalPos = true;
                    }
                    else
                    {
                        accountOutBalPos = false;
                        // should include method for imposing fee on customer for Insufficient Funds, though ideally using false result
                    }
                }
                foreach (Account a in accounts)
                {
                    if (a.getAccountID() == accountIn.getAccountID())
                    {
                        accountInExists = true;
                        break;
                    }
                }

                // if outgoing account has positive balance AND ingoing account exists, proceed with transfer
                if ((accountOutBalPos == true) && (accountInExists == true))
                {
                    accountOut.withdraw(amount);
                    accountIn.deposit(amount);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }



        // calculate total interest earned throughout all accounts belonging to customer
        public double totalInterestEarned()
        {
            double total = 0.0;
            foreach (Account a in accounts)
                total += a.interestEarned();
            return total;
        }

        // this method produces an overall statement for the customer
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

        // this method prepares a statement for a given account
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

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
