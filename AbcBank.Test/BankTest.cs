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
        public void Test_CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [Test]
        public void Test_CustomerSummary_ZeroAccounts()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");

            Assert.AreEqual("Customer Summary\n - No accounts found", bank.CustomerSummary());
        }

        [Test]
        public void Test_GetFirstCustomerName()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");            
            bank.AddCustomer(john);

            Assert.AreEqual("John", bank.GetFirstCustomerName());
        }

        [Test]
        public void Test_GetFirstCustomerName_Exception()
        {
            Bank bank = new Bank();
            Assert.AreEqual("Error", bank.GetFirstCustomerName());
        }

        [Test]
        public void Test_InterestPaid_CheckingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void Test_InterestPaid_SavingsAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new SavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void Test_InterestPaid_MaxSavings_Account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new MaxSavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void Test_InterestPaid_ZeroCustomers()
        {
            Bank bank = new Bank();            
            Assert.AreEqual(0.0, bank.TotalInterestPaid());
        }
    }
}
