using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public interface IAccountRateCalculator
    {
        double Calculate(double amount);
    }
}
