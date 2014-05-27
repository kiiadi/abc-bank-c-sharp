using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using System.Xml;

namespace AbcBank.Test
{
    [TestFixture]

    class AccountTest
    {
        /** Pull unit test variables from a configuration file - app.config **/
        static NameValueCollection testSettings;

        /** Returned the value of the passed configuration file key **/
        public string GetSettings(string key)
        {
            return testSettings[key];
        }

        /** Construct a account test object **/
        public AccountTest()
        {
            /** Initialize the testSettings object to the collection of configuration file key/value pairs **/
            testSettings = ConfigurationManager.AppSettings as NameValueCollection;
        }

        /** Checking Account object to be used for all the unit testing below **/
        Account checkingAccount = new Account(Account.CHECKING);

        /** Savings Account object to be used for all the unit testing below **/
        Account savingsAccount = new Account(Account.SAVINGS);

        /** Maxi_Savings Account object to be used for all the unit testing below **/
        Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
        
        //BASIC Condition Tests - expected conditions that fit typical input/output parameter(s) and/or result(s)
        [Test] // AccountObjectCreation1
        public void AccountTestA()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            //Assert that each object is initialized correctly
            Assert.AreEqual(0, checkingAccount.getAccountType());
            Assert.AreEqual(1, savingsAccount.getAccountType());
            Assert.AreEqual(2, maxiSavingsAccount.getAccountType());
        }

        [Test] // AccountTransactions1
        public void AccountTansactions1()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Perform a few account transactions for each category of account objects **/
            checkingAccount.deposit(100.00);
            savingsAccount.deposit(100.00);
            maxiSavingsAccount.deposit(100.00);

            checkingAccount.withdraw(50.00);
            savingsAccount.withdraw(50.00);
            maxiSavingsAccount.withdraw(50.00);

            /** Assert that transactions are being recorded correctly **/
            Assert.AreEqual(2, checkingAccount.transactions.Count);
            Assert.AreEqual(2, savingsAccount.transactions.Count);
            Assert.AreEqual(2, maxiSavingsAccount.transactions.Count);

            /** Assert that transaction calculations are correct **/
            Assert.AreEqual(50, checkingAccount.sumTransactions());
            Assert.AreEqual(50, savingsAccount.sumTransactions());
            Assert.AreEqual(50, maxiSavingsAccount.sumTransactions());
        }

        [Test] // AccountInterest1
        public void AccountInterest1()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Perform a few account transactions for each category of account objects **/
            checkingAccount.deposit(50.00);
            savingsAccount.deposit(1050.00);
            maxiSavingsAccount.deposit(2050.00);

            /**Assert that interest rate calculations are correct **/
            Assert.That(checkingAccount.interestEarned().Equals((50*.001)*365));
            Assert.That(savingsAccount.interestEarned().Equals(((1000.00 * .001) + 50.00) * .002 * 365));
            //Assert.That(maxiSavingsAccount.interestEarned().Equals(((1000.00 * .02) + (1000.00*.05) + 50.00) * .1 * 365)); - No longer valid. See AccountWithHighInterest() test.
        }

        //BOUNDARY Condition Tests - edge conditions that do not fit typical input/output parameter(s) and/or result(s)
        [Test] 
        public void AccountObjectCreation2()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Assert that attempting to create a account using a non-defined  numerical representation should fail **/
            var ex1 = Assert.Throws<ArgumentException>(() => new Account(Convert.ToInt32(GetSettings("badAccountType1"))));
            Assert.That(ex1.Message, Is.EqualTo("Error: The account type id must be a value between 0 and 2"));
            
            /** Savings Account object to be used for all the unit testing below **/
            var ex2 = Assert.Throws<ArgumentException>(() => new Account(Convert.ToInt32(GetSettings("badAccountType2"))));
            Assert.That(ex2.Message, Is.EqualTo("Error: The account type id must be a value between 0 and 2"));

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            var ex3 = Assert.Throws<ArgumentException>(() => new Account(Convert.ToInt32(GetSettings("badAccountType3"))));
            Assert.That(ex2.Message, Is.EqualTo("Error: The account type id must be a value between 0 and 2"));
        }

        [Test]
        public void AccountTansactions2()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Perform a few account transactions for each category of account objects **/
            checkingAccount.deposit(1.00);
            savingsAccount.deposit(1.00);
            maxiSavingsAccount.deposit(1.00);

            /** Assert that invalid withdrawal amount will throw and exception **/
            var ex1 = Assert.Throws<ArgumentException>(() => checkingAccount.withdraw(Convert.ToInt32(GetSettings("badWithdrawalAmount1"))));
            Assert.That(ex1.Message, Is.EqualTo("Error: Insufficient funds available for the desired withdrawal amount"));

            var ex2 = Assert.Throws<ArgumentException>(() => savingsAccount.withdraw(Convert.ToInt32(GetSettings("badWithdrawalAmount2"))));
            Assert.That(ex2.Message, Is.EqualTo("Error: Insufficient funds available for the desired withdrawal amount"));

            var ex3 = Assert.Throws<ArgumentException>(() => maxiSavingsAccount.withdraw(Convert.ToInt32(GetSettings("badWithdrawalAmount3"))));
            Assert.That(ex2.Message, Is.EqualTo("Error: Insufficient funds available for the desired withdrawal amount"));

        }

        [Test]
        public void AccountWithHighInterest()
        {
            /** Checking Account object to be used for all the unit testing below **/
            checkingAccount = new Account(Account.CHECKING);

            /** Savings Account object to be used for all the unit testing below **/
            savingsAccount = new Account(Account.SAVINGS);

            /** Maxi_Savings Account object to be used for all the unit testing below **/
            maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Create a date object set to January 1, 2014 **/
            DateTime date = new DateTime(2014,01,01);

            /** Deposit funds into current/instant account object using date from January 1, 2014 **/
            maxiSavingsAccount.testDeposit(1000.00, date);

            /** Assert that this account is a high interest earning account **/
            Assert.That(maxiSavingsAccount.interestEarned().Equals(1000.00*0.05*365));

            /** Create a date object set to today (now) **/
            date = DateTime.Now;

            /** Deposit funds into current/instant account object using date from today **/
            //maxiSavingsAccount.testDeposit(1000.00, date);
            maxiSavingsAccount.withdraw(1.00);

            /** Assert that this account is not a high interest earning account - because of today's withdrawal**/
            Assert.That(maxiSavingsAccount.interestEarned().Equals(999.00*0.001*365));
        }

        //----THE TESTS BELOW WOULD HAVE BEEN IMPLEMENTED, BUT OUT OF TIME ...

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
