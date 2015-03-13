namespace AbcBank
{
    using System;

    public class SavingBankAccount : BankAccount
    {
        public SavingBankAccount(DateTime time_, string ownerAccountId_, decimal b_, Tuple<InterestRateTerms, InterestRateTerms> irTerms_, AccountHistory history_)
            : base(AccountType.Saving, time_, ownerAccountId_, b_, irTerms_, history_)
        {
        }

        public override void CalculateInterestRates(DateTime time_)
        {
            base.AddAccruedInterests(time_);
        }
    }
}
