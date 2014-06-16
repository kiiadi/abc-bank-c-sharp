using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void CustomerStatementGeneration()
        {

            Account checkingAccount = new Account(AccountType.Checking);
            Account savingsAccount = new Account(AccountType.Savings);
            Account maxiSavingsAccount = new Account(AccountType.Maxi_Savings);

            Customer henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);
            henry.OpenAccount(maxiSavingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            String customerStatement = "Statement for Henry\n" +
                               "\n" +
                               "Checking Account\n" +
                               "  deposit $100.00\n" +
                               "Total $100.00\n" +
                               "\n" +
                               "Savings Account\n" +
                               "  deposit $4,000.00\n" +
                               "  withdrawal $200.00\n" +
                               "Total $3,800.00\n" +
                               "\n" +
                               "Maxi-Savings Account\n" +
                               "Total $0.00\n" +
                               "\n" +
                               "Total In All Accounts $3,900.00";
            Console.WriteLine(customerStatement);
            Assert.AreEqual(customerStatement, henry.GetStatement());
        }

        [Test]
        public void CustomerWithNoAccounts()
        {
            Customer oscar = new Customer("Oscar");
            //Account account = new Account(AccountType.Savings);
            //oscar.OpenAccount(account);

            Assert.AreEqual(0, oscar.NumberOfAccounts);
        }

        [Test]
        public void CustomerHavingOneAccount()
        {
            Customer oscar = new Customer("Oscar");
            Account account = new Account(AccountType.Savings);
            oscar.OpenAccount(account);

            Assert.AreEqual(1, oscar.NumberOfAccounts);
        }

        [Test]
        public void CustomerHavingMoreThanOneAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(AccountType.Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));
            Assert.AreEqual(2, oscar.NumberOfAccounts);
        }

        [Test]
        public void CustomerHavingMoreThanOneAccountOfTheSameType()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(AccountType.Maxi_Savings));
            oscar.OpenAccount(new Account(AccountType.Checking));
            oscar.OpenAccount(new Account(AccountType.Maxi_Savings));
            Assert.AreEqual(3, oscar.NumberOfAccounts);
        }
    }
}
