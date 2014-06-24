using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    // Using naming convention <Method-Name Under Test>_<Scenario>_<Expected-Outcome>
    [TestFixture]
    public class CustomerTest
    {

        [Test]
        public void CustomerConstructor_CreateCustomer_Instance()
        {
            Customer c = new Customer("Mike");
            Assert.AreEqual(c.Name,"Mike");
            Assert.NotNull(c.Accounts);
        }

        [Test]
        public void openAccount_CreateCustomerWithAccount_Account()
        {
            Customer c = new Customer("Mike");
            Account account = new Account(Account.AccountType.CHECKING);
            c.openAccount(account);
            Assert.AreEqual(c.Accounts.Count(), 1);
            Assert.NotNull(c.Accounts.Where(a => a.Type == Account.AccountType.CHECKING).FirstOrDefault());
        }

        [Test]
        public void openAccount_CreateCustomerWithSameAccountTypeTwice_Exception()
        {
            Customer c = new Customer("Mike");
            Account account = new Account(Account.AccountType.CHECKING);
            c.openAccount(account);
            account = new Account(Account.AccountType.CHECKING);
            Assert.Throws(typeof(Exception), ()=>{c.openAccount(account);});
        }

        [Test]
        public void totalInterestEarned_CreateCustomerWithAccounts_InterestIsEarned()
        {
            Customer c = new Customer("Mike");
            Account account = new Account(Account.AccountType.CHECKING);
            c.openAccount(account);
            account.deposit(500);
            account = new Account(Account.AccountType.SAVINGS);
            c.openAccount(account);
            account.deposit(500);
            account.deposit(1000);
            Assert.AreEqual(c.totalInterestEarned(), 2.5);
        }

        [Test] 
        public void getStatement_CreateCustomerWithAccount_Statement()
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
        public void transferFunds_TransferBetweenTwoCustomerAccounts_AccountsDebitedAndCredited()
        {
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);
            checkingAccount.deposit(500.0);
            savingsAccount.deposit(500.0);
            henry.transferFunds(Account.AccountType.CHECKING, Account.AccountType.SAVINGS, 200);
            Assert.AreEqual(checkingAccount.sumTransactions(), 300);
            Assert.AreEqual(savingsAccount.sumTransactions(), 700);
        }
    }
}
