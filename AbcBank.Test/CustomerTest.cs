using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using AbcBank.AccountManager;


namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

   
        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.openAccount(AccountType.Savings);
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.openAccount(AccountType.Savings);
            oscar.openAccount(AccountType.Checking);
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Test]
        public void WhenCheckingAccountIsCreated()
        {
            Customer oscar = new Customer("Oscar");
     
            var account = oscar.openAccount(AccountType.Checking);
            Assert.IsInstanceOf(typeof(CheckingAccount), account);
        }

        [Test]
        public void WhenSavingsAccountIsCreated()
        {
            Customer oscar = new Customer("Oscar");

            var account = oscar.openAccount(AccountType.Savings);
            Assert.IsInstanceOf(typeof(SavingsAccount), account);
        }

        [Test]
        public void WhenMaxiSavingsAccountIsCreated()
        {
            Customer oscar = new Customer("Oscar");

            var account = oscar.openAccount(AccountType.MaxiSavings);
            Assert.IsInstanceOf(typeof(MaxiSavingsAccount), account);
        }

        [Test]
        public void GettingTotalInterestEarnedOnAllAccount()
        {
            Customer oscar = new Customer("Oscar");

            var account = oscar.openAccount(AccountType.MaxiSavings);
            account.setAccountBalance(54033);

            var account2 = oscar.openAccount(AccountType.Savings);
            account2.setAccountBalance(90003);

            var expectedTotal = account.getInterest() + account2.getInterest();

            var totalInterest = oscar.totalInterestEarned();

            Assert.AreEqual(expectedTotal, totalInterest);
        }

    }
}
