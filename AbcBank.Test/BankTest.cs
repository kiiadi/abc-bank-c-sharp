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
        public void customerSummary_Test()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(AccountFactory.CreateAccount(AccountType.Savings));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }

        [Test]
        public void checkingAccount_totalInterestPaid_Test()
        {
            Bank bank = new Bank();
            Account checkingAccount = (AccountFactory.CreateAccount(AccountType.Checking));
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(1.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savingsAccount_TotalInterestPaid_Test()
        {
            Bank bank = new Bank();
            Account savingsAccount = (AccountFactory.CreateAccount(AccountType.Savings));
            bank.addCustomer(new Customer("Bill").openAccount(savingsAccount));

            savingsAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxiSavingsAccount_TotalInterestPaid_Test()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = (AccountFactory.CreateAccount(AccountType.Maxi_Savings));
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));

            maxiSavingsAccount.deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
        [Test]
        public void getFirstcustomer_Test()
        {
            Bank bank = new Bank();
            bank.addCustomer(new Customer("Bill"));
            Assert.AreEqual("Bill", bank.getFirstCustomer());
        }
    }
}
