namespace AbcBank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Customer : ICustomer
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Ssn { get; private set; }

        public string Gender { get; private set; }

        public string Title { get; private set; }

        public string Address { get; set; }

        public string Phone { get; set; }


        public Customer(string fname_, string lname_, string ssn_, string address_, string phone_, string gender_ = "", string title_ = "")
        {
            this.FirstName = fname_;
            this.LastName = lname_;
            this.Ssn = ssn_;
            this.Address = address_;
            this.Phone = phone_;
            this.Gender = gender_;
            this.Title = title_;

            this.Accounts = new ConcurrentList<BankAccount>();
        }

        public ConcurrentList<BankAccount> Accounts { get; set; }

        public IList<string> GetAccounts()
        {
            var list = (from t in Accounts select t.OwnerAccountId).ToList();

            return list;
        }

        public void InternalTransfer(BankAccount fromAccount_, BankAccount toAccount_, decimal amount_)
        {
            if (fromAccount_ == null || toAccount_ == null) throw new ArgumentException(string.Format("Account(s) are null"));

            fromAccount_.Withdraw(DateTime.Now, amount_);
            toAccount_.Deposit(DateTime.Now, amount_);
        }

        public int NumberOfAccounts()
        {
            return Accounts.Count;
        }

        public decimal TotalInterestEarned()
        {
            return this.Accounts.Sum(a_ => a_.InterestEarned());
        }

        public String TotalAccountsStatement()
        {
            var statement = new StringBuilder();
            var titleName = string.Format("{0} {1} {2}", this.Title, this.FirstName, this.LastName);

            statement.Append("TotalStatement for ").Append(titleName).Append(Environment.NewLine);

            var total = 0.0M;
            foreach (var a in this.Accounts)
            {
                statement.Append(Environment.NewLine).Append(AccountStatement(a));
                total += a.ActionsHistory.Balance;
            }
            statement.Append(Environment.NewLine).Append("The Total of all ").Append(titleName).Append("'s Accounts: ").Append(Utils.Dollars(total)).Append(Environment.NewLine);

            return statement.ToString();
        }

        public String CustomerAccountsStatement(string customer_)
        {
            var statement = new StringBuilder();
            var titleName = string.Format("{0} {1} {2}", this.Title, this.FirstName, this.LastName);

            statement.Append("TotalStatement for ").Append(titleName).Append(Environment.NewLine);

            var total = 0.0M;
            foreach (var a in this.Accounts)
            {
                statement.Append(Environment.NewLine).Append(AccountStatement(a));
                total += a.ActionsHistory.Balance;
            }
            statement.Append(Environment.NewLine).Append("The Total of all ").Append(titleName).Append("'s Accounts: ").Append(Utils.Dollars(total)).Append(Environment.NewLine);

            return statement.ToString();
        }

        private String AccountStatement(BankAccount a_)
        {
            var s = new StringBuilder();

            s.Append(a_.AccountType).Append(Environment.NewLine);
            s.Append(a_.ActionsHistory.TransactionsSummary());

            return s.ToString();
        }
    }
}