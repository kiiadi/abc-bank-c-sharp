using System.Linq;

namespace AbcBank
{
    using System;

    public enum ActionType
    {
        Deposit = 101,
        Interest,
        Widthdraw,
        FeeCharge
    }

    public abstract class BankAccount
    {
        public string OwnerAccountId { get; private set; }

        protected DateTime Inception;

        protected Tuple<InterestRateTerms, InterestRateTerms> PrimarySecondaryInterestRateTerms;
        protected InterestRateTerms PrimaryInterestRateTerm;

        protected decimal AccountBalance;

        public AccountType AccountType { get; private set; }

        public AccountHistory ActionsHistory { get; set; }

        public BankAccount(AccountType type_, DateTime time_, string ownerAccountId_, decimal amount_, Tuple<InterestRateTerms, InterestRateTerms> irTerms_, AccountHistory history_)
        {
            this.AccountType = type_;
            this.ActionsHistory = history_;
            this.PrimarySecondaryInterestRateTerms = irTerms_;
            this.PrimaryInterestRateTerm = irTerms_.Item1;
            this.OwnerAccountId = ownerAccountId_;
            this.AccountBalance = amount_;
            this.Inception = time_;

            if (history_ == null) ActionsHistory = new AccountHistory();

            this.AccountBalance = ActionsHistory.BalanceFrom() + amount_;

            ActionsHistory.Add(time_, ActionType.Deposit, amount_, this.AccountBalance);

            if (this.AccountBalance < 0)
                throw new ArgumentException(string.Format("Account has negative AccountBalance {0}", this.AccountBalance));
        }

        public virtual decimal Balance
        {
            get
            {
                return this.AccountBalance;
            }
        }

        public virtual void Withdraw(DateTime time_, decimal amount_)
        {
            if (amount_ < 0) throw new ArgumentException("amount must be greater or equal zero");
            if (amount_ > this.AccountBalance) throw new ArgumentException("trying to withdraw too much money");
            this.AccountBalance -= amount_;

            ActionsHistory.Add(time_, ActionType.Widthdraw, amount_, this.AccountBalance);
        }

        public virtual void Deposit(DateTime time_, decimal amount_)
        {
            this.AccountBalance += amount_;
            ActionsHistory.Add(time_, ActionType.Deposit, amount_, this.AccountBalance);
        }

        protected void UpdateBalanceWithInterstPayment(DateTime time_, decimal balance_, decimal balancePlusInterest_)
        {
            this.AccountBalance = balancePlusInterest_;
            var interest = balancePlusInterest_ - balance_;

            ActionsHistory.Add(time_, ActionType.Interest, interest, this.AccountBalance);
        }

        protected decimal CalculatInterestRate(decimal balance_, InterestRateTerms irTerm_, int days_)
        {
            var interestPlusBalance = irTerm_.CalcInterestRate(balance_, days_);
            return interestPlusBalance;
        }

        protected int GetInterestElapsedTime(DateTime time_)
        {
            var lastInterstRatePaid = this.ActionsHistory.LastDayOfInterstPaid();
            var elapasedTime = (int)(time_ - lastInterstRatePaid).TotalDays;

            return elapasedTime;
        }

        protected virtual void AddInterests(DateTime time_)
        {
            AddInterests(time_, this.AccountBalance, this.PrimaryInterestRateTerm);
        }

        protected void AddInterests(DateTime time_, decimal balance_, InterestRateTerms interestRateTerms_)
        {
            var elapasedTime = GetInterestElapsedTime(time_);

            if (elapasedTime <= 0) return;

            var balancePlusInterest = this.CalculatInterestRate(this.AccountBalance, interestRateTerms_, elapasedTime);
            UpdateBalanceWithInterstPayment(time_, this.AccountBalance, balancePlusInterest);
        }

        public abstract void CalculateInterestRates(DateTime time_);

        protected virtual void AddAccruedInterests(DateTime time_)
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

                var startingDayBalance = cumulativeBalanceAndInterests + netFundsAddedThisDay;

                var dayCountForThisPeriod = (int)(curDay - prevDay).TotalDays;

                InterestRateTerms interestRateTerms = this.GetPrimaryRates();

                cumulativeBalanceAndInterests = this.CalculatInterestRate(startingDayBalance, interestRateTerms, dayCountForThisPeriod);

                prevDay = curDay;

                netFundsAddedThisDay = daysActivities[jUnpaidDay].Item4;

                if (++jUnpaidDay >= totalUnpaidDays) break;
            }

            var totalBalancePlusInterests = cumulativeBalanceAndInterests;
            UpdateBalanceWithInterstPayment(time_, lastDayBalance, totalBalancePlusInterests);
        }

        protected InterestRateTerms GetPrimaryRates()
        {
            return  this.PrimarySecondaryInterestRateTerms.Item1;
        }

        protected InterestRateTerms GetPenaltyRates()
        {
            return this.PrimarySecondaryInterestRateTerms.Item2;
        }

        protected InterestRateTerms GetConditionalRates(bool withdrawCondition_)
        {
            return withdrawCondition_
                       ? this.PrimarySecondaryInterestRateTerms.Item2
                       : this.PrimarySecondaryInterestRateTerms.Item1;
        }

        public override string ToString()
        {
            return OwnerAccountId + "'s Account Balance is : " + this.AccountBalance;
        }

        public decimal InterestEarned()
        {
            return (decimal) this.ActionsHistory.GetInterestPaid();
        }
    }
}
