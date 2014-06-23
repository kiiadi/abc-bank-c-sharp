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
        public void testCustomerStatementGeneration()
        {

            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
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

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.AccountType.SAVINGS));
            Assert.AreEqual(oscar.Name, "Oscar");
            Assert.AreEqual(oscar.Accounts.Count(), 1);
            Assert.AreEqual(oscar.Accounts[0].Type, Account.AccountType.SAVINGS);
            oscar.Accounts[0].deposit(100.00);
            Assert.AreEqual(oscar.Accounts[0].sumTransactions(), 100);
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.AccountType.SAVINGS));
            oscar.openAccount(new Account(Account.AccountType.CHECKING));
            Assert.AreEqual(oscar.Name, "Oscar");
            Assert.AreEqual(oscar.Accounts.Count(), 2);
            Assert.AreEqual(oscar.Accounts.Where(a => a.Type == Account.AccountType.SAVINGS).Count(), 1);
            Assert.AreEqual(oscar.Accounts.Where(a => a.Type == Account.AccountType.CHECKING).Count(), 1);
            oscar.Accounts.Where(a => a.Type == Account.AccountType.CHECKING).FirstOrDefault().deposit(100.00);
            oscar.Accounts.Where(a => a.Type == Account.AccountType.SAVINGS).FirstOrDefault().deposit(100.00);
            Assert.AreEqual(oscar.Accounts.Select(x => x.sumTransactions()).Sum(), 200.0);
        }

        [Test]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.AccountType.SAVINGS));
            oscar.openAccount(new Account(Account.AccountType.CHECKING));
            oscar.openAccount(new Account(Account.AccountType.MAXI_SAVINGS));
            Assert.AreEqual(oscar.Name, "Oscar");
            Assert.AreEqual(oscar.Accounts.Count(), 3);
            Assert.AreEqual(oscar.Accounts.Where(a => a.Type == Account.AccountType.SAVINGS).Count(), 1);
            Assert.AreEqual(oscar.Accounts.Where(a => a.Type == Account.AccountType.CHECKING).Count(), 1);
            Assert.AreEqual(oscar.Accounts.Where(a => a.Type == Account.AccountType.MAXI_SAVINGS).Count(), 1);
            oscar.Accounts.Where(a => a.Type == Account.AccountType.CHECKING).FirstOrDefault().deposit(100.00);
            oscar.Accounts.Where(a => a.Type == Account.AccountType.SAVINGS).FirstOrDefault().deposit(100.00);
            oscar.Accounts.Where(a => a.Type == Account.AccountType.MAXI_SAVINGS).FirstOrDefault().deposit(100.00);
            Assert.AreEqual(oscar.Accounts.Select(x => x.sumTransactions()).Sum(), 300.00);
        }
    }
}
