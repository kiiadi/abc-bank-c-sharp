using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
            john.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(john);
            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);
            checkingAccount.deposit(100.0);
            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
        [Test]
        public void checkingAccountAccrued()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);
            checkingAccount.deposit(1000.0);
            Assert.AreEqual(Math.Round(0.08330, 3), Math.Round(bank.totalAccruedInterestPaid(),3));
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));
            checkingAccount.deposit(3000.0);
            Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
       
    }
}
