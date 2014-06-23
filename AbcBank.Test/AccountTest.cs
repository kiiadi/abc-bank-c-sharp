using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void creation()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            Assert.AreEqual(account.Type, Account.AccountType.CHECKING);
            Assert.IsNotNull(account.Transactions);
        }

        [Test]
        public void deposit()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(100.50);
            account.deposit(2000.23);
            Assert.AreEqual(account.sumTransactions(), 2100.73);
        }

        [Test]
        public void withdraw()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.sumTransactions(), 9999.84);
            account.withdraw(1000.80);
            Assert.AreEqual(account.sumTransactions(), 8999.04);
        }

        [Test]
        public void interestEarned()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.interestEarned(), 9.99984);
        }


        [Test]
        public void sumTransactions()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.sumTransactions(), 9999.84);
            account.withdraw(1000.80);
            Assert.AreEqual(account.sumTransactions(), 8999.04);
        }

        [Test]
        public void getStringRepresentationForAccount()
        {
            Account account = new Account(Account.AccountType.CHECKING);
            account.deposit(9999.84);
            Assert.AreEqual(account.interestEarned(), 9.99984);
        }


    }
}
