using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
         /** Pull unit test variables from a configuration file **/
        static NameValueCollection testSettings;

        /** Account object to be used throughtout the test **/
        Account checkingAccount = new Account(Account.CHECKING);
        Account savingsAccount = new Account(Account.SAVINGS);
        Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

        /** Returned the value of the passed configuration file key **/
        public string GetSettings(string key)
        {
            return testSettings[key];
        }

        /** Construct a customer test object **/
        public BankTest()
        {
            /** Account object to be used throughtout the test **/
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            /** Initialize the testSettings object to the collection of configuration file key/value pairs **/
            testSettings = ConfigurationManager.AppSettings as NameValueCollection;
        }

        //BASIC Condition Tests - expected conditions that fit typical input/output parameter(s) and/or result(s)
        [Test]
        public void BankObjectCreation()
        {
            /** Bank object to be used throughout the test **/
            Bank okoronkwoBank = new Bank("Okoronkwo");

            /** Create customer and opewn accounts **/
            Customer chin = new Customer("Chin");
            chin.openAccount(checkingAccount).openAccount(savingsAccount).openAccount(maxiSavingsAccount);

            /** Add a cusomer to the bank **/
            okoronkwoBank.addCustomer(chin);

            /**Assert that the customer has been added **/
            Assert.AreEqual("Customer Summary\n - Chin (3 accounts)", okoronkwoBank.customerSummary());
        }

        [Test]
        public void BankInterestPaid()
        {
            /** Bank object to be used throughout the test **/
            Bank okoronkwoBank = new Bank("Okoronkwo");

            /** Create customer and opewn accounts **/
            Customer chin = new Customer("Chin");
            checkingAccount.deposit(1.00);
            savingsAccount.deposit(1.00);
            maxiSavingsAccount.deposit(1.00);
            chin.openAccount(checkingAccount).openAccount(savingsAccount).openAccount(maxiSavingsAccount);

            /** Add a cusomer to the bank **/
            okoronkwoBank.addCustomer(chin);

            /**Assert that the interest paid is being correctly calculated **/
            Assert.That(okoronkwoBank.totalInterestPaid().Equals(((1 * .001 * 365) + (1 * .001 * 365) + (1 * .05 * 365))));
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
