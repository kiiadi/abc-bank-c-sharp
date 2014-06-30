using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        private readonly double _amount;
        private DateTime _transactionDate;

        public Transaction(double amount)
        {
            _amount = amount;
            _transactionDate = DateProvider.Instance.now();
        }
        public double Amount
        {
            get { return _amount; }
        }
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
        } 


    }
}
