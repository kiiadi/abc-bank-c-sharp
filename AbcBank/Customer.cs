using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class Customer
    {
        #region private fields
        private String _name;
        private List<Account> _accounts = new List<Account>();
        #endregion

        #region ctor(s)
        public Customer(String name)
        {
            this._name = name;
        }
        #endregion

        #region public methods
        public String Name { get { return _name; } }
        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }
        public int NumberOfAccounts { get{ return _accounts.Count;} }
        public double TotalInterestEarned()
        {
            return _accounts.Select(a => a.InterestEarned()).Sum();
        }
        /*******************************
         * This method gets a statement
         *********************************/
        public String BuildStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            //String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            StringBuilder statement = new StringBuilder();
            statement.AppendFormat("Statement for {0}{1}", _name, System.Environment.NewLine);

            double total = 0.0;
            _accounts.ForEach(
                a => { 
                    statement.AppendFormat("{0}{1}", System.Environment.NewLine, a.BuildStatement()); 
                    total += a.SumTransactions(); 
                }
            );
            statement.AppendFormat("{0}Total In All Accounts: {1}", System.Environment.NewLine, total.ToDollars());
            
            return statement.ToString();
        }
        #endregion
    }
}
