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
            if (NoWithdrawalsWithin10Days(account))
                return account.CurrentAmount * 0.05;
            else
                return account.CurrentAmount * 0.001;
        }

        private static bool NoWithdrawalsWithin10Days(IAccountForRateCalculators account)
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
