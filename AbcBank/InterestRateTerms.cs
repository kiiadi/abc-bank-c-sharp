using System;
using System.Collections.Generic;

namespace AbcBank
{
    /// <summary>
    /// Term Structured Interest Rate
    /// </summary>
    /// 
    public class InterestRateTerms
    {
        private readonly List<RateBucket> _buckets;

        private const double DaysInYear = 365.25;

        public InterestRateTerms(params object[] args_)
        {
            _buckets = new List<RateBucket>();
            var len = args_.Length;
            var i = 0;
            object min = 0.0M;

            while (i < len)
            {
                var max = args_[i++];
                if (i==len) throw new ArgumentNullException();
                var rate = (double)args_[i++];

                var backet = new RateBucket { Min = Convert.ToDecimal(min), Max = Convert.ToDecimal(max), Rate = rate };
                min = max;

                _buckets.Add(backet);
            }
        }

        public virtual decimal CalcInterestRate(decimal balance_, int days_)
        {
            double interestPlusBalance = 0.0;
            for (int i = 0; i <= _buckets.Count; i++)
            {
                decimal curSize = 0.0M;
                if ( balance_ <= _buckets[i].Max )
                {
                    curSize = balance_ - _buckets[i].Min;
                    interestPlusBalance += (double)curSize * Math.Pow((1 + _buckets[i].Rate / DaysInYear), days_);
                    break;
                }

                curSize = _buckets[i].Max - _buckets[i].Min;
                var interestToPay = (double)curSize * Math.Pow((1 + _buckets[i].Rate / DaysInYear), days_);
                interestPlusBalance += interestToPay;
            }
            return (decimal)interestPlusBalance;
        }

        public virtual decimal CalcAccruedDailyRate(decimal balance_)
        {
            double interest = 0.0;
            for (int i = 0; i <= _buckets.Count; i++)
            {
                decimal curSize = 0.0M;
                if (balance_ <= _buckets[0].Max)
                {
                    curSize = balance_ - _buckets[0].Min;
                    interest += (double)curSize * (1 + _buckets[i].Rate / DaysInYear);
                    break;
                }

                curSize = _buckets[0].Max - _buckets[0].Min;
                interest += (double)curSize * (1 + _buckets[i].Rate / DaysInYear);
            }
            return (decimal)interest;
        }
    }
}
