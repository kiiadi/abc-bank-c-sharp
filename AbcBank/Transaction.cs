using System;

namespace AbcBank
{
    public class Transaction
    {
        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public Transaction(double amount)
        {
            this.Amount = amount;
            this.TransactionDate = DateProvider.getInstance().now();
        }

    }
}
