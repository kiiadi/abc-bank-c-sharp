using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    // Using naming convention <Method-Name Under Test>_<Scenario>_<Expected-Outcome>
    [TestFixture]
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void Account_Constructor_InstanceCreated()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            Assert.AreEqual(account.Type, Account.AccountType.CHECKING);
            Assert.IsNotNull(account.Transactions);
        }

        [Test]
        public void deposit_MakeDeposits_MoneyAddedToAccount()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(100.50);
            account.deposit(2000.23);
            Assert.AreEqual(account.sumTransactions(), 2100.73);
        }

        [Test]
        public void withdraw_MakeWithdrawal_MoneyRemovedFromAccount()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.sumTransactions(), 9999.84);
            account.withdraw(1000.80);
            Assert.AreEqual(account.sumTransactions(), 8999.04);
        }

        [Test]
        public void interestEarned_MakeDeposit_InterestEarned()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(500);
            Assert.AreEqual(account.interestEarned(), 0.5);
           
            account = new Account(Account.AccountType.SAVINGS);
            account.deposit(500);
            // account < 1000
            Assert.AreEqual(account.interestEarned(), 0.5);
            account.deposit(1000);
            // account > 1000
            Assert.AreEqual(account.interestEarned(), 2.0);
 
            account = new Account(Account.AccountType.MAXI_SAVINGS);
            account.deposit(500);
            // account < 1000
            Assert.AreEqual(account.interestEarned(), 10.0);
            account.deposit(1000);
            // account < 2000
            Assert.AreEqual(account.interestEarned(), 45.0);
            account.deposit(1000);
            // account > 2000
            Assert.AreEqual(account.interestEarned(), 120.0);
        }


        [Test]
        public void sumTransactions_CreateTransactions_SumOfTransactions()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.sumTransactions(), 9999.84);
            account.withdraw(1000.80);
            Assert.AreEqual(account.sumTransactions(), 8999.04);
        }

        [Test]
        public void getStringRepresentationForAccount_AddAccounts_StringRepresentingAccounts()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            Assert.AreEqual(account.getStringRepresentationForAccount(), "Checking Account\n");
            account = new Account(Account.AccountType.SAVINGS);
            Assert.AreEqual(account.getStringRepresentationForAccount(), "Savings Account\n");
            account = new Account(Account.AccountType.MAXI_SAVINGS);
            Assert.AreEqual(account.getStringRepresentationForAccount(), "Maxi Savings Account\n");

        }
    }
}
