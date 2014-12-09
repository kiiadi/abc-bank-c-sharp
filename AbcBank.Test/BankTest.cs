using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using AbcBank.AccountManager;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");

            john.openAccount(AccountType.Checking);
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }


        [Test]
        public void interestPaidCheckingAccount()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            var account = bill.openAccount(AccountType.Checking);
            bank.addCustomer(bill);

            account.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void interestPaidSavingsAccount()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            var account = bill.openAccount(AccountType.Savings);
            bank.addCustomer(bill);

            account.deposit(1500.0);


            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }


        [Test]
        public void interestPaid_WhenMaxiSavingsHasNoWithDrawals()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            var account = bill.openAccount(AccountType.MaxiSavings);
            bank.addCustomer(bill); ;

            account.deposit(3000.0);

            Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void interestPaid_WhenMaxiSavingsHasRecentWithDrawals()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            var account = bill.openAccount(AccountType.MaxiSavings);
            bank.addCustomer(bill); ;
            account.setAccountBalance(4000);
            account.withdraw(3000.0);

            Assert.AreEqual(10.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
