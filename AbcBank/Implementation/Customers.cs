using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.CustomerInterface;
using AbcBank.AccountsInterface;
using AbcBank.utilities;


namespace AbcBank.Implementation
{
    public class Customers : ICustomerInterface
    {
        private readonly String _customerName;
        private List<IAccountsInterface> _customerAccounts ;
        private List<int> _accounts;
        private List<double> _accountTotals;
        

        public Customers(String CustName)
        {
            _customerName = CustName;
            _accounts = new List<int>();
            _customerAccounts = new List<IAccountsInterface>();
            _accountTotals = new List<double>();
        }

        public String CustomerName
        {
            get
            {
                return _customerName;
            }
        }

        public int TotalAccounts
        {
            get
            {
                return _accounts.Count;
            }
        }

        public void AddAccount(IAccountsInterface Accounts)
        {
            if (!_accounts.Contains(Accounts.AccountType))
            {
                _customerAccounts.Add(Accounts);
                _accounts.Add(Accounts.AccountType);
            }
        }

        public void Deposit(IAccountsInterface Accounts, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                if (_accounts.Contains(Accounts.AccountType))
                {
                    Accounts.EnterTransactions(amount);
                }
                else
                {
                    throw new ArgumentException(String.Format("customer does not have a {0} account to deposit money into.", Accounts.Name));
                }
            }
        }

        public void Withdraw(IAccountsInterface Accounts, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else if (amount > Accounts.AccountTransactions.Values.Sum())
            {
                throw new ArgumentException("cannot transfer amount as it is greater than what is currently available in your account");
            }
            else
            {
                if (_accounts.Contains(Accounts.AccountType))
                {
                    Accounts.EnterTransactions(-amount);
                }
                else
                {
                    throw new ArgumentException(String.Format("customer does not have a {0} account to withdraw money out of.", Accounts.Name));
                }
            }
        }

        public void Transfer(IAccountsInterface SourceAccount, IAccountsInterface DestinationAccount, double Amount)
        {
            if (SourceAccount.AccountTransactions.Values.Sum() < Amount)
            {
                throw new ArgumentException(String.Format("amount must be lesser than or equal to total held in Source Account {0}", SourceAccount.Name));
            }
            SourceAccount.EnterTransactions(-Amount);
            DestinationAccount.EnterTransactions(Amount);
        }

        public double totalInterestEarned()
        {
            foreach (IAccountsInterface a in _customerAccounts)
            {
                _accountTotals.Add(a.PerformInterestCalculations(a.AccountTransactions.Values.Sum()));
            }
            return _accountTotals.Sum();
        }


        public String GetAccountStatementforCustomer()
        {
            double accountTotal = 0.00;
            StringBuilder statement = new StringBuilder();
            statement.AppendLine(String.Format("Statement for {0} ", this.CustomerName));
            foreach (IAccountsInterface a in _customerAccounts)
            {
                statement.AppendLine(String.Format("\n  {0}  \n", GetStatementForAccount(a)));
                accountTotal += a.AccountTransactions.Values.Sum();
            }
            return statement.AppendLine(String.Format("\nTotal In All Accounts: {0}", toDollars(accountTotal))).ToString();
        }

        private String GetStatementForAccount(IAccountsInterface a)
        {
            StringBuilder s = new StringBuilder("");
            s.AppendLine(a.Name);
            //Now total up all the transactions
            foreach (double t in a.AccountTransactions.Values)
            {
                s.AppendLine(String.Format("\n  {0}  {1}  \n", toDollars(t), (t < 0 ? "withdrawal" : "deposit")));
                
            }
            return s.AppendLine(String.Format("Total: {0}", toDollars(a.AccountTransactions.Values.Sum()))).ToString();
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
