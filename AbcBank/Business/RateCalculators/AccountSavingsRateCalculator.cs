using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public class AccountSavingsRateCalculator : IAccountRateCalculator
    {
        public double Calculate(double amount)
        {
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }
    }
}
