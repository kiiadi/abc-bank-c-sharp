using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class Account
    {
        #region nested types
        public enum AccountType
        {
            Checking,
            Saving,
            MaxiSaving
        }
        #endregion

        #region private fields
        private readonly AccountType _accountType;
        private List<Transaction> _transactions = new List<Transaction>();
        #endregion

        #region ctor(s)
        public Account(AccountType accountType)
        {
            _accountType = accountType;
        }
        #endregion

        #region public methods/properties
        public IEnumerable<Transaction> Transactions { get { return _transactions; } }
        public AccountType Type { get { return _accountType; } }
        public void Deposit(double amount)
        {
            DepositImpl(amount);
        }
        public void Withdraw(double amount)
        {
            DepositImpl(-amount);
        }
        public double InterestEarned()
        {
            double amount = SumTransactions();
            switch (_accountType)
            {
                case AccountType.Saving:
                    return GetSavingInterest(amount);
                // case SUPER_SAVINGS:
                //     if (amount <= 4000)
                //         return 20;
                case AccountType.MaxiSaving:
                    return GetMaxiSavingInterest(amount);
                case AccountType.Checking:
                    return GetCheckingInterest(amount);
                default:
                    throw new InvalidOperationException(string.Format("Unknown account type: {0}", _accountType));
            }
        }
        public double SumTransactions()
        {
            return _transactions.Select(t => t.Amount).Sum();
        }
        public String BuildStatement()
        {
            StringBuilder s = new StringBuilder();

            //Translate to pretty account type
            switch (_accountType)
            {
                case Account.AccountType.Checking:
                    s.AppendLine("Checking Account");
                    break;
                case Account.AccountType.Saving:
                    s.AppendLine("Savings Account");
                    break;
                case Account.AccountType.MaxiSaving:
                    s.AppendLine("Maxi Savings Account");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in _transactions)
            {
                s.AppendFormat("  {0} {1}\n", (t.Amount < 0 ? "withdrawal" : "deposit"), t.Amount.ToDollars());
                total += t.Amount;
            }
            s.AppendFormat("Total {0}", total.ToDollars());
            return s.ToString();
        }
        #endregion

        #region private methods
        private void DepositImpl(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount must be greater than zero");

            _transactions.Add(new Transaction(amount));
        }
        private static double GetSavingInterest(double amount)
        {
            if (amount <= 1000)
                return amount * 0.001;

            return 1 + (amount - 1000) * 0.002;
        }
        private static double GetMaxiSavingInterest(double amount)
        {
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }
        private static double GetCheckingInterest(double amount)
        {
            return amount * 0.001;
        }
        #endregion
    }
}
