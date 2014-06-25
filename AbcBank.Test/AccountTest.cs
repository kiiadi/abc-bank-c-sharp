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
            Assert.AreEqual(account.interestEarned(), 0.00136986, Constants.DOUBLE_DELTA);
           
            account = new Account(Account.AccountType.SAVINGS);
            account.deposit(500);
            // account < 1000
            Assert.AreEqual(account.interestEarned(), 0.001369863, Constants.DOUBLE_DELTA);
            account.deposit(1000);
            // account > 1000
            Assert.AreEqual(account.interestEarned(), 0.00547945, Constants.DOUBLE_DELTA);

            account = new Account(Account.AccountType.MAXI_SAVINGS);
            account.deposit(500);
            account.Transactions.Add(new Transaction(200.0, Transaction.transactionType.DEPOSIT,
                DateTime.UtcNow.AddDays(-11)));
            account.Transactions.Add(new Transaction(200.0, Transaction.transactionType.DEPOSIT,
                DateTime.UtcNow.AddDays(-15)));
            // Interest rate used here will be 5% because all transactions are older than 10 days
            Assert.AreEqual(account.interestEarned(), 0.00246575, Constants.DOUBLE_DELTA);
            account.Transactions.Add(new Transaction(200.0, Transaction.transactionType.DEPOSIT,
                DateTime.UtcNow.AddDays(-2)));
            // Interest rate used here will 0.1% because there is a transaction within the last 10 days
            Assert.AreEqual(account.interestEarned(), 0.00301369, Constants.DOUBLE_DELTA);
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
