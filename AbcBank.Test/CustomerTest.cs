using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.AbcAccount;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(AccountType.Savings);
            
            Account checkingAccount = new Account(AccountType.Checking);
            CustomerAccount cusAccount = new CustomerAccount(new Customer("Henry")).openAccount(savingsAccount).openAccount(checkingAccount);

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
                    "Total In All Accounts $3,900.00", BankUtility.getStatement(cusAccount));
        }

        [Test]
        public void testOneAccount()
        {
            Account savingsAccount = new Account(AccountType.Savings);
            CustomerAccount cusAccount = new CustomerAccount(new Customer("Oscar")).openAccount(savingsAccount);
            Assert.AreEqual(1, cusAccount.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Account checkingAccount = new Account(AccountType.Checking);
            Account savingsAccount = new Account(AccountType.Savings);
            
            CustomerAccount cusAccount = new CustomerAccount(new Customer("Oscar")).openAccount(savingsAccount).openAccount(checkingAccount);
            Assert.AreEqual(2, cusAccount.getNumberOfAccounts());
        }

        [Test]
        public void testTransfer()
        {
            Account checkingAccount = new Account(AccountType.Checking);
            Account savingsAccount = new Account(AccountType.Savings);

            CustomerAccount cusAccount = new CustomerAccount(new Customer("Oscar")).openAccount(savingsAccount).openAccount(checkingAccount);

            checkingAccount.deposit(1000);
            checkingAccount.Transfer(savingsAccount, 100);
            //BankUtility.getStatement(cusAccount);
           
            Assert.AreEqual("Statement for Oscar\n" +
                   "\n" +
                   "Savings Account\n" +
                   "  deposit $100.00\n" +
                   "Total $100.00\n" +
                   "\n" +
                   "Checking Account\n" +
                   "  deposit $1,000.00\n" +
                   "  withdrawal $100.00\n" +
                   "Total $900.00\n" +
                   "\n" +
                   "Total In All Accounts $1,000.00", BankUtility.getStatement(cusAccount));
        }

        [Ignore]
        public void testThreeAcounts()
        {
           
            Account checkingAccount = new Account(AccountType.Checking);
            Account savingsAccount = new Account(AccountType.Savings);

            CustomerAccount cusAccount = new CustomerAccount(new Customer("Oscar")).openAccount(savingsAccount).openAccount(checkingAccount).openAccount(savingsAccount);

            Assert.AreEqual(3, cusAccount.getNumberOfAccounts());
        }
    }
}
