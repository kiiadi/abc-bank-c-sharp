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
        public void checkingDeposits()
        {
            Account account = new Account(Account.AccountType.Checking);
            account.Deposit(20);
            account.Deposit(30);
            account.Deposit(50);
            account.Withdraw(10);
            Assert.AreEqual(account.SumTransactions(), 90);
        }
    }
}
