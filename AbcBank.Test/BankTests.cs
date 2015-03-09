using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTests
    {
        private Bank _bank;

        const string JohnSmithSocialSecurity = "111";
        private string _johnSmithCheckingAccount01;
        private string _johnSmithSavingAccount01;
        private string _johnSmithMaxiAccount01;
        private string _johnSmithSavingAccount02;

        const string IsabelleDavisSocialSecurity = "222";
        private string _isabelleDavisCheckingAccount01;

        [SetUp]
        public void TestInitialize()
        {
            this._bank = new Bank("AbcBank");
            this._bank.CreateNewCustomer("John", "Smith", JohnSmithSocialSecurity, "12 Park Ave., NYC, NY", "212-212-1001", "M", "Mr.");
            this._johnSmithCheckingAccount01 = this._bank.OpenNewAccount(JohnSmithSocialSecurity, AccountType.Checking, Convert.ToDateTime("01/2/2015"), 1000.00M, null);
            this._johnSmithSavingAccount01 = this._bank.OpenNewAccount(JohnSmithSocialSecurity, AccountType.Saving, Convert.ToDateTime("01/2/2015"), 2000.00M, null);
            this._johnSmithMaxiAccount01 = this._bank.OpenNewAccount(JohnSmithSocialSecurity, AccountType.MaxiSaving, Convert.ToDateTime("01/2/2015"), 10000.00M, null);
            this._johnSmithSavingAccount02 = this._bank.OpenNewAccount(JohnSmithSocialSecurity, AccountType.Saving, Convert.ToDateTime("01/2/2015"), 8000.00M, null);
            this._bank.CreateNewCustomer("Isabelle", "Davis", IsabelleDavisSocialSecurity, "3500 Edison Drive., Paramus, NJ", "201-711-1818", "F", "Mrs.");
            this._isabelleDavisCheckingAccount01 = this._bank.OpenNewAccount(IsabelleDavisSocialSecurity, AccountType.Checking, Convert.ToDateTime("01/14/2015"), 4000.00M, null);
        }

        [Test]
        public void TestCheckingDeposit()
        {
            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);

            checkingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 50.50M);

            Assert.AreEqual(checkingAccount.Balance, 1050.50M);
        }

        [Test]
        public void TestCheckingWithdraw()
        {
            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);

            checkingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 50.50M);

            Assert.AreEqual(checkingAccount.Balance, 1050.50M);

            checkingAccount.Withdraw(Convert.ToDateTime("01/4/2015"), 50.50M);

            Assert.AreEqual(checkingAccount.Balance, 1000.00M);
        }

        [Test]
        public void TestCheckingInterest()
        {
            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);
            checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/3/2015"));

            Assert.IsTrue(checkingAccount.Balance > 1000.00M && checkingAccount.Balance < 1000.003M);
        }

        [Test]
        public void TestSavingInterest()
        {
            var savingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithSavingAccount01);
            savingAccount.CalculateInterestRates(Convert.ToDateTime("01/3/2015"));
        }

        [Test]
        public void TestMaxiSavingInterest()
        {
            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);
            maxiSavingAccount.CalculateInterestRates(Convert.ToDateTime("01/3/2015"));

            Assert.IsTrue(maxiSavingAccount.Balance > 10002.00M && maxiSavingAccount.Balance < 10003.00M);
        }

        [Test]
        public void TestMaxiSavingInterestsWithoutWithdraws()
        {
            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);

            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Deposit(Convert.ToDateTime("01/4/2015"), 1000.00M);

            maxiSavingAccount.CalculateInterestRates(Convert.ToDateTime("01/10/2015"));

            Assert.IsTrue(maxiSavingAccount.Balance > 13024.00M && maxiSavingAccount.Balance < 13025.00M);
        }


        [Test]
        public void TestMaxiSavingInterestsWithWithdraws()
        {
            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);

            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Withdraw(Convert.ToDateTime("01/4/2015"), 2000.00M);

            maxiSavingAccount.CalculateInterestRates(Convert.ToDateTime("01/6/2015"));

            Assert.IsTrue(maxiSavingAccount.Balance > 10002.40M && maxiSavingAccount.Balance < 10003.00M);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPriorDateTimeTransaction()
        {
            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);
            checkingAccount.Deposit(Convert.ToDateTime("01/5/2015 1:05:28 PM"), 1000.00M);
            try
            {
                checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/5/2015 9:32:47 AM"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test]
        public void TestCheckingAccountTwoInterestPayments()
        {
            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);
            checkingAccount.Deposit(Convert.ToDateTime("01/5/2015"), 1000.00M);

            checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/5/2015"));

            Assert.IsTrue(checkingAccount.Balance > 1000.00M && checkingAccount.Balance < 1000.009M);

            checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/8/2015"));

            Assert.IsTrue(checkingAccount.Balance > 2000.014M && checkingAccount.Balance < 2000.03M);
        }

        [Test]
        public void TestInternalTransfer()
        {
            var customer = this._bank.GetCustomer(JohnSmithSocialSecurity);

            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);

            var savingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithSavingAccount01);

            customer.InternalTransfer(savingAccount, maxiSavingAccount, 500.00M);

            Assert.AreEqual(savingAccount.Balance, 1500.00M);
            Assert.AreEqual(maxiSavingAccount.Balance, 10500.00M);
        }

        [Test]
        public void TestGlobalCustomerAccountsReport()
        {
            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);

            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Withdraw(Convert.ToDateTime("01/4/2015"), 2000.00M);

            maxiSavingAccount.CalculateInterestRates(Convert.ToDateTime("01/6/2015"));

            var savingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithSavingAccount01);
            savingAccount.Deposit(Convert.ToDateTime("01/4/2015"), 800.00M);

            savingAccount.CalculateInterestRates(Convert.ToDateTime("01/6/2015"));

            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);
            checkingAccount.Deposit(Convert.ToDateTime("01/5/2015"), 400.00M);

            checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/11/2015"));

            var checkingAccountIsabelle = this._bank.GetAccount(IsabelleDavisSocialSecurity, this._isabelleDavisCheckingAccount01);
            checkingAccountIsabelle.Deposit(Convert.ToDateTime("02/5/2015"), 1400.00M);
            checkingAccountIsabelle.CalculateInterestRates(Convert.ToDateTime("02/15/2015"));

            var globalStatementReport = this._bank.GetGlobalAccountsReport();
        }

        [Test]
        public void TestTotalInterestRatePaidReport()
        {
            var totalInterestRatePaid = this._bank.GetTotalInterestRatePaidReport();

            Assert.IsTrue(totalInterestRatePaid < 1e-10);

            var maxiSavingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithMaxiAccount01);

            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Deposit(Convert.ToDateTime("01/3/2015"), 1000.00M);
            maxiSavingAccount.Withdraw(Convert.ToDateTime("01/4/2015"), 2000.00M);

            maxiSavingAccount.CalculateInterestRates(Convert.ToDateTime("01/6/2015"));

            totalInterestRatePaid = this._bank.GetTotalInterestRatePaidReport();
            Assert.IsTrue(totalInterestRatePaid > 1002.4 && totalInterestRatePaid < 1002.5);

            var savingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithSavingAccount01);
            savingAccount.Deposit(Convert.ToDateTime("01/4/2015"), 800.00M);

            savingAccount.CalculateInterestRates(Convert.ToDateTime("01/6/2015"));

            totalInterestRatePaid = this._bank.GetTotalInterestRatePaidReport();
            Assert.IsTrue(totalInterestRatePaid > 1002.5 && totalInterestRatePaid < 1002.52);

            var checkingAccount = this._bank.GetAccount(JohnSmithSocialSecurity, this._johnSmithCheckingAccount01);
            checkingAccount.Deposit(Convert.ToDateTime("01/5/2015"), 400.00M);

            checkingAccount.CalculateInterestRates(Convert.ToDateTime("01/11/2015"));

            totalInterestRatePaid = this._bank.GetTotalInterestRatePaidReport();

            Assert.IsTrue(totalInterestRatePaid > 1002.54 && totalInterestRatePaid < 1002.56);
        }

        [Test]
        public void TestCustomerStatementReport()
        {
            var customerStatementReport = this._bank.GetCustomerAccountsReport(JohnSmithSocialSecurity);

            //Assert.IsTrue(customerStatementReport.Count == 4);
        }
    }
}
