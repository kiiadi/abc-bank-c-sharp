using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.utilities
{
    public class Transactions
    {
        public readonly double amount;
        public readonly int Account;
        protected readonly Dictionary<DateTime, double> _transaction = new Dictionary<DateTime, double>();
        private DateTime transactionDate;
        int _millisecondValue = 0;

        public Transactions()
        {
        }

        public Dictionary<DateTime, double> AccountTransactions
        {
            get
            {
                return _transaction;
            }
        }

        public void EnterTransactions(double amount)
        {
            _millisecondValue += 1000;
            this.transactionDate = DateProvider.getInstance.now();
            if (_transaction.ContainsKey(this.transactionDate))
            {
                this.transactionDate = this.transactionDate.AddMilliseconds(_millisecondValue);
            }
            _transaction.Add(transactionDate, amount);
            
        }

        

    }
}
