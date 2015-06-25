using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank.AbcAccount
{
    public class Account
    {
        private readonly AccountType accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType)
        {
            this.accountType = accountType;
            transactions = new List<Transaction>();
        }

        public void deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Constants.AmountGreaterThanZero);
            }
            else
            {
                transactions.Add(new Transaction(amount, accountType));
            }
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Constants.AmountGreaterThanZero);
            }
            else
            {
                transactions.Add(new Transaction(-amount, accountType));
            }
        }

        /// <summary>
        /// for transfer considered example of checking and transfer to savings
        /// </summary>
        /// <param name="sourceAccount"></param>
        /// <param name="targetAccount"></param>
        /// <param name="amount"></param>
        public void Transfer(Account targetAccount, Double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Constants.AmountGreaterThanZero);
            }
            transactions.Add(new Transaction(-amount, accountType));
            //deposit the amount in the target account
            targetAccount.deposit(amount);
        }

        ///
        public double interestEarned()
        {
            var accountManager = new AccountManager();
            var amount = CalculationHelper.SumTransactions(transactions);
            return accountManager.getInterestEarned(accountType, amount);
        }

        public AccountType getAccountType()
        {
            return accountType;
        }
    }
}
