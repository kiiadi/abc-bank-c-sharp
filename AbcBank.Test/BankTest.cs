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

        iBank bank;
        iCustomer customer;
        iAccount account;

        [SetUp]
        public void init()
        {
            bank = new Bank();
            customer = new Customer("Sung");
        }

        [Test]
        public void TestCustomerSummary()
        {            
            account = new Account(AccountType.Checking);
            customer.OpenAccount(account);
            bank.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - Sung (1 account)", bank.CustomerSummary());
        }

        [Test]
        public void TestCheckingInterestPaid()
        {
            bank.AddCustomer(customer);
            account = new Account(AccountType.Checking);
            customer.OpenAccount(account);
            account.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void TestSavingsInterestPaid()
        {
            bank.AddCustomer(customer);
            account = new Account(AccountType.Savings);
            customer.OpenAccount(account);
            account.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void TestMaxiSavingsInterestPaid()
        {
            bank.AddCustomer(customer);
            account = new Account(AccountType.MaxiSavings);
            customer.OpenAccount(account);
            account.Deposit(3000);

            Assert.AreEqual(170.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
        //Interest rates should accrue daily (incl. weekends), rates above are per-annum

    }
}
