using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbcBank;

namespace AbcBank.Test
{
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));

            Customer mark = new Customer("Mark");
            mark.openAccount(new Account(Account.SAVINGS));

            bank.addCustomer(john);
            bank.addCustomer(mark);

            string expected = "John (1 account),Mark (1 account)";
            string actual = bank.GetCustomerSummary();
            //actual = string.Join(",", bank.GetCustomerSummaryList()); 

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(3000.0);

            Assert.AreEqual(71.0, bank.totalInterestPaid(), DOUBLE_DELTA); //change to .01 %
        }

        [TestMethod]
        public void maxi_savings_account_with_withdraw_last10days()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(3000.0);
            checkingAccount.withdraw(3000.0);
            checkingAccount.deposit(3000.0); //withdraw in last 10 days, expect 5% interest in maxi_savings logic
            //3000 - 2000 = 1000 * .05 = 50 + 70 = 120

            Assert.AreEqual(120.0, bank.totalInterestPaid(), DOUBLE_DELTA); 
        }

    }
}
