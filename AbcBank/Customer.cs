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

        public Account getAccount(int AccountType)
        {
            return  accounts!= null ?  accounts.Where(account => account.getAccountType() == AccountType).Single() : null  ; 
        }
        public String getName()
        {
            return name;
        }

        public Customer openAccount(Account account)
        {
            if (accounts.Where(acc => acc.getAccountType() == account.getAccountType()).Count() == 0)
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

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }

        public double AccountBalance(int accountType)
        {
            var CurrentAccount = FetchAccountFromList(accountType);
            if(CurrentAccount!= null)
            {
                return CurrentAccount.sumTransactions();
            }
            return 0.0;
        }

        public string TransferBetweenAccounts(int Account1, int Account2,double amount)
        {
            var message = string.Empty;
            var balAccount1 = AccountBalance(Account1);
            var balAccount2 = AccountBalance(Account2);
            if(balAccount2-balAccount1 >= amount)
            {
                 FetchAccountFromList(Account1).deposit(amount);
                 FetchAccountFromList(Account2).withdraw(amount);
                 return message = string.Format("Transfer Compelete:  amount {0} moved from {1} (current balance {3}) to {2} (current balance {4}) ", amount, FetchAccountFromList(Account1).AccountTypeName(Account2),
                     FetchAccountFromList(Account1).AccountTypeName(Account1), AccountBalance(Account2), AccountBalance(Account1));
            }
            return message = string.Format("Transfer Failed : Amount {0} greater difference (second account - first account) {1} ({2}) - {3} ({4}) ", amount, FetchAccountFromList(Account1).AccountTypeName(Account2)
                , AccountBalance(Account2), FetchAccountFromList(Account1).AccountTypeName(Account1), AccountBalance(Account1));

        }
        private Account FetchAccountFromList(int accountType)
        {
            return accounts.Where(account => account.getAccountType() == accountType).Single();
        }

    }

}
