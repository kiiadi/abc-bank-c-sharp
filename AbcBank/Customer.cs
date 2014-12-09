
﻿using System;

﻿using AbcBank.AccountManager;
using System;
using System.Collections.Generic;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Customer
    {
        private String name;
        private List<IAccount> accounts;
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String getName()
        {
            return name;
        }

 
        public IAccount openAccount(AccountType accountType)
        {
            //To do - dependency injection for factory class
            var accountFactory = new AccountFactory();
            IAccount account = accountFactory.createAccount(accountType);

            accounts.Add(account);

            return account;
        }

        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double totalInterestEarned()
        {
            double total = 0;

             foreach (IAccount a in accounts)
                total += a.getInterest();
            return total;
        }



           
        
        /*

        
>>>>>>> parent of 8d292fa... Revert "Only added one feature while refactoring"
        public String getStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement = "Statement for " + name + "\n";
            double total = 0.0;
<<<<<<< HEAD
            foreach (Account a in accounts)
=======
            foreach (IAccount a in accounts)
>>>>>>> parent of 8d292fa... Revert "Only added one feature while refactoring"
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

<<<<<<< HEAD
        private String statementForAccount(Account a)
=======
        private String statementForAccount(IAccount a)
>>>>>>> parent of 8d292fa... Revert "Only added one feature while refactoring"
        {
            String s = "";

            //Translate to pretty account type
            switch (a.getAccountType())
            {
<<<<<<< HEAD
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
=======
                case AccountType.Checking:
                    s += "Checking Account\n";
                    break;
                case AccountType.Savings:
                    s += "Savings Account\n";
                    break;
                case AccountType.MaxiSavings:
>>>>>>> parent of 8d292fa... Revert "Only added one feature while refactoring"
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
<<<<<<< HEAD
=======
         * */
    }
}
