using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public class AccountMaxiSavingsRateCalculator : IAccountRateCalculator
    {
        public double Calculate(double amount)
        {
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }
    }
}
