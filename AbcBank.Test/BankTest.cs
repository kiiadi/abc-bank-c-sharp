using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.Interfaces;
using AbcBank.Enums;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        Bank bank;
        Customer customer;
        Account account;
        Account savingsAccount;
        Account checkingAccount;
        Account maxiSavingAccount;

        [SetUp]
        public void init()
        {
            bank = new Bank();
            customer = new Customer("Sung");
            checkingAccount = new CheckingAccount();
            savingsAccount = new SavingsAccount();
            maxiSavingAccount = new MaxiSavingsAccount();
        }

        [Test]
        public void TestCustomerSummary()
        {            
            account = new CheckingAccount();
            customer.OpenAccount(account);
            bank.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - Sung (1 account)", bank.CustomerSummary());
        }

        [Test]
        public void TestTotalInterestPaid()
        {
            bank.AddCustomer(customer);
            customer.OpenAccount(checkingAccount);
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(maxiSavingAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(1500.0);
            maxiSavingAccount.Deposit(3000);

            Assert.AreEqual(172.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        //[Test]
        //public void TestInterestRateAccruedDailyForCheckingAccount()
        //{
        //    bank.AddCustomer(customer);
        //    account = new Account(AccountType.Checking);
        //    customer.OpenAccount(account);
        //    account.Deposit(3000, DateTime.Parse("1/1/2014"));
        //    account.Deposit(5000, DateTime.Parse("2/5/2014"));

        //    Assert.AreEqual(170.0, bank.TotalInterestPaid(DateTime.Parse("3/10/2014")), DOUBLE_DELTA);
        //}


        //[Test]
        //public void TestInterestRateAccruedDailyForSavingsAccount()
        //{
        //    bank.AddCustomer(customer);
        //    account = new Account(AccountType.Savings);
        //    customer.OpenAccount(account);
        //    account.Deposit(3000, DateTime.Parse("1/1/2014"));
        //    account.Deposit(5000, DateTime.Parse("2/5/2014"));

        //    Assert.AreEqual(170.0, bank.TotalInterestPaid(DateTime.Parse("3/10/2014")), DOUBLE_DELTA);
        //}

        //[Test]
        //public void TestInterestRateAccruedDailyForMaxiSavingsAccount()
        //{
        //    bank.AddCustomer(customer);
        //    account = new Account(AccountType.MaxiSavings);
        //    customer.OpenAccount(account);
        //    account.Deposit(3000, DateTime.Parse("1/1/2014"));
        //    account.Deposit(5000, DateTime.Parse("2/5/2014"));

        //    Assert.AreEqual(170.0, bank.TotalInterestPaid(DateTime.Parse("3/10/2014")), DOUBLE_DELTA);
        //}

        //Checking accounts have a flat rate of 0.1%
        //Savings accounts have a rate of 0.1% for the first $1,000 then 0.2%
        //Maxi-Savings accounts have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10%
        //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
        //Interest rates should accrue daily (incl. weekends), rates above are per-annum

    }
}
