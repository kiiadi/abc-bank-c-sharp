using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public class AccountMaxiSavingsRateCalculator : IAccountRateCalculator
    {
        public double Calculate(IAccountForRateCalculators account)
        {
            double rate = NoWithdrawalsWithin10Days(account) ? 0.05 : 0.001;

            List<InvestmentPeriod> investmentPeriods = account.InvestmentPeriods;

            double compoundedAmount = 0.0;
            foreach (var item in investmentPeriods)
            {
                compoundedAmount = CompoundInterestRate.Calculate(compoundedAmount + item.amount, rate, item.days);
            }

            return compoundedAmount - account.CurrentAmount;
        }

        private bool NoWithdrawalsWithin10Days(IAccountForRateCalculators account)
        {
            int index = account.Transactions.Count - 1;
            while (index >= 0 && account.Transactions[index].AgeInDays <= 10)
            {
                if (account.Transactions[index].Type == TransactionTypes.WITHDRAWAL)
                {
                    return false;
                }
                index--;
            }

            return true;
        }
    }
}
