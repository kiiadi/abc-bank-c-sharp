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

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);

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
                    "Total In All Accounts $3,900.00", henry.getStatement());
        }

        [Test] //Test whether transactions exist 
        public void testTransactionsExist()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Customer henry = new Customer("Henry").openAccount(checkingAccount);
            checkingAccount.deposit(100.0);
            Assert.AreEqual(true, checkingAccount.checkIfTransactionsExist());
        }

        [Test] //Test whether transactions do not exist
        public void testTransactionsNotExist()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Customer henry = new Customer("Henry").openAccount(checkingAccount);
            Assert.AreEqual(false, checkingAccount.checkIfTransactionsExist());
        }

        [Test] // Test the creation of one account for one customer
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test] // Test the creation of two different accounts for one customer
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Test] // Test the creation of three different accounts for one customer
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            oscar.openAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }
    }
}
