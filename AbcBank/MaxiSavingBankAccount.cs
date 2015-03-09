namespace AbcBank
{
    using System;
    using System.Linq;

    public class MaxiSavingBankAccount : BankAccount
    {
        public MaxiSavingBankAccount(DateTime time_, string ownerAccountId_, decimal b_, Tuple<InterestRateTerms, InterestRateTerms> irTerms_, AccountHistory history_)
            : base(AccountType.MaxiSaving, time_, ownerAccountId_, b_, irTerms_, history_)
        {
        }

        public override void CalculateInterestRates(DateTime time_)
        {
            var elapasedTime = GetInterestElapsedTime(time_);
            if (elapasedTime <= 0) return;

            var daysActivities = ActionsHistory.GetDaysOfUnpaidInterest(time_).ToList();
            var lastDayBalance = daysActivities.Last().Item2;
            var lastDay = daysActivities.Last().Item1;
            var priorDateOfWithdraw = daysActivities.Last().Item3;

            if (time_.Date > lastDay.Date)
            {
                daysActivities.Add(new Tuple<DateTime, decimal, DateTime, decimal>(time_.Date, lastDayBalance, priorDateOfWithdraw, 0.0M));
            }

            var totalUnpaidDays = daysActivities.Count;
            var curDay = daysActivities[0].Item1;
            var prevDay = curDay;
            var curBalance = daysActivities[0].Item2;
            var cumulativeBalanceAndInterests = curBalance;
            var netFundsAddedThisDay = 0.0M;
            int jUnpaidDay = 1;
            while (jUnpaidDay < totalUnpaidDays)
            {
                curDay = daysActivities[jUnpaidDay].Item1;

                var startingBalance = cumulativeBalanceAndInterests + netFundsAddedThisDay;

                var dayCountForThisPeriod = (int)(curDay - prevDay).TotalDays;

                DateTime prevDateOfWithdraw = daysActivities[jUnpaidDay].Item3;
                var withdrawMadeInLast10Days = Utils.DateDiffLessThan10Days(prevDateOfWithdraw, curDay);

                InterestRateTerms interestRateTerms = GetConditionalRates( withdrawMadeInLast10Days );

                cumulativeBalanceAndInterests = this.CalculatInterestRate(startingBalance, interestRateTerms, dayCountForThisPeriod);

                prevDay = curDay;

                netFundsAddedThisDay = daysActivities[jUnpaidDay].Item4;

                if (++jUnpaidDay >= totalUnpaidDays) break;
            }

            var totalBalancePlusInterests = cumulativeBalanceAndInterests;
            UpdateBalanceWithInterstPayment(time_, lastDayBalance, totalBalancePlusInterests);
        }
    }
}
