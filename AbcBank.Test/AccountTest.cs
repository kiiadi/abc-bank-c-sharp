using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void sumTransactionsTest()
        {
            Account acct = new Account(AccountType.CHECKING);
            acct.deposit(1000);
            acct.withdraw(500);
            Assert.AreEqual(500, acct.sumTransactions());
        }

        [Test]
        public void transferAccountTest()
        {
            Account acct = new Account(AccountType.CHECKING);
            acct.deposit(1000);
            acct.withdraw(500);
            acct.deposit(500);
            Account newAcct = acct.transferAccounts(acct);

            Assert.AreEqual(acct.Transactions.Count, newAcct.Transactions.Count);
            Assert.AreEqual(acct.AccountType, newAcct.AccountType);
        }
    }
}
