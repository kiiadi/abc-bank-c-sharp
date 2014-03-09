using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank
{
    public class Transaction: ITransaction
    {
        public readonly double Amount;
        public readonly TransactionType TransactionType;
        public readonly DateTime TransactionDate;

        public Transaction(double amount, DateTime transactionDate)
        {
            this.Amount = amount;
            this.TransactionType = (amount < 0 ? TransactionType.Withdrawal : TransactionType.Deposit);
            this.TransactionDate = transactionDate;
        }

    }
}
