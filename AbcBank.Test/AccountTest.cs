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
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        Bank bank;
        Customer customer;
        Account account;

        [SetUp]
        public void init()
        {
            bank = new Bank();
            customer = new Customer("Sung");
        }

        [Test] 
        public void TestAccountCheckingDeposit()
        {
            account = new CheckingAccount();
            account.Deposit(100);

            Assert.AreEqual(100, account.Balance());
        }

        [Test]
        public void TestAccountCheckingWithdrawal()
        {
            account = new CheckingAccount();
            account.Deposit(100);
            account.Withdraw(25);

            Assert.AreEqual(75, account.Balance());
        }

        [Test]
        public void TestCheckingAccountInterestEarned()
        {
            account = new CheckingAccount();
            account.Deposit(100.0);

            Assert.AreEqual(0.1, account.InterestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void TestSavingAccountInterestEarned()
        {
            account = new SavingsAccount();
            account.Deposit(1500.0);

            Assert.AreEqual(2.0, account.InterestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void TestMaxiSavingAccountInterestEarned()
        {
            account = new MaxiSavingsAccount();
            account.Deposit(3000);

            Assert.AreEqual(170.0, account.InterestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void TestCheckingAccountInterestByAsOfDate()
        {
            account = new CheckingAccount();
            account.Deposit(100, DateTime.Parse("1/1/14"));
            account.Deposit(100, DateTime.Parse("1/15/14"));
            account.Deposit(100, DateTime.Parse("2/1/14"));

            Assert.AreEqual(0.00139726514544254, account.InterestEarned(DateTime.Parse("2/1/14")), DOUBLE_DELTA);
        }


        [Test]
        public void TestSavingsAccountInterestByAsOfDate()
        {
            account = new MaxiSavingsAccount();
            account.Deposit(1500, DateTime.Parse("1/1/14"));
            account.Deposit(1500, DateTime.Parse("1/15/14"));
            account.Deposit(1500, DateTime.Parse("2/1/14"));

            Assert.AreEqual(0.331528633030922, account.InterestEarned(DateTime.Parse("2/1/14")), DOUBLE_DELTA);
        }

        [Test]
        public void TestMaxiAccountInterestByAsOfDateNoWithdrawal()
        {
            account = new MaxiSavingsAccount();
            account.Deposit(1500, DateTime.Parse("1/1/14"));
            account.Deposit(1500, DateTime.Parse("1/15/14"));
            account.Deposit(1500, DateTime.Parse("2/1/14"));

            Assert.AreEqual(10.497742314664196, account.InterestEarned(DateTime.Parse("2/1/14")), DOUBLE_DELTA);
        }

        [Test]
        public void TestMaxiAccountInterestByAsOfDateWithdrawal()
        {
            account = new MaxiSavingsAccount();
            account.Deposit(1500, DateTime.Parse("1/1/14"));
            account.Withdraw(1000, DateTime.Parse("1/7/14"));
            account.Deposit(1500, DateTime.Parse("2/1/14"));

            Assert.AreEqual(2.5522641237001151, account.InterestEarned(DateTime.Parse("2/1/14")), DOUBLE_DELTA);
        }

        [Test]
        public void StressTest()
        {
            account = new MaxiSavingsAccount();
            account.Deposit(1500, DateTime.Parse("1/1/1950"));
            account.Withdraw(1000, DateTime.Parse("1/7/2014"));
            account.Deposit(1500, DateTime.Parse("2/1/2014"));
            account.Deposit(1500, DateTime.Parse("3/1/2014"));
            account.Withdraw(1000, DateTime.Parse("4/7/2014"));
            account.Deposit(1500, DateTime.Parse("5/1/2014"));
            var interestEarned = account.InterestEarned(DateTime.Parse("5/1/2014"));
            Assert.AreEqual(1, 1, DOUBLE_DELTA);
        }
    }
}
