using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;
        Bank bank;

        [Test]
        public void CustomerSummaryWithOneAccount()
        {
            bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary - Total Customers: 1\n - John (1 account)", bank.CustomerSummary());
        }

        [Test]
        public void CustomerSummaryWithPlurals()
        {
            bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.Checking));
            john.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(john);

            Customer brian = new Customer("Brian");
            brian.OpenAccount(new Account(AccountType.Savings));
            brian.OpenAccount(new Account(AccountType.Maxi_Savings));
            brian.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(brian);

            Assert.AreEqual("Customer Summary - Total Customers: 2\n - John (2 accounts)\n - Brian (3 accounts)", bank.CustomerSummary());
        }

        [Test]
        public void CheckingAccountInterestEarned()
        {
            bank = new Bank();
            Account account = new Account(AccountType.Checking);
            Customer bill = new Customer("Bill");
            bill.OpenAccount(account);
            bank.AddCustomer(bill);
            account.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void SavingsAccountInterestEarned()
        {
            bank = new Bank();
            Account account = new Account(AccountType.Savings);
            Customer bill = new Customer("Bill");
            bill.OpenAccount(account);
            bank.AddCustomer(bill);

            account.Deposit(1500.0);
            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);

            account.Withdraw(500.0);  //Test with amount accruing less interest
            Assert.AreEqual(1.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void MaxiSavingsAaccountInterestEarned()
        {
            bank = new Bank();
            Account account = new Account(AccountType.Maxi_Savings);
            Customer bill = new Customer("Bill");
            bill.OpenAccount(account);
            bank.AddCustomer(bill);

            account.Deposit(3000.0);
            Assert.AreEqual(3000.0 * 0.001, bank.TotalInterestPaid(), DOUBLE_DELTA);

            account.Deposit(2000.0);
            account.Transactions[1].Date -= TimeSpan.FromDays(11);
            Console.WriteLine(bank.TotalInterestPaid());
            Assert.AreEqual((3000.0 + 2000.0) * 0.05, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void BankTotalInterestPaid()
        {
            bank = new Bank();
            Account billAccount = new Account(AccountType.Checking);
            Customer bill = new Customer("Bill");
            bill.OpenAccount(billAccount);
            bank.AddCustomer(bill);
            billAccount.Deposit(100.0);

            Account johnAccount = new Account(AccountType.Savings);
            Customer john = new Customer("John");
            john.OpenAccount(johnAccount);
            bank.AddCustomer(john);

            johnAccount.Deposit(1500.0);
            //Test with amount accruing less interest
            johnAccount.Withdraw(500.0);

            Account oscarAccount = new Account(AccountType.Maxi_Savings);
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(oscarAccount);
            bank.AddCustomer(oscar);

            oscarAccount.Deposit(3000.0);
            oscarAccount.Deposit(2000.0);

            oscarAccount.Transactions[1].Date -= TimeSpan.FromDays(11);

            String report = "Total Interest Paid by Bank: " + (0.1 + 1.0 + (3000.0 + 2000.0) * 0.05).ToString();
            Console.WriteLine(report);
            Assert.AreEqual(report, bank.TotalInterestPaidReport());
        }
    }
}
