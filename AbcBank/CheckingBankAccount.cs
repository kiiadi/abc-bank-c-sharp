namespace AbcBank
{
    using System;

    public class CheckingBankAccount : BankAccount
    {
        public CheckingBankAccount(DateTime time_, string ownerAccountId_, decimal b_, Tuple<InterestRateTerms, InterestRateTerms> irTerms_, AccountHistory history_)
            : base(AccountType.Checking, time_, ownerAccountId_, b_, irTerms_, history_)
        {
        }

        public override void CalculateInterestRates(DateTime time_)
        {
            base.AddAccruedInterests(time_);
        }
    }
}
