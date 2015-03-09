using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class InterestRateContainer
    {
        private static readonly object Locker = new object();
        private static volatile InterestRateContainer interestRateContainer = null;

        private static readonly IDictionary<AccountType, Tuple<InterestRateTerms, InterestRateTerms>>
            AccountInterestMap = new Dictionary<AccountType, Tuple<InterestRateTerms, InterestRateTerms>>(); 
        private InterestRateContainer()
        {
        }

        public static InterestRateContainer GetInstance()
        {
            if (interestRateContainer == null)
            {
                lock (Locker)
                {
                     if (interestRateContainer == null) interestRateContainer = new InterestRateContainer();
                }
            }
            return interestRateContainer;
        }

        public void Register(
            AccountType type_,
            InterestRateTerms primaryInterestRateTerms_,
            InterestRateTerms secondaryInterestRateTerms_=null)
        {
            var interestRatePair = new Tuple<InterestRateTerms, InterestRateTerms>(
                primaryInterestRateTerms_,
                secondaryInterestRateTerms_);

            if (!AccountInterestMap.ContainsKey(type_))
                AccountInterestMap.Add(type_, interestRatePair);
        }

        public Tuple<InterestRateTerms, InterestRateTerms> Get(AccountType type_)
        {
            if (!AccountInterestMap.ContainsKey(type_))
                throw new ArgumentException("Account type not found");

            return AccountInterestMap[type_];
        }
    }
}
