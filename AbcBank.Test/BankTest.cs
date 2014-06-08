using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        // private static readonly double DOUBLE_DELTA = 1e-15; no longer necessary, so REM-ed out

        [Test]
        public void testcustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }

        // Test added by jmk:
        [Test]
        public void testmulti_customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(john);

            Customer jack = new Customer("Jack");
            jack.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(jack);

            Assert.AreEqual("Customer Summary\n - John (1 account)\n - Jack (1 account)", bank.customerSummary());
        }

        // Test added by jmk:
        [Test]
        public void testmulti_customer_multi_account_Summary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            john.openAccount(new Account(Account.CHECKING));
            john.openAccount(new Account(Account.MAXI_SAVINGS));
            bank.addCustomer(john);

            Customer jack = new Customer("Jack");
            jack.openAccount(new Account(Account.CHECKING));
            jack.openAccount(new Account(Account.SAVINGS));
            bank.addCustomer(jack);

            Assert.AreEqual("Customer Summary\n - John (3 accounts)\n - Jack (2 accounts)", bank.customerSummary());
        }

        [Test]
        public void testcheckingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid()); // removed DOUBLE_DELTA
        }

        [Test]
        public void testsavings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid()); // removed DOUBLE_DELTA
        }

        // Test added by jmk:
        [Test]
        public void testopenCheckingAndSavingsAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            Account savingsAccount = new Account(Account.SAVINGS);
            bill.openAccount(savingsAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(1500.0);

            Assert.AreEqual(2.1, bank.totalInterestPaid()); // removed DOUBLE_DELTA
        }

        // Test added by jmk:
        [Test]
        public void testtransferBetweenCheckingAndSavingsAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            Account savingsAccount = new Account(Account.SAVINGS);
            bill.openAccount(savingsAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(1500.0);

            bill.transferFundsBetweenAccounts(50, checkingAccount, savingsAccount);

            Assert.GreaterOrEqual(savingsAccount.accountBalance(), 1550.0);
        }

        // Test added by jmk:
        [Test]
        public void testtransferBetweenCheckingAndSavingsAccountFAIL()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            Account savingsAccount = new Account(Account.SAVINGS);
            bill.openAccount(savingsAccount);
            bank.addCustomer(bill);

            // simulation of insufficient funds - nothing deposited within checking account

            savingsAccount.deposit(1500.0);

            try
            {
                bill.transferFundsBetweenAccounts(50, checkingAccount, savingsAccount);
                Assert.Fail();
            }
            catch (Exception e) { }
        }

        [Test]
        public void testmaxi_savings_account()
        {
            Bank bank = new Bank();
            Account maxi_savingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxi_savingsAccount));

            maxi_savingsAccount.deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid()); // removed DOUBLE_DELTA
        }

        // Test added by jmk:
        [Test]
        public void testAccount_misDeposit()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            try
            {
                checkingAccount.deposit(-5.0);
                Assert.Fail();
            }
            catch (Exception exp1) { }
        }

        // Test added by jmk:
        [Test]
        public void testAccount_misWithdraw()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            try
            {
                checkingAccount.withdraw(-5.0);
                Assert.Fail();
            }
            catch (Exception exp1) { }
        }

        // Test added by jmk:
        [Test]
        public void testWithdraw()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            bank.addCustomer(new Customer("Jeff").openAccount(checkingAccount));

            checkingAccount.deposit(3000.0);
            checkingAccount.withdraw(50.0);

            Assert.AreEqual(true, checkingAccount.checkWithdrawalsLast10days());
        }

    }
}
