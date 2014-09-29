using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture, Description("Tests for Checking Account class and its base Account class")]
    public class AccountTest
    {
        
        [Test]
        [ExpectedException(ExpectedMessage = Account.INVALID_TRANSACTION_AMOUNT)]
        public void Should_Fail_Deposit_For_Invalid_Amount([Values(-0.01, 0, -50)] double amount)
        {
            var checking = new Account(AccountType.Checking);
            checking.Deposit(amount);
        }
        [Test]
        public void Should_Calculate_Savings_Account_Earned_Interest_For_365_Days()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-365), 100000)
            };
            var checking = new Account(AccountType.Savings, transactions);
            var earned = checking.GetEarnedInterest();
            Console.WriteLine("Earned: {0}", earned);
            Assert.AreEqual(199.1981d, earned, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Calculate_MaxiSavings_Account_Earned_Interest_For_365_Days()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-365), 100000)
            };
            var checking = new Account(AccountType.MaxiSavings, transactions);
            var earned = checking.GetEarnedInterest();
            Console.WriteLine("Earned: {0}", earned);
            Assert.AreEqual(5126.7496d, earned, Helper.TOLERANCE);
        }
        [Test]
        [Ignore]
        public void Should_Calculate_MaxiSavings_Account_Earned_Interest_For_365_Days_When_There_is_A_Withdrawal()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-365), 100000),
                new Transaction(DateTime.Today.AddDays(-9), 500)
            };
            var checking = new Account(AccountType.MaxiSavings, transactions);
            var earned = checking.GetEarnedInterest();
            Console.WriteLine("Earned: {0}", earned);
            Assert.Fail("Need to determine the expected value");
        }
        [Test]
        [ExpectedException(ExpectedMessage = Account.INVALID_TRANSACTION_AMOUNT)]
        public void Should_Fail_Withdraw_For_Invalid_Amount([Values(-0.01, 0, -50)] double amount)
        {
            var checking = new Account(AccountType.Checking);
            checking.Withdraw(amount);
        }        
        [Test]
        public void Should_Deposit([Random(1.5, 10000, 10)]  double amount)
        {
            var checking = new Account(AccountType.Checking);
            checking.Deposit(amount);
            Assert.AreEqual(amount, checking.GetSumOfTransactions(), Helper.DOUBLE_DELTA);
        }
        [Test, Sequential]
        [ExpectedException(ExpectedMessage = Account.OVERDRAFT_ERROR)]
        public void Should_Fail_Withdraw_For_Overdraft([Values(1, 10, 499, 1000)]  double depositAmount, [Values(1.01, 10.9, 500, 1001)]  double withdrawalAmount)
        {
            var checking = new Account(AccountType.Checking);
            checking.Deposit(depositAmount);
            checking.Withdraw(withdrawalAmount);
        }
        [Test, Sequential]
        public void Should_Calculate_Checking_Account_Earned_Interest_For_100K([Values(1, 10, 30, 365, 400)]  int days, [Values(0.274d, 2.7398d, 8.2195d, 100.0499d, 109.649d)]  double expectedEarnedInterest)
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-days), 100000)
            };
            var checking = new Account(AccountType.Checking, transactions);
            var calculatedEarnedInterest = checking.GetEarnedInterest();
            Console.WriteLine("Expected: {0} Earned: {1}", expectedEarnedInterest, calculatedEarnedInterest);
            Assert.AreEqual(expectedEarnedInterest, calculatedEarnedInterest, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Calculate_Checking_Account_Earned_Interest_For_Multiple_Transactions1()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-10), 100000),
                new Transaction(DateTime.Today, 10000)
            };
            var checking = new Account(AccountType.Checking, transactions);
            var calculatedEarnedInterest = checking.GetEarnedInterest();
            var expected = 2.74d;
            Console.WriteLine("Expected: {0} Earned: {1}", expected, calculatedEarnedInterest);
            Assert.AreEqual(expected, calculatedEarnedInterest, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Calculate_Checking_Account_Earned_Interest_For_Multiple_Transactions2()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-100), 100000),
                new Transaction(DateTime.Today.AddDays(-90), 10000)
            };
            var checking = new Account(AccountType.Checking, transactions);
            var calculatedEarnedInterest = checking.GetEarnedInterest();
            var expected = 29.867d;
            Console.WriteLine("Expected: {0} Earned: {1}", expected, calculatedEarnedInterest);
            Assert.AreEqual(expected, calculatedEarnedInterest, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Calculate_Checking_Account_Earned_Interest_For_Multiple_Transactions_Incl_A_Withdrawal()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.Today.AddDays(-100), 100000), 
                new Transaction(DateTime.Today.AddDays(-90), 10000), 
                new Transaction(DateTime.Today.AddDays(-50), -5000) //withdrawal
            };
            var checking = new Account(AccountType.Checking, transactions);
            var calculated = checking.GetEarnedInterest();
            var expected = 29.1821d;
            Console.WriteLine("Expected: {0} Earned: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Have_Checking_Account_Title()
        {
            Assert.AreEqual("Checking Account", new Account(AccountType.Checking).Title);
        }
        [Test]
        public void Should_Calculate_Checking_Account_Current_Balance()
        {
            var checking = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-200), 100000), 
                    new Transaction(DateTime.Today.AddDays(-190), 10000), 
                    new Transaction(DateTime.Today.AddDays(-150), -5000), 
                    new Transaction(DateTime.Today.AddDays(-100), 1000)
                }
           );
            
            checking.Withdraw(6000);
            checking.Deposit(14001.99);
            var calculated = checking.GetCurrentBalance();
            var expected = 114060.2251d;
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Calculate_Checking_Account_GetSumOfTransactions()
        {
            var checking = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-200), 100000), 
                    new Transaction(DateTime.Today.AddDays(-190), 10000), 
                    new Transaction(DateTime.Today.AddDays(-150), -5000), 
                    new Transaction(DateTime.Today.AddDays(-100), 1000)
                }
           );

            checking.Withdraw(6000);
            checking.Deposit(14001.99);
            var calculated = checking.GetSumOfTransactions();
            var expected = 114001.99d;
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated, Helper.TOLERANCE);
        }
        [Test]
        public void Should_Format_Checking_Account_Statement()
        {
            var checking = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-200), 100000), 
                    new Transaction(DateTime.Today.AddDays(-190), 10000), 
                    new Transaction(DateTime.Today.AddDays(-150), -5000), 
                    new Transaction(DateTime.Today.AddDays(-100), 1000)
                }
           );

            checking.Withdraw(6000);
            checking.Deposit(14001.99);
            var calculated = checking.GetStatement();
            var expected = "Checking Account\r\n  deposit $100,000.00\r\n  deposit $10,000.00\r\n  withdrawal $5,000.00\r\n  deposit $1,000.00\r\n  withdrawal $6,000.00\r\n  deposit $14,001.99\r\nTotal $114,001.99";
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated);
        }
    }
}
