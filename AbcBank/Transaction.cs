using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public readonly double amount;

        private DateTime transactionDate;

        public Transaction(double amount)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().now();
        }

        public double getAmount()
        {
            return amount;
        }

        public DateTime getTransactionDate()
        {
            return transactionDate;
        }


    }
}
