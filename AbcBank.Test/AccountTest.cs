using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void AccountDeposit_Throws_ArgumentException()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account account = new Account(AccountType.Checking);
            john.OpenAccount(account);
            double amount = -1.0;

            try
            {
                account.Deposit(amount);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, Account.AmountIsNotPositiveMessage);
                return;
            }

            Assert.Fail("Account.Deposit did not throw expected ArgumentException");
        }

        [Test]
        public void AccountWithdrow_Throws_ArgumentException_AmountLessThanZero()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account account = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(account);
            double amount = -1.0;

            try
            {
                account.Withdraw(amount);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, Account.AmountIsNotPositiveMessage);
                return;
            }

            Assert.Fail("Account.Withdrow did not throw expected ArgumentException");
        }

        [Test]
        public void AccountWithdrow_Throws_ArgumentException_AmountExceedsBalance()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account account = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(account);
            double depositAmount = 1000.0;
            double withdrawAmount = 1001.0;

            try
            {
                account.Deposit(depositAmount);
                account.Withdraw(withdrawAmount);
            }
            catch (ArgumentException e)
            {
                double balance = account.Balance;
                String message = String.Format(Account.AmountExceedsBalanceMessage, withdrawAmount, balance);
                StringAssert.Contains(e.Message, message);
                return;
            }

            Assert.Fail("Account.Withdrow did not throw expected ArgumentException");
        }
    }
}
