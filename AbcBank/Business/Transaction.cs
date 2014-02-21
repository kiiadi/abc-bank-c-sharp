using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface ITransaction
    {
        double Amount { get; }
        string Type { get; }
    }

    public class Transaction : ITransaction
    {
        private readonly DateTime transactionDate;
        
        public double Amount { get; private set; }

        public string Type 
        {
            get { return Amount < 0 ? TransactionTypes.WITHDRAWAL : TransactionTypes.DEPOSIT; }
        }

        public Transaction(double amount)
        {
            Amount = amount;
            transactionDate = DateTime.Now;
        }
    }
}
