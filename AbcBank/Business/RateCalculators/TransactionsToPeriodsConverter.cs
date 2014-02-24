using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Business
{
    public interface ITransactionsToPeriodsConverter
    {
        List<InvestmentPeriod> Calculate(List<ITransaction> transactions);
    }

    public class TransactionsToPeriodsConverter : ITransactionsToPeriodsConverter
    {
        public List<InvestmentPeriod> Calculate(List<ITransaction> transactions)
        {
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>();

            for (int i = 1; i < transactions.Count; i++)
            {
                InvestmentPeriod investmentPeriod;
                investmentPeriod.amount = transactions[i - 1].Amount;
                investmentPeriod.days = transactions[i - 1].AgeInDays - transactions[i].AgeInDays;
                investmentPeriods.Add(investmentPeriod);
            }

            if (transactions.Count > 0)
            {
                InvestmentPeriod investmentPeriod;
                investmentPeriod.amount = transactions[transactions.Count - 1].Amount;
                investmentPeriod.days = transactions[transactions.Count - 1].AgeInDays;
                investmentPeriods.Add(investmentPeriod);
            }

            return investmentPeriods;
        }
    }
}
