using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    public class Account
    {
        public const string AmountIsNotPositiveMessage = "amount must be positive";
        public const string AmountExceedsBalanceMessage = "withdraw: amount {0} exceeds balance {1}";

        private AccountType fType;
        public AccountType Type { get { return fType; } }

        private double fBalance;
        public double Balance
        {
            get
            {
                fBalance = 0;
                foreach (Transaction t in Transactions)
                    fBalance += t.Amount;
                return fBalance;
            }
        }

        public List<Transaction> Transactions;

        public Account(AccountType accountType)
        {
            fType = accountType;
            Transactions = new List<Transaction>();
        }

        ///
        /// <summary>
        ///     Credit the account with the amount
        /// </summary>
        /// <param>
        ///     name="amount" - deposited amount
        /// </param>
        /// Exceptions:
        ///   System.ArgumentException:
        ///       amount is not positive.
        ///       
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(AmountIsNotPositiveMessage);
            }

            Transactions.Add(new Transaction(amount));
        }

        ///
        /// <summary>
        ///     Debit the account with the amount
        /// </summary>
        /// <param>
        ///     name="amount" - withdrawn amount
        /// </param>
        /// Exceptions:
        ///   System.ArgumentException:
        ///       amount is not positive.
        ///       amount exceeds balance.
        ///       
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(AmountIsNotPositiveMessage);
            }

            if (amount > Balance)
            {
                String message = String.Format(Account.AmountExceedsBalanceMessage, amount, Balance);
                throw new ArgumentException(message);
            }

            Transactions.Add(new Transaction(-amount));
        }

        /// <summary>
        ///     Calculates SIMPLE interest by type of the account
        /// </summary>
        /// <returns>
        ///     Total historical interest earned
        /// </returns>
        /// TODO Replace the below calculations per pending requarement:
        ///     "Interest rates should accrue daily (incl. weekends), rates *below* are per-annum" 
        public double InterestEarned()
        {
            double amount = Balance;
            switch (Type)
            {
                case AccountType.Checking:
                    amount *= 0.001;
                    break;
                case AccountType.Savings:
                    if (amount <= 1000)
                        amount *= 0.001;
                    else
                        amount = 1000 * 0.001 + (amount - 1000) * 0.002;
                    break;
                case AccountType.Maxi_Savings:
                //Implements requirement
                //  "Change **Maxi-Savings accounts** to have an interest rate of 5%,
                //   assuming no withdrawals in the past 10 days, otherwise 0.1%"
                //   TODO Replace temporary change below with true accrue daily per TODO Task for the method
                    if (DateTime.Now - Transactions.Last().Date > TimeSpan.FromDays(10))
                        amount *= 0.05;
                    else
                        amount *= 0.001;
                    break;
            }
            return amount;
        }
    }
}
