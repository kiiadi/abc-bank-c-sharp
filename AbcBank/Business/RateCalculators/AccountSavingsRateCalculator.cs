using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public class AccountSavingsRateCalculator : IAccountRateCalculator
    {
        public double Calculate(IAccountForRateCalculators account)
        {
            List<InvestmentPeriod> investmentPeriods = account.InvestmentPeriods;

            double compoundedAmount = 0.0;
            foreach (var item in investmentPeriods)
            {
                double principalAmount = compoundedAmount + item.amount;
                double rate = principalAmount <= 1000 ? 0.001 : 0.002;
                compoundedAmount = CompoundInterestRate.Calculate(principalAmount, rate, item.days);
            }

            return compoundedAmount - account.CurrentAmount;
        }
    }
}
