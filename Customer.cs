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
        private List<IAccount> accounts;
       
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<IAccount>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(IAccount account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (IAccount a in accounts)
                total += a.interestearned();
            return total;
        }

        /*******************************
         * This method gets a statement
         *********************************/
        public String GetStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            //String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            StringBuilder s = new StringBuilder();
           // s.Append( "Statement for " + name +  Environment.NewLine );
            double total = 0.0;
            foreach (IAccount a in accounts)
            {
               // s.Append("\n");
                //s.Append(StatementForAccount(a) + Environment.NewLine);
                total += a.sumtransactions();
            }
            s.Append(ToDollars(total));
            return s.ToString();
        }

        private String StatementForAccount(IAccount a)
        {
            StringBuilder s = new StringBuilder();
            s.Append(a.nameofaccount+ " Account");
            
            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.gettrans)
            {
                s.Append("  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) +  Environment.NewLine );
                total += t.amount;
            }
            s.Append("Total " + ToDollars(total));
            return s.ToString();
        }

        private String ToDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
        public void TransferFunds(IAccount fromacctt, IAccount toacct, double amt) 
        {
            IAccount acct1 = fromacctt;
            IAccount acct2 = toacct;
            double amount = amt;

            acct1.withdraw(amt);
            acct2.deposit(amt);


        
        }
    }
}
