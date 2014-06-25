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

        // Customer can only open one of each account type
        public Customer openAccount(Account account)
        {
            if (Accounts.Where(a => a.Type == account.Type).Count() != 0)
                throw new Exception(String.Format("Account type:{0} already exists for this customer", account.Type.ToString()));
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


        public void transferFunds(Account.AccountType source, Account.AccountType destination, double amount)
        {
            // Only one type of each account is allowed for a customer 
            Account acctFrom = Accounts.Where(a => a.Type == source).FirstOrDefault();
            if (acctFrom == null)
                throw new Exception("Cannot complete transfer. Source account does not exist");
            Account acctDest = Accounts.Where(a => a.Type == destination).FirstOrDefault();
            if (acctDest == null)
                throw new Exception("Cannot complete transfer. Destination account does not exist");
            bool bAcctFromSuccess = false;
            bool bAcctDestSuccess = false;
            try
            {
                acctFrom.withdraw(amount);
                bAcctFromSuccess = true;
                acctDest.deposit(amount);
                bAcctDestSuccess = true;
            }
            // roll back entire transaction if there was an exception
            catch (Exception)
            {
                if (bAcctFromSuccess == true)
                    acctFrom.deposit(amount);
                if (bAcctDestSuccess)
                    acctDest.withdraw(amount);
            }
        }

        private String statementForAccount(Account a)
        {
            String s = a.getStringRepresentationForAccount();

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
