using System;

namespace AbcBank
{
    public class Transaction
    {
        public readonly double Amount;

        public Transaction(double amount)
        {
            Amount = amount;
            TransactionDate = DateProvider.now();
        }

        public DateTime TransactionDate { get; private set; }
    }
}
