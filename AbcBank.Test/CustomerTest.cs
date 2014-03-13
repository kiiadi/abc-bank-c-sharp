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
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {

            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            IAccountsInterface HenryMaxiSavings = new MaxiSavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            Henry.AddAccount(HenryMaxiSavings);
            Henry.Deposit(HenryChecking, 100.00);
            Henry.Deposit(HenryChecking, 225.00);
            Henry.Deposit(HenrySavings, 1550.00);
            Henry.Withdraw(HenrySavings, 225.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Withdraw(HenrySavings, 1225.00);
            Henry.Deposit(HenryMaxiSavings, 1747.00);
            Henry.Deposit(HenryMaxiSavings, 2750.00);
            Henry.Transfer(HenryMaxiSavings, HenryChecking, 2750);

            Assert.AreEqual("Statement for Henry \r\n\n  Checking Account\r\n\n  $100.00  deposit  \n\r\n\n  $225.00  deposit  \n\r\n\n  $2,750.00  deposit  \n\r\nTotal: $3,075.00\r\n  \n\r\n\n  " +
                "Savings Account\r\n\n  $1,550.00  deposit  \n\r\n\n  $225.00  withdrawal  \n\r\n\n  $1,225.00  deposit  \n\r\n\n  $1,225.00  withdrawal  \n\r\nTotal: $1,325.00\r\n  \n\r\n\n  " +
                "Maxi Savings Account\r\n\n  $1,747.00  deposit  \n\r\n\n  $2,750.00  deposit  \n\r\n\n  $2,750.00  withdrawal  \n\r\nTotal: $1,747.00\r\n  \n\r\n\n" +
                "Total In All Accounts: $6,147.00\r\n", Henry.GetAccountStatementforCustomer());
        }

        [Test]
        public void testOneAccount()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Assert.AreEqual(1, Henry.TotalAccounts);
        }

        [Test]
        public void testTwoAccount()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            Assert.AreEqual(2, Henry.TotalAccounts);
        }

        [Test]
        public void testThreeAcounts()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            IAccountsInterface HenryMaxiSavings = new MaxiSavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            Henry.AddAccount(HenryMaxiSavings);
            Assert.AreEqual(3, Henry.TotalAccounts);
        }

        [Test]
        public void testCheckingDeposits()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.Deposit(HenryChecking, 100.00);
            Henry.Deposit(HenryChecking, 225.00);
            Assert.AreEqual("Statement for Henry \r\n\n  Checking Account\r\n\n  $100.00  deposit  \n\r\n\n  $225.00  deposit  \n\r\nTotal: $325.00\r\n  \n\r\n\nTotal In All Accounts: $325.00\r\n", Henry.GetAccountStatementforCustomer());
        }

        [Test]
        public void testSavingsToCheckingsTransfers()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            Henry.Deposit(HenryChecking, 100.00);
            Henry.Deposit(HenryChecking, 225.00);
            Henry.Deposit(HenryChecking, 1750.00);
            Henry.Deposit(HenrySavings, 1550.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Transfer(HenrySavings, HenryChecking, 1125.00);
            Assert.AreEqual("Statement for Henry \r\n\n  Checking Account\r\n\n  $100.00  deposit  \n\r\n\n  $225.00  deposit  \n\r\n\n  $1,750.00  deposit  \n\r\n\n  $1,125.00  deposit  \n\r\nTotal: $3,200.00\r\n  \n\r\n\n  Savings Account\r\n\n  $1,550.00  deposit  \n\r\n\n  $1,225.00  deposit  \n\r\n\n  $1,125.00  withdrawal  \n\r\nTotal: $1,650.00\r\n  \n\r\n\nTotal In All Accounts: $4,850.00\r\n", Henry.GetAccountStatementforCustomer());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSavingsToCheckingsTransfersException()
        {
            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            Henry.Deposit(HenryChecking, 100.00);
            Henry.Deposit(HenryChecking, 225.00);
            Henry.Deposit(HenrySavings, 1550.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Transfer(HenrySavings, HenryChecking, 3225.00);
        }

        [Test]
        public void testSavingsWithdrawal()
        {
            IAccountsInterface HenrySavings = new SavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenrySavings);
            Henry.Deposit(HenrySavings, 100.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Withdraw(HenrySavings, 1225.00);
            Assert.AreEqual("Statement for Henry \r\n\n  Savings Account\r\n\n  $100.00  deposit  \n\r\n\n  $1,225.00  deposit  \n\r\n\n  $1,225.00  withdrawal  \n\r\nTotal: $100.00\r\n  \n\r\n\n" +
                "Total In All Accounts: $100.00\r\n", Henry.GetAccountStatementforCustomer());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testSavingsWithdrawalException()
        {
            IAccountsInterface HenrySavings = new SavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenrySavings);
            Henry.Deposit(HenrySavings, 100.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Withdraw(HenrySavings, 3225.00);
        }
    }
}
