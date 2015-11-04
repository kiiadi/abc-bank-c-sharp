using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public static class CalculationHelper
    {

        /// <summary>
        /// Return sum of all transactions 
        /// </summary>
        /// <returns></returns>
        public static double SumTransactions(List<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
            {
                throw new Exception("No transactions exists");
            }
            double amount = 0.0;
            foreach (Transaction tran in transactions)
                amount += tran.amount;
            return amount;
        }

    }
}
