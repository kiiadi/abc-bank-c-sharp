using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
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

        public Transaction(double amount)
        {
            this.amount = amount;
            this.utcDate = DateTime.UtcNow;
        }

    }
}
