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
        int AgeInDays { get; }
    }

    public class Transaction : ITransaction
    {
        private readonly DateTime transactionDateTime;

        #region Properties
        public double Amount { get; private set; }

        public string Type 
        {
            get { return Amount < 0 ? TransactionTypes.WITHDRAWAL : TransactionTypes.DEPOSIT; }
        }

        public int AgeInDays
        {
            get { return (DateTime.Now - transactionDateTime).Days; }
        }
        #endregion

        public Transaction(double amount)
        {
            Amount = amount;
            transactionDateTime = DateTime.Now;
        }
    }
}
