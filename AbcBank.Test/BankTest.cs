using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.AbcAccount;
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
            CustomerAccount cusAccount=new CustomerAccount(john);
            cusAccount.openAccount(new AbcAccount.Account(AccountType.Checking));
            bank.AddCustomer(cusAccount);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.GetCustomerSummary());
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.Checking);
            CustomerAccount cusAccount = new CustomerAccount(new Customer("Bill")).openAccount(checkingAccount);
            bank.AddCustomer(cusAccount);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(AccountType.Savings);

            CustomerAccount cusAccount = new CustomerAccount(new Customer("Bill")).openAccount(savingsAccount);
            bank.AddCustomer(cusAccount);

            savingsAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(AccountType.MaxiSavings);
            CustomerAccount cusAccount = new CustomerAccount(new Customer("Bill")).openAccount(maxiSavingsAccount);
            bank.AddCustomer(cusAccount);

            maxiSavingsAccount.deposit(3000.0);

            Assert.AreEqual(71.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
