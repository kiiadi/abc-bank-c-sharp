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
    public class CustomerTest
    {

        iBank bank;
        iCustomer customer;
        iAccount savingsAccount;
        iAccount checkingAccount;
        iAccount maxiSavingAccount;

        [SetUp]
        public void init()
        {
            bank = new Bank();
            customer = new Customer("Sung");
            checkingAccount = new Account(AccountType.Checking);
            savingsAccount = new Account(AccountType.Savings);
            maxiSavingAccount = new Account(AccountType.MaxiSavings);
        }

        [Test] //Test customer statement generation
        public void TestCustomerStatementGeneration()
        {
            customer.OpenAccount(checkingAccount);
            customer.OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Sung\r\n" +
                    "\r\n" +
                    "Checking Account\r\n" +
                    "  deposit $100.00\r\n" +
                    "Total $100.00\r\n" +
                    "\r\n" +
                    "Savings Account\r\n" +
                    "  deposit $4,000.00\r\n" +
                    "  withdrawal $200.00\r\n" +
                    "Total $3,800.00\r\n" +
                    "\r\n" +
                    "Total In All Accounts $3,900.00", customer.GetStatement());
        }

        [Test]
        public void TestCreationOfOneAccount()
        {
            customer.OpenAccount(savingsAccount);
            Assert.AreEqual(1, customer.GetNumberOfAccounts());
        }

        [Test]
        public void TestCreationOfTwoAccounts()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);
            Assert.AreEqual(2, customer.GetNumberOfAccounts());
        }

        [Test]
        public void TestCreationOfThreeAcounts()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);
            customer.OpenAccount(maxiSavingAccount);

            Assert.AreEqual(3, customer.GetNumberOfAccounts());
        }

        [Test]
        public void TestTransferFund()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);

            savingsAccount.Deposit(1000);
            checkingAccount.Deposit(1000);

            var transferResult =  customer.TransferFunds(savingsAccount, checkingAccount, 50);

            Assert.AreEqual(transferResult, TransferResult.Transferred);
            Assert.AreEqual(950, savingsAccount.Balance(), "Incorrect savings balance");
            Assert.AreEqual(1050, checkingAccount.Balance(), "Incorrect checking balance");
        }

        [Test]
        public void TestTransferFundInvalidAmount()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);

            savingsAccount.Deposit(1000);
            checkingAccount.Deposit(1000);

            var transferResult = customer.TransferFunds(savingsAccount, checkingAccount, 1050);

            Assert.AreEqual(transferResult, TransferResult.InvalidFunds);
        }
    }
}
