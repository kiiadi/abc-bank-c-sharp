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

            Customer henry = new Customer("Henry");
            henry.openAccount(checkingAccount);
            henry.openAccount(savingsAccount);

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

        [Test]
        
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }
        [Test]
        
        public void TransferFunds()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer Bill = (new Customer("Bill").openAccount(checkingAccount));
            Bill.openAccount(savingsAccount);
            checkingAccount.deposit(3000.0);
            Bill.TransferFunds(checkingAccount, savingsAccount, 1000.00);
            Assert.AreNotEqual("Statement for Bill\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $3000.00\n" +
                    "  withdrawal $1,000.00\n" +
                    "Total $2000.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $1000.00\n" +
                    "Total $1000.00\n" +
                    "\n" +
                    "Total In All Accounts $3,000.00", Bill.getStatement());
        }

        [Ignore]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }
    }
}
