using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class When_interacting_with_an_account
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void it_should_create_an_account_with_the_right_account_type()
        {
            var expected = (AccountType)new Random().Next(0,2);
            var account = new Account(expected, "test");
            Assert.AreEqual(expected, account.getAccountType());
            Assert.AreEqual("test", account.getAccountNumber());
        }

        [Test]
        public void it_should_deposit()
        {
            var account = new Account(AccountType.Checking,"");
            double expected = new Random().Next();

            account.deposit(expected);
            Assert.AreEqual(expected, account.sumTransactions());
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void it_should_throw_argument_exception_for_negative_deposits()
        {
            new Account(AccountType.Checking, "").deposit(-10.0);
        }
        
        [Test]
        public void it_should_withdraw()
        {
            var account = new Account(AccountType.Checking, "");
            double expected = new Random().Next(0,10000000);
            account.withdraw(expected);

            Assert.AreEqual(-expected, account.sumTransactions());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void it_should_throw_argument_exception_for_negative_withdrawals()
        {
            new Account(AccountType.Checking, "").withdraw(-20.0);
        }

        [Test]
        public void it_should_sum_transactions()
        {
            var account = new Account(AccountType.Checking, "");
            double deposit = new Random().Next(0, 1000000);
            double withdrawal = new Random((int) deposit).Next(0, 1000000);
            account.deposit(deposit);
            account.withdraw(withdrawal);

            Assert.AreEqual(deposit-withdrawal, account.sumTransactions());
        }
        
        [Test]
        public void it_should_calc_interest_checking_account()
        {
            var account = new Account(AccountType.Checking, "");
            double deposit = new Random().Next(0, 1000000);
            account.deposit(deposit);
            double expected = deposit*.001;
            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void it_should_calc_interest_savings_account()
        {
            var account = new TestableAccount(AccountType.Savings, "");
            var deposit = new Random().Next(0, 1000);
            var expected = deposit*0.001;
            account.SumFunc = () => deposit;

            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);

            deposit = new Random().Next(1001, 100000000);
            expected = (1 + (deposit - 1000)* 0.002);
            account.SumFunc = () => deposit;

            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void it_should_calc_interest_maxisavings_account()
        {
            var account = new TestableAccount(AccountType.MaxiSavings, "");
            var deposit = new Random().Next(0, 1000);
            var expected = deposit * 0.02;
            account.SumFunc = () => deposit;

            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);

            deposit = new Random().Next(1001, 2000);
            expected = (20 + (deposit - 1000) * 0.05);
            account.SumFunc = () => deposit;

            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);

            deposit = new Random().Next(2001,100000000);
            expected = (70 + (deposit - 2000) * 0.1);
            account.SumFunc = () => deposit;

            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);
        }


    }

    public class TestableAccount:Account
    {
        public Func<double> SumFunc { get; set; }

        public TestableAccount(AccountType accountType, string accountNumber)
            : base(accountType, accountNumber)
        {
            SumFunc = base.sumTransactions;
        }

        public override double sumTransactions()
        {
            return SumFunc();
        }
    }
}

