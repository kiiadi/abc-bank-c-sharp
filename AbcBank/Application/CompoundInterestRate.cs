using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Application
{
    public static class CompoundInterestRate
    {
        //x, 0.05, 365
        public static double Calculate(double principalAmount, double annualNominalRate, int numberOfDays)
        {
            return principalAmount * Math.Pow(1 + annualNominalRate / 365, numberOfDays);
        }
    }
}
