using System;

namespace AbcBank
{
    public class Transaction
    {
        private double fAmount;
        public double Amount { get { return fAmount; } }

        private DateTime fDate;
        public DateTime Date { get { return fDate; } set { fDate = value; } }

        public Transaction(double amount)
        {
            fAmount = amount;
            Date = DateTime.Now;
        }
    }
}
