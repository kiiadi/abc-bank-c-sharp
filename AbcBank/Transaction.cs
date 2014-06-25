using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public enum transactionType {WITHDRAWAL, DEPOSIT}
        private transactionType type;
        public transactionType Type
        {
            get { return type; }
        }

        private readonly double amount;
        public double Amount
        {
            get { return amount; }
        } 

        private DateTime utcDate;
        public DateTime UtcDate
        {
            get { return utcDate; }
            set { utcDate = value; }
        }

        public Transaction(double amount, transactionType type, DateTime date)
        {
            this.amount = amount;
            this.type = type;
            this.utcDate = date;
        }

    }
}
