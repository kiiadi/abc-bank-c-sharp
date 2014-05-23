using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {
        /** Pull unit test variables from a configuration file **/
        static NameValueCollection testSettings;

        /** Returned the value of the passed configuration file key **/
        public string GetSettings(string key)
        {
            return testSettings[key];
        }

        /** Construct a customer test object **/
        public CustomerTest()
        {
            /** Initialize the testSettings object to the collection of configuration file key/value pairs **/
            testSettings = ConfigurationManager.AppSettings as NameValueCollection;
        }

        /** Customer object with theree accounts to be used throughout the test **/
        Customer chin = new Customer("Chin");

        /** Account objects to be used throughout the test **/
        Account checkingAccount = new Account(Account.CHECKING);
        Account savingsAccount = new Account(Account.SAVINGS);
        Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);


        //BASIC Condition Tests - expected conditions that fit typical input/output parameter(s) and/or result(s)
        [Test]
        public void CustomerObjectCreation()
        {
            /** Account objects to be used throughout the test **/
            checkingAccount = new Account(Account.CHECKING);
            savingsAccount = new Account(Account.SAVINGS);
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            chin = new Customer("Chin");

            /** Deposit funds into each account **/
            checkingAccount.deposit(1.00);
            savingsAccount.deposit(1.00);
            maxiSavingsAccount.deposit(1.00);
            chin.openAccount(checkingAccount).openAccount(savingsAccount).openAccount(maxiSavingsAccount);

            //Assert that each object is initialized correctly
            Assert.AreEqual("Chin", chin.getName());
            Assert.AreEqual(3, chin.getNumberOfAccounts());
            Assert.That(chin.totalInterestEarned().Equals(((1 * .001 * 365) + (1 * .001 * 365) + (1 * .05 * 365))));
        }

        [Test]
        public void CustomerStatement()
        {
            /** Account objects to be used throughout the test **/
            checkingAccount = new Account(Account.CHECKING);
            savingsAccount = new Account(Account.SAVINGS);
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            chin = new Customer("Chin");

            /** Deposit funds into each account **/
            checkingAccount.deposit(1.00);
            savingsAccount.deposit(1.00);
            maxiSavingsAccount.deposit(1.00);
            chin.openAccount(checkingAccount).openAccount(savingsAccount).openAccount(maxiSavingsAccount);

            //Assert that the statement outputs correctly
            Assert.AreEqual("Statement for Chin\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $1.00\n" +
                    "Total $1.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $1.00\n" +
                    "Total $1.00\n" +
                    "\n" +
                    "Maxi Savings Account\n" +
                    "  deposit $1.00\n" +
                    "Total $1.00\n" +
                    "\n" +
                    "Total In All Accounts $3.00", chin.getStatement());

        }

        [Test]
        public void CustomerTransfer()
        {
            /** Account objects to be used throughout the test **/
            checkingAccount = new Account(Account.CHECKING);
            savingsAccount = new Account(Account.SAVINGS);
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            chin = new Customer("Chin");

            /** Deposit funds into each account **/
            checkingAccount.deposit(1.00);
            savingsAccount.deposit(1.00);
            maxiSavingsAccount.deposit(1.00);
            chin.openAccount(checkingAccount).openAccount(savingsAccount).openAccount(maxiSavingsAccount);

            /** Transfer funds from checking to savings **/
            chin.transferFunds(ref checkingAccount, ref savingsAccount, 1.00);

            /** Assert that funds were transferred from checking to svings **/
            Assert.That(checkingAccount.sumTransactions().Equals(0.00));
            Assert.That(savingsAccount.sumTransactions().Equals(2.00));
        }

        //----THE TESTS BELOW WOULD HAVE BEEN IMPLEMENTED, BUT OUT OF TIME ...

        //BOUNDARY Condition Tests - edge conditions that do not fit typical input/output parameter(s) and/or result(s)

        //INVERSE Condition Tests - when conditions are reversed the outcome should still be valid

        //CROSS CHECK Tests - the end result can be traced back to the beginning values

        //FORCED ERROR Tests - keys obvious values are incorrect

        //PERFORMANCE Tests - stree test of the code (NOT USUALLY APPLICABLE TO NONE WEB HOSTED APPLICATIONS)
        //---For desktop machine performance will often hinge on the hardware running the application

        [TearDown]
        public void CleanUpTest()
        {
            //No unmanged test object(s) to garbage collect
        }

        [TestFixtureTearDown]
        public void CleanUpTestFixture()
        {
            //No unmanged test fixture object(s) to garbage collect
        }
    }
}