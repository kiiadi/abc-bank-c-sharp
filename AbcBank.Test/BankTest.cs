using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.Implementation;
using AbcBank.AccountsInterface;
using AbcBank.CustomerInterface;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void customerSummary()
        {
            MainBank bank = new MainBank();
            ICustomerInterface Henry = new Customers("Henry");
            IAccountsInterface HenrySavings = new SavingsAccount();
            bank.AddCustomer(Henry);
            Henry.AddAccount(HenrySavings);

            Assert.AreEqual("Customer Summary\r\n - Customer Henry maintains 1 account\r\n", bank.CustomerSummary());
        }

        [Test]
        public void checkingAccount()
        {
            MainBank bank = new MainBank();
            ICustomerInterface James = new Customers("James");
            IAccountsInterface JamesChecking = new CheckingAccount();
            bank.AddCustomer(James);
            James.AddAccount(JamesChecking);
            James.Deposit(JamesChecking, 100.00);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            MainBank bank = new MainBank();
            ICustomerInterface Jerry = new Customers("Jerry");
            IAccountsInterface JerrySavings = new SavingsAccount();
            bank.AddCustomer(Jerry);
            Jerry.AddAccount(JerrySavings);
            Jerry.Deposit(JerrySavings, 1500.00);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            MainBank bank = new MainBank();
            ICustomerInterface Paul = new Customers("Paul");
            IAccountsInterface PaulMaxiSavings = new MaxiSavingsAccount();
            bank.AddCustomer(Paul);
            Paul.AddAccount(PaulMaxiSavings);
            Paul.Deposit(PaulMaxiSavings, 3000.00);

            Assert.AreEqual(0.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
