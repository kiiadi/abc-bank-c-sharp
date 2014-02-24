using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public interface ICustomerInterestCalculator
    {
        double Calculate(IEnumerable<IAccount> accounts);
    }

    public class CustomerInterestCalculator : ICustomerInterestCalculator
    {
        public double Calculate(IEnumerable<IAccount> accounts)
        {
            return accounts.Sum(a => a.InterestEarned);
        }
    }
}
