using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public double amount;

        private DateTime transactionDate;

        private AccountType accountType;

        public Transaction(double amount, AccountType accountType)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.getInstance().now();
            this.accountType = accountType;
        }

    }
}
