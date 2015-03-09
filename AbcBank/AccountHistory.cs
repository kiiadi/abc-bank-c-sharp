using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    using System.Collections;
    using System.Data;
    using System.Text;

    public class AccountHistory
    {
        private readonly object _locker = new object();

        private readonly DataTable _transactionDataTable;
        private  long _actionId;

        public decimal Balance { get; private set; }
        public DateTime LastDateChanged { get; private set; }

        public AccountHistory()
        {
            this._transactionDataTable = new DataTable
            {
                Columns =
                                 {
                                     { "Id", typeof(long) },
                                     { "Date", typeof(string) },
                                     { "DayAndTime", typeof(DateTime) },
                                     { "ActionCode", typeof(int) },
                                     { "Action", typeof(string) },
                                     { "Amount", typeof(decimal) },
                                     { "Balance", typeof(decimal) }
                                 }
            };
            this._actionId = 1;
        }

        public void Add(DateTime time_, ActionType actionType_, decimal amount_, decimal newBalance_ = 0.0M)
        {
            lock (_locker)
            {
                if (time_ < LastDateChanged) throw new ArgumentException(string.Format("Can't enter a transaction for a date prior to : {0}", LastDateChanged.ToString("yyyy-MM-dd")));
                LastDateChanged = time_;

                var date = time_.ToString("yyyy-MM-dd");

                var verifiedBalance = this.VerifyBalance(time_, actionType_, amount_, newBalance_);
                this.Balance = verifiedBalance;

                this._transactionDataTable.Rows.Add(this._actionId++, date, time_, actionType_,actionType_.ToString(), amount_, verifiedBalance);
            }
        }

        public DateTime LastDayOfWidthdraw()
        {
            lock (_locker)
            {
                var lastDateOfWidthdraw = (from x in this._transactionDataTable.AsEnumerable()
                                           where x.Field<int>("ActionCode") == (int)ActionType.Widthdraw
                                           orderby x.Field<DateTime>(("DayAndTime"))
                                           select x.Field<DateTime>("DayAndTime")).ToList()
                    .OrderByDescending(y_ => y_)
                    .FirstOrDefault();

                return lastDateOfWidthdraw;
            }
        }

        public DateTime LastDayOfWidthdrawSince(DateTime time_)
        {
            lock (_locker)
            {
                var lastDateOfWidthdraw = (from x in this._transactionDataTable.AsEnumerable()
                                           let actionDate = x.Field<DateTime>("DayAndTime").Date
                                           where x.Field<int>("ActionCode") == (int)ActionType.Widthdraw && actionDate <= time_.Date && NetDepositVsWithdrawIsNegativeAsOf(actionDate)
                                           orderby x.Field<DateTime>(("DayAndTime"))
                                           select x.Field<DateTime>("DayAndTime")).ToList()
                    .OrderByDescending(y_ => y_)
                    .FirstOrDefault();

                return lastDateOfWidthdraw;
            }
        }

        public decimal InterestPaidOn(DateTime time_)
        {
            lock (_locker)
            {
                var interestPaidToday = (from x in this._transactionDataTable.AsEnumerable()
                                           where x.Field<int>("ActionCode") == (int)ActionType.Interest && x.Field<DateTime>("DayAndTime").Date == time_.Date
                                           orderby x.Field<DateTime>(("DayAndTime")) descending 
                                           select x.Field<decimal>("Amount")).ToList()
                    .FirstOrDefault();

                return interestPaidToday;
            }
        }

        public decimal EodBalanceAsOf(DateTime time_)
        {
            lock (_locker)
            {
                if (this._transactionDataTable.Rows.Count == 0) return 0.00M;

                var availableBalanceAsOf = (from a in this._transactionDataTable.AsEnumerable()
                                            let aDate = a.Field<DateTime>("DayAndTime").Date
                                            where aDate <= time_.Date
                                            group a by new { date = new DateTime(aDate.Year, aDate.Month, aDate.Day), id = a.Field<long>("Id"), balance = a.Field<decimal>("Balance") } into g
                                            select new { date = g.Key.date, id = g.Max(x_ => x_.Field<long>("Id")), balance = g.Max(y_ => y_.Field<decimal>("Balance")) }
                ).OrderByDescending(z_ => z_.date).FirstOrDefault();

                if (availableBalanceAsOf != null)
                {
                    return availableBalanceAsOf.balance;
                }

                return 0.00M;
            }
        }

        public decimal NetDepositVsWithdrawAsOf(DateTime asOfDate_)
        {
            lock (_locker)
            {
                DateTime asOfDate = asOfDate_.Date;

                var todayTotalWithdraws = (from x in this._transactionDataTable.AsEnumerable()
                                           let actionDate = x.Field<DateTime>("DayAndTime").Date
                                           where actionDate == asOfDate && x.Field<int>("ActionCode") == (int)ActionType.Widthdraw
                                           group x by
                                               new
                                                   {
                                                       date =
                                               new DateTime(actionDate.Year, actionDate.Month, actionDate.Day)
                                                   }
                                           into g
                                           select
                                               new
                                                   {
                                                       toDay = g.Key.date,
                                                       totalToday =
                                               g.Sum(
                                                   y_ =>
                                                   y_.Field<decimal>("Amount"))
                                                   }
                                          ).Select(z_ => z_.totalToday).FirstOrDefault();

                var todayTotalDeposits = (from x in this._transactionDataTable.AsEnumerable()
                                           let actionDate = x.Field<DateTime>("DayAndTime").Date
                                          where actionDate == asOfDate && x.Field<int>("ActionCode") == (int)ActionType.Deposit
                                           group x by
                                               new
                                               {
                                                   date =
                                           new DateTime(actionDate.Year, actionDate.Month, actionDate.Day)
                                               }
                                               into g
                                               select
                                                   new
                                                   {
                                                       toDay = g.Key.date,
                                                       totalToday =
                                               g.Sum(
                                                   y_ =>
                                                   y_.Field<decimal>("Amount"))
                                                   }
                                       ).Select(z_ => z_.totalToday).FirstOrDefault();

                return todayTotalDeposits - todayTotalWithdraws;
            }
        }

        public decimal NetFundAddedAsOf(DateTime asOfDate_)
        {
            lock (_locker)
            {
                DateTime asOfDate = asOfDate_.Date;

                var todayTotalDebit = (from x in this._transactionDataTable.AsEnumerable()
                                           let actionDate = x.Field<DateTime>("DayAndTime").Date
                                           let actionCode = x.Field<int>("ActionCode")
                                           where actionDate == asOfDate && (actionCode == (int)ActionType.Widthdraw || actionCode == (int)ActionType.FeeCharge)
                                           group x by
                                               new
                                               {
                                                   date =
                                           new DateTime(actionDate.Year, actionDate.Month, actionDate.Day)
                                               }
                                               into g
                                               select
                                                   new
                                                   {
                                                       toDay = g.Key.date,
                                                       totalToday =
                                               g.Sum(
                                                   y_ =>
                                                   y_.Field<decimal>("Amount"))
                                                   }
                                          ).Select(z_ => z_.totalToday).FirstOrDefault();

                var todayTotalCreditExcludingInterestRates = (from x in this._transactionDataTable.AsEnumerable()
                                          let actionDate = x.Field<DateTime>("DayAndTime").Date
                                          where actionDate == asOfDate && x.Field<int>("ActionCode") == (int)ActionType.Deposit
                                          group x by
                                              new
                                              {
                                                  date =
                                          new DateTime(actionDate.Year, actionDate.Month, actionDate.Day)
                                              }
                                              into g
                                              select
                                                  new
                                                  {
                                                      toDay = g.Key.date,
                                                      totalToday =
                                              g.Sum(
                                                  y_ =>
                                                  y_.Field<decimal>("Amount"))
                                                  }
                                       ).Select(z_ => z_.totalToday).FirstOrDefault();

                return todayTotalCreditExcludingInterestRates - todayTotalDebit;
            }
        }

        public bool NetDepositVsWithdrawIsPositiveAsOf(DateTime asOfDate_)
        {
            return this.NetDepositVsWithdrawAsOf(asOfDate_) >= 0;
        }

        public bool NetDepositVsWithdrawIsNegativeAsOf(DateTime asOfDate_)
        {
            return this.NetDepositVsWithdrawAsOf(asOfDate_) < 0;
        }

        public DateTime LastDayOfInterstPaid()
        {
            lock (_locker)
            {
                var firstDt = this._transactionDataTable.AsEnumerable().Min(r_ => r_.Field<DateTime>("DayAndTime"));

                var lastDateOfIrPayment = (from x in this._transactionDataTable.AsEnumerable()
                                           where x.Field<int>("ActionCode") == (int)ActionType.Interest
                                           orderby x.Field<DateTime>(("DayAndTime"))
                                           select x.Field<DateTime>("DayAndTime")).ToList()
                    .OrderByDescending(y_ => y_)
                    .FirstOrDefault();

                return lastDateOfIrPayment > firstDt ? lastDateOfIrPayment : firstDt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upUntil_"></param>
        /// <returns> Tuple<Today, Balance, DateOfPriorWithdraw, NetFundsAddedToAccount/> </returns>
        public IEnumerable<Tuple<DateTime, decimal, DateTime, decimal>> GetDaysOfUnpaidInterest(DateTime upUntil_)
        {
            var lastInterestPaidDate = LastDayOfInterstPaid();
            lock (_locker)
            {
                var daysNotPaid = (from x in this._transactionDataTable.AsEnumerable()
                                   let actionDate = x.Field<DateTime>("DayAndTime").Date
                                   where actionDate >= lastInterestPaidDate.Date && actionDate <= upUntil_.Date
                                   orderby(actionDate)
                                   select new { Date = actionDate }).Distinct().Select(d_ => new Tuple<DateTime, decimal, DateTime, decimal>(d_.Date, this.EodBalanceAsOf(d_.Date), this.LastDayOfWidthdrawSince(d_.Date), this.NetFundAddedAsOf(d_.Date)));

                return daysNotPaid;
            }
        }

        private decimal VerifyBalance(DateTime time_, ActionType type_, decimal amount_, decimal balance_)
        {
            lock (_locker)
            {
                // check if balance is consistent
                var signedAmount = (type_ == ActionType.Widthdraw || type_ == ActionType.FeeCharge) ? -amount_ : amount_;

                var balanceAsOf = EodBalanceAsOf(time_);

                var currentBalance = balanceAsOf + signedAmount;

                if (currentBalance == balance_) return currentBalance;

                currentBalance = balanceAsOf + signedAmount;
                
                return currentBalance;
            }
        }

        public decimal BalanceFrom()
        {
            lock (_locker)
            {
                var totalDebit =
                    this._transactionDataTable.AsEnumerable()
                        .Where(
                            r_ =>
                            r_.Field<int>("ActionCode") == (int)ActionType.Widthdraw
                            || r_.Field<int>("ActionCode") == (int)ActionType.FeeCharge)
                        .Sum(r_ => (double)r_.Field<decimal>("Amount") * -1.0);

                var totalCredit =
                    this._transactionDataTable.AsEnumerable()
                        .Where(
                            r_ =>
                            r_.Field<int>("ActionCode") != (int)ActionType.Widthdraw
                            && r_.Field<int>("ActionCode") != (int)ActionType.FeeCharge)
                        .Sum(r_ => (double)r_.Field<decimal>("Amount"));

                return (decimal)(totalCredit + totalDebit);
            }
        }

        public double GetInterestPaid()
        {
            lock (_locker)
            {
                var totalInterestRatePaid =
                    this._transactionDataTable.AsEnumerable()
                        .Where(r_ => r_ != null && r_.Field<int>("ActionCode") == (int)ActionType.Interest)
                        .Sum(r_ => (double)r_.Field<decimal>("Amount"));

                return totalInterestRatePaid;
            }
        }

        public string TransactionsSummary()
        {
            lock (_locker)
            {
                var sb = new StringBuilder();
                foreach (var row in Enumerable.Select(this._transactionDataTable.AsEnumerable(), t_ => string.Format("{0}, date:{1}, action:{2}, amount:{3}, balance:{4}", t_.Field<long>("Id"), t_.Field<DateTime>("DayAndTime"), t_.Field<string>("Action"), Utils.Dollars(t_.Field<decimal>("Amount")), Utils.Dollars(t_.Field<decimal>("Balance")))))
                {
                    sb.Append(row).Append(Environment.NewLine);
                }

                return sb.ToString();
            }
        }
    }
}
