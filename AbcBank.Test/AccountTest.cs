using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void AccountHasNoTransactions()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account account = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(account);

            Assert.AreEqual(0.0, account.InterestEarned(), Utility.DOUBLE_DELTA);
        }

        [Test]
        public void AccountDeposit_Throws_ArgumentException()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account account = new Account(AccountType.Checking);
            john.OpenAccount(account);
            double amount = -1.23;

            try
            {
                account.Deposit(amount);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Amount entered -1.23, but it must be positive");
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
            double amount = -1.23;

            try
            {
                account.Withdraw(amount);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Amount entered -1.23, but it must be positive");
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
            double depositAmount = 100.0;
            double withdrawAmount = 101.0;

            try
            {
                account.Deposit(depositAmount);
                account.Withdraw(withdrawAmount);
            }
            catch (ArgumentException e)
            {
                double balance = account.Balance;
                String message = String.Format("Withdraw: amount {0} exceeds balance {1}", withdrawAmount, balance);
                StringAssert.Contains(e.Message, message);
                return;
            }

            Assert.Fail("Account.Withdrow did not throw expected ArgumentException");
        }

        [Test]
        public void TransferToAccount_Throws_ArgumentException_AmountLessThanZero()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account accountFrom = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(accountFrom);
            double amount = -1.23;

            Account accountTo = new Account(AccountType.Savings);
            john.OpenAccount(accountTo);

            try
            {
                accountFrom.TransferToAccount(accountTo, amount);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Amount entered -1.23, but it must be positive");
                return;
            }

            Assert.Fail("Account.Withdrow did not throw expected ArgumentException");
        }

        [Test]
        public void TransferToAccount_Throws_ArgumentException_AmountExceedsBalance()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account accountFrom = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(accountFrom);
            double depositAmount = 100.0;
            double transferAmount = 101.0;

            Account accountTo = new Account(AccountType.Savings);
            john.OpenAccount(accountTo);

            try
            {
                accountFrom.Deposit(depositAmount);
                accountFrom.TransferToAccount(accountTo, transferAmount);
            }
            catch (ArgumentException e)
            {
                double balance = accountFrom.Balance;
                String message = String.Format("Withdraw: amount {0} exceeds balance {1}", transferAmount, balance);
                StringAssert.Contains(e.Message, message);
                return;
            }

            Assert.Fail("Account.Withdrow did not throw expected ArgumentException");
        }

        [Test]
        public void TransferToAccount_Completes()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.AddCustomer(john);
            Account accountFrom = new Account(AccountType.Maxi_Savings);
            john.OpenAccount(accountFrom);
            double depositAmountFrom = 200.0;

            Account accountTo = new Account(AccountType.Savings);
            john.OpenAccount(accountTo);
            double depositAmountTo = 100.0;

            double transferAmount = 30.0;

            accountFrom.Deposit(depositAmountFrom);
            accountTo.Deposit(depositAmountTo);
            accountFrom.TransferToAccount(accountTo, transferAmount);

            String customerStatement = "Statement for John\n" +
                   "\n" +
                   "Maxi-Savings Account\n" +
                   "  deposit $200.00\n" +
                   "  withdrawal $30.00\n" +
                   "Total $170.00\n" +
                   "\n" +
                   "Savings Account\n" +
                   "  deposit $100.00\n" +
                   "  deposit $30.00\n" +
                   "Total $130.00\n" +
                   "\n" +
                   "Total In All Accounts $300.00";
            Console.WriteLine(customerStatement);

            Assert.AreEqual(customerStatement, john.GetStatement());
        }
    }
}
