﻿using System;
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
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Customer name cannot be empty");
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String getName()
        {
            return name;
        }

        public Customer openAccount(Account account)
        {
            int acctType = account.getAccountType();
           
            if (acctType.CompareTo(Account.CHECKING) == 0 || 
                acctType.CompareTo(Account.SAVINGS) ==0 ||
                acctType.CompareTo(Account.MAXI_SAVINGS) ==0 )
                 accounts.Add(account);
            else
                throw new ArgumentException("Account Type must be one of CHCECKING, SAVINGS and MAXI_SAVINGS");
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

        public double totalAcruedInterest()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.AccruedInterest();
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
                Console.WriteLine(s);
                total += t.amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }
        public void TransferFunds(Account Source, Account Destination, double amount)
        {
            Source.TransferFunds(Destination, amount);

        }
        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
