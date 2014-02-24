using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public class AccountCheckingRateCalculator : IAccountRateCalculator
    {
        public double Calculate(IAccountForRateCalculators account)
        {
            List<InvestmentPeriod> investmentPeriods = account.InvestmentPeriods;

            double compoundedAmount = 0.0;
            foreach (var item in investmentPeriods)
            {
                compoundedAmount = CompoundInterestRate.Calculate(compoundedAmount + item.amount, 0.001, item.days);
            }

            return compoundedAmount - account.CurrentAmount;
        }
        
    }
}
