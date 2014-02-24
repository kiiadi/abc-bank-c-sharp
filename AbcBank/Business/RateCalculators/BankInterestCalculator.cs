using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public interface IBankInterestCalculator
    {
        double Calculate(IEnumerable<ICustomer> customers);
    }

    public class BankInterestCalculator : IBankInterestCalculator
    {
        public double Calculate(IEnumerable<ICustomer> customers)
        {
            return customers.Sum(c => c.TotalInterestEarned);
        }
    }
}
