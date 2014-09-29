using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void Should_Set_Customer_Name()
        {
            var customer = new Customer("John");
            Assert.AreEqual("John", customer.Name);
        }
        [Test]
        public void Should_Add_Customer_Account()
        {
            var customer = new Customer("Oscar");
            customer.AddAccount(new Account(AccountType.Checking));
            Assert.AreEqual(1, customer.Accounts.Count());
        }
        [Test]
        public void Should_Earn_No_Interest1()
        {
            var customer = new Customer("Oscar");
            Assert.AreEqual(0d, customer.GetEarnedInterest());
        }
        [Test]
        public void Should_Earn_No_Interest2()
        {
            var customer = new Customer("Oscar");
            customer.AddAccount(new Account(AccountType.Checking));
            Assert.AreEqual(0d, customer.GetEarnedInterest());
        }
        [Test]
        public void Should_Earn_No_Interest3()
        {
            var customer = new Customer("Oscar");
            var account = new Account(AccountType.Checking);
            account.Deposit(10000);
            customer.AddAccount(account);
            Assert.AreEqual(0d, customer.GetEarnedInterest());
        }
        [Test]
        public void Should_Earn_No_Interest4()
        {
            var customer = new Customer("Oscar");
            customer.AddAccount(new Account(AccountType.Checking));
            customer.AddAccount(new Account(AccountType.Checking));
            customer.AddAccount(new Account(AccountType.MaxiSavings));
            customer.AddAccount(new Account(AccountType.Savings));
            Assert.AreEqual(0d, customer.GetEarnedInterest());
        }
        [Test]       
        public void Should_Earn_Interest1()
        {
            
            var account = new Account
           (
               AccountType.Checking,
               new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
          );

            var customer = new Customer("Oscar");
            customer.AddAccount(account);


            var earned = customer.GetEarnedInterest();
            var expected = 29.867d;
            Console.WriteLine("Expected: {0} Earned: {1}", expected, earned);
            Assert.AreEqual(expected, earned, Helper.TOLERANCE);

        }
        [Test]
        public void Should_Format_Statement()
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

            var customer = new Customer("Oscar");
            customer.AddAccount(checking);

            var calculated = customer.GetStatement();
            var expected = "Statement for Oscar\nChecking Account\r\n  deposit $100,000.00\r\n  deposit $10,000.00\r\n  withdrawal $5,000.00\r\n  deposit $1,000.00\r\n  withdrawal $6,000.00\r\n  deposit $14,001.99\r\nTotal $114,001.99\n\nTotal In All Accounts $114,001.99";
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated);
        }
        [Test]
        public void Should_Get_Summary_No_Accounts()
        {
            var customer = new Customer("George");
            var calculated = customer.GetSummary();
            var expected = "- George (no accounts)";
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated);

        }
        [Test]
        public void Should_Get_Summary_One_Account()
        {
            var customer = new Customer("George");
            customer.AddAccount(new Account(AccountType.Checking));
            var calculated = customer.GetSummary();
            var expected = "- George (1 account)";
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated);

        }
        [Test]
        public void Should_Get_Summary_Three_Accounts()
        {
            var customer = new Customer("George");
            customer.AddAccount(new Account(AccountType.Checking));
            customer.AddAccount(new Account(AccountType.MaxiSavings));
            customer.AddAccount(new Account(AccountType.Savings));

            var calculated = customer.GetSummary();
            var expected = "- George (3 accounts)";
            Console.WriteLine("Expected: {0} calculated: {1}", expected, calculated);
            Assert.AreEqual(expected, calculated);

        }
        [Test]
        [ExpectedException(ExpectedMessage = Customer.INVALID_SOURCE_ACCOUNT)]
        public void Should_Fail_Transfer_From_Invalid_Account()
        {

            var fromAccount = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
           );

            var toAccount = new Account
           (
               AccountType.Checking,
               new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
          );

            var customer = new Customer("Tom");
            customer.AddAccount(toAccount);
            customer.Transfer(fromAccount, toAccount, 1000);


        }
        [Test]
        [ExpectedException(ExpectedMessage = Customer.INVALID_TARGET_ACCOUNT)]
        public void Should_Fail_Transfer_To_Invalid_Account()
        {

            var fromAccount = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
           );

            var toAccount = new Account
           (
               AccountType.Checking,
               new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
          );

            var customer = new Customer("Tom");
            customer.AddAccount(fromAccount);
            customer.Transfer(fromAccount, toAccount, 1000);


        }
        [Test]
        [ExpectedException(ExpectedMessage = Customer.INVALID_TRANSFER_AMOUNT)]
        public void Should_Fail_To_Transfer_Invalid_Amount([Values(-10, 0)] double amount)
        {

            var fromAccount = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
           );

            var toAccount = new Account
           (
               AccountType.Checking,
               new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
          );

            var customer = new Customer("Tom");
            customer.AddAccount(fromAccount);
            customer.AddAccount(toAccount);
            customer.Transfer(fromAccount, toAccount, amount);

        }

        [Test]        
        public void Should_Transfer_Amount([Values(1000, 5000, 110000)] double amount)
        {
            var fromAccount = new Account
            (
                AccountType.Checking,
                new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-100), 100000),
                    new Transaction(DateTime.Today.AddDays(-90), 10000)
                }
           );

           var toAccount = new Account(AccountType.Checking);

            var customer = new Customer("Tom");
            customer.AddAccount(fromAccount);
            customer.AddAccount(toAccount);

            var fromBefore = fromAccount.GetCurrentBalance();
            var toBefore = toAccount.GetCurrentBalance();
            customer.Transfer(fromAccount, toAccount, amount);
            var fromAfter = fromAccount.GetCurrentBalance();
            var toAfter= toAccount.GetCurrentBalance();

            Assert.AreEqual(amount, fromBefore - fromAfter, Helper.DOUBLE_DELTA);
            Assert.AreEqual(amount, toAfter - toBefore, Helper.DOUBLE_DELTA);

        }      
    }
}
