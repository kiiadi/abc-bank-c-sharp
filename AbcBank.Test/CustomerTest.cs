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

        [Test] //Test customer statement generation
        public void testApp()
        {
            Account checkingAccount = new Account(Account.AccountType.Checking);
            Account savingsAccount = new Account(Account.AccountType.Saving);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
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
                    "Total In All Accounts $3,900.00", henry.BuildStatement());
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.Saving));
            Assert.AreEqual(1, oscar.NumberOfAccounts);
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.AccountType.Saving));
            oscar.OpenAccount(new Account(Account.AccountType.Checking));
            Assert.AreEqual(2, oscar.NumberOfAccounts);
        }

        [Ignore]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.AccountType.Saving));
            oscar.OpenAccount(new Account(Account.AccountType.Checking));
            Assert.AreEqual(3, oscar.NumberOfAccounts);
        }
    }
}
