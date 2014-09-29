using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Customer
    {
        public const string INVALID_SOURCE_ACCOUNT = "Source account is not owned by customer";
        public const string INVALID_TARGET_ACCOUNT = "Target account is not owned by customer";
        public const string INVALID_TRANSFER_AMOUNT = "Transfer amount must be greater than zero";
        private readonly List<Account> accounts = new List<Account>();
        public Customer(string name)
        {
            Name = name;
        }
        public string Name{get; private set;}
        public double GetEarnedInterest()
        {
            return this.accounts.Sum(a => a.GetEarnedInterest());
        }
        public string GetStatement()
        {
            var statement = new StringBuilder("Statement for " + Name);
            double total = 0.0;
            foreach (var account in accounts)
            {
                statement.Append("\n" + account.GetStatement() + "\n");
                total += account.GetSumOfTransactions();
            }
            statement.Append("\nTotal In All Accounts " + Utility.ToDollars(total));
            return statement.ToString();
        }
        public string GetSummary()
        {
            switch (this.accounts.Count)
            {
                case 0:
                    return string.Format("- {0} (no accounts)", Name);
                case 1:
                    return string.Format("- {0} (1 account)", Name);
                default:
                    return string.Format("- {0} ({1} accounts)", Name, this.accounts.Count);
            }
        }
        public IEnumerable<Account> Accounts
        {
            get { return this.accounts; }
        }
        public void AddAccount(Account account)
        {
            this.accounts.Add(account);
        }
        
        public void Transfer(Account from, Account to, double amount)
        {

            if (!this.accounts.Contains(from))
                throw new Exception(INVALID_SOURCE_ACCOUNT);

            if (!this.accounts.Contains(to))
                throw new Exception(INVALID_TARGET_ACCOUNT);

            if (amount <= 0d)
                throw new Exception(INVALID_TRANSFER_AMOUNT);


            from.Withdraw(amount);
            to.Deposit(amount);
        }
    }
}
