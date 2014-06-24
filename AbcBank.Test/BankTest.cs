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
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            Assert.AreEqual("Customer Summary", bank.customerSummary());
            john.openAccount(new Account(Account.AccountType.CHECKING));
            bank.addCustomer(john);
            john.openAccount(new Account(Account.AccountType.SAVINGS));
            john.openAccount(new Account(Account.AccountType.MAXI_SAVINGS));
            Assert.AreEqual("Customer Summary\n - John (3 accounts)", bank.customerSummary());
            Customer bill = new Customer("Bill");
            bill.openAccount(new Account(Account.AccountType.MAXI_SAVINGS));
            bank.addCustomer(bill);
            Assert.AreEqual("Customer Summary\n - John (3 accounts)\n - Bill (1 account)", bank.customerSummary());
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);
            Assert.AreEqual(checkingAccount.Type, Account.AccountType.CHECKING);
            Assert.Throws(typeof(ArgumentException), ()=>{checkingAccount.deposit(-100.00); });
            Assert.Throws(typeof(ArgumentException), ()=>{ checkingAccount.withdraw(-100.00); });
            checkingAccount.deposit(100.0);
            Assert.AreEqual(checkingAccount.sumTransactions(), 100.0);
            checkingAccount.withdraw(50.0);
            Assert.AreEqual(checkingAccount.sumTransactions(), 50.0);
            Assert.AreEqual(0.05, bank.totalInterestPaid(), DOUBLE_DELTA);
            checkingAccount.deposit(2000.0);
            Assert.AreEqual(2.05, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
            Customer bill = new Customer("Bill").openAccount(savingsAccount);
            bank.addCustomer(bill);
            Assert.AreEqual(savingsAccount.Type, Account.AccountType.SAVINGS);
            Assert.Throws(typeof(ArgumentException), () => { savingsAccount.deposit(-100.00); });
            Assert.Throws(typeof(ArgumentException), () => { savingsAccount.withdraw(-100.00); });
            savingsAccount.deposit(100.0);
            Assert.AreEqual(savingsAccount.sumTransactions(), 100.0);
            savingsAccount.withdraw(50.0);
            Assert.AreEqual(savingsAccount.sumTransactions(), 50.0);
            Assert.AreEqual(0.05, bank.totalInterestPaid(), DOUBLE_DELTA);
            savingsAccount.deposit(2000.0);
            Assert.AreEqual(3.1, bank.totalInterestPaid(), DOUBLE_DELTA);

        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account maxiSavings = new Account(Account.AccountType.MAXI_SAVINGS);
            Customer bill = new Customer("Bill").openAccount(maxiSavings);
            bank.addCustomer(bill);
            Assert.AreEqual(maxiSavings.Type, Account.AccountType.MAXI_SAVINGS);
            Assert.Throws(typeof(ArgumentException), () => { maxiSavings.deposit(-100.00); });
            Assert.Throws(typeof(ArgumentException), () => { maxiSavings.withdraw(-100.00); });
            maxiSavings.deposit(100.0);
            Assert.AreEqual(maxiSavings.sumTransactions(), 100.0);
            maxiSavings.withdraw(50.0);
            Assert.AreEqual(maxiSavings.sumTransactions(), 50.0);
            Assert.AreEqual(1.0, bank.totalInterestPaid(), DOUBLE_DELTA);
            maxiSavings.deposit(2000.0);
            Assert.AreEqual(75.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
