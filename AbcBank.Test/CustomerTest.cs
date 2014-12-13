using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbcBank;

namespace AbcBank.Test
{
    [TestClass]
    public class CustomerTest
    {

        [TestMethod] //Test customer statement generation
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

        [TestMethod]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [TestMethod]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [TestMethod]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            oscar.openAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }

        [TestMethod] //Test transfer between customer account
        public void testTransfer()
        {

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(500.0);

            checkingAccount.TransferToAccount(100.0, savingsAccount);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  withdrawal $100.00\n" +
                    "Total $0.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $500.00\n" +
                    "  deposit $100.00\n" +
                    "Total $600.00\n" +
                    "\n" +
                    "Total In All Accounts $600.00", henry.getStatement());
        }


        [TestMethod] //Test transfer between customer account
        public void testAccrIntr()
        {

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(500.0);

            double accrIntr = henry.getAccruedInterest();
            Assert.AreEqual(0.082, accrIntr,0.001);
        }        

    }
}
