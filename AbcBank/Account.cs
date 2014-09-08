using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    public class Account
    {
        private const string AmountIsNotPositiveMessage = "amount must be positive";
        private const string AmountExceedsBalanceMessage = "withdraw: amount {0} exceeds balance {1}";

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
        ///     Debit this account with the amount
        ///     Credit the accountTo account with the amount
        /// </summary>
        /// <param>
        ///     name="accountTo" - account to which money is transferred
        /// </param>
        /// <param>
        ///     name="amount" - transferred amount
        /// </param>
        /// Exceptions:
        ///   System.ArgumentException:
        ///       amount is not positive.
        ///       amount exceeds balance.
        public void TransferToAccount(Account accountTo, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Account.AmountIsNotPositiveMessage);
            }

            if (amount > Balance)
            {
                String message = String.Format(Account.AmountExceedsBalanceMessage, amount, Balance);
                throw new ArgumentException(message);
            }

            Transactions.Add(new Transaction(-amount));
            accountTo.Transactions.Add(new Transaction(amount));
        }

        /// <summary>
        ///     Calculates SIMPLE interest by type of the account.
        ///         Interest rates accrue daily, including weekends.  Rates are per-annum. 
        /// </summary>
        /// <returns>
        ///     Total historical interest earned
        /// </returns>
        public double InterestEarned()
        {
            const double checkingDailyRate = 0.001 / 365;                     //Checking accounts have a flat rate of 0.1%
            const double savingsFirst1000DaylyRate = 0.001 / 365;             //Savings accounts have a rate of 0.1% for the first $1,000,
            const double savingsAfter1000DaylyRate = 0.002 / 365;             // AND then 0.2% for balance above $1,000
            const double maxi_savingsNoWithdrawalDaylyRate = 0.05 / 365;      //Maxi-Savings accounts have a rate of 5%,
            //  if no withdrawal was made in prior 10 days
            const double maxi_savingsWithdrawalLast10DaylyRate = 0.001 / 365; //    otherwise, Maxi-Savings accounts have a rate of 0.1%

            if (Transactions.Count == 0)
                return 0.0;

            double amount = 0.0;
            double balance = 0.0;
            DateTime lastLowInterestDay = Transactions[0].Date.AddDays(-1);  //only for maxi-saving 

            for (int i = 0; i < Transactions.Count; i++)
            {
                balance += Transactions[i].Amount;
                DateTime startDate = Transactions[i].Date;
                DateTime endDate = i + 1 < Transactions.Count ? Transactions[i + 1].Date : DateTime.Now;
                int days = (endDate - startDate).Days;

                switch (Type)
                {
                    case AccountType.Checking:
                        amount += balance * checkingDailyRate * days;
                        break;
                    case AccountType.Savings:
                        if (balance <= 1000)
                            amount += balance * savingsFirst1000DaylyRate * days;
                        else
                            amount += (1000 * savingsFirst1000DaylyRate + (balance - 1000) * savingsAfter1000DaylyRate) * days;
                        break;
                    case AccountType.Maxi_Savings:
                        if (Transactions[i].Amount < 0)
                            lastLowInterestDay = Transactions[i].Date + TimeSpan.FromDays(10);

                        if ((lastLowInterestDay - startDate).Days > 0)
                        {
                            DateTime lastIncludedDay = endDate < lastLowInterestDay ? endDate : lastLowInterestDay;
                            amount += balance * maxi_savingsWithdrawalLast10DaylyRate * (lastIncludedDay - startDate).Days;
                        }
                        if ((endDate - lastLowInterestDay).Days > 0)
                        {
                            DateTime firstIncludedDay = startDate > lastLowInterestDay ? startDate : lastLowInterestDay;
                            amount += balance * maxi_savingsNoWithdrawalDaylyRate * (endDate - firstIncludedDay).Days;
                        }
                        break;
                }
            }
            return amount;
        }
    }
}
