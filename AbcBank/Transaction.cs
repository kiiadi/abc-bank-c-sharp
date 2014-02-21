using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank
{
    public class Transaction: iTransaction
    {
        public readonly double Amount;
        public TransactionType TransactionType;
        private DateTime _transactionDate;

        public Transaction(double amount, iDateProvider dateProvider)
        {
            this.Amount = amount;
            this.TransactionType = (amount < 0 ? TransactionType.Withdrawal : TransactionType.Deposit);
            this._transactionDate = dateProvider.Now();
        }

    }
}
