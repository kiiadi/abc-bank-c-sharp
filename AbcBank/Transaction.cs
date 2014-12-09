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

        public enum TransactionType
        {
            Deposit,
            Withdrawal
        }

        public TransactionType transactionType { get; set; }

        public Transaction(double amount, AccountAction action)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().now();

            switch (action)
            { 
                case AccountAction.Deposit:
                    this.transactionType = TransactionType.Deposit;
                    break;
                case AccountAction.Withdraw:
                    this.transactionType = TransactionType.Withdrawal;
                    break;
                   
            }
        }

        public double getTransAmount()
        {
            return this.amount;
        }

        public TransactionType getTransActionType()
        {
            return this.transactionType;
        }

        public DateTime getTransactionDate()
        {
            return this.transactionDate;
        }

    }
}
