using System;
using NUnit.Framework;
using AbcBank;

namespace AbcBank.Test
{
    [TestFixture]
    public class ManagerTest
    {
        Bank bank;

        [Test]
        public void CustomerSummaryWithOneAccount()
        {
            bank = new Bank();
            Manager manager = new Manager("Peter");
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(john);

            String result = "Customer Summary - Total Customers: 1 - Printed by Peter\n - John (1 account)";
            Console.WriteLine(result);

            Assert.AreEqual(result, manager.CustomerSummary());
        }

        [Test]
        public void CustomerSummaryWithPlurals()
        {
            bank = new Bank();
            Manager manager = new Manager("Peter");
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.Checking));
            john.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(john);

            Customer brian = new Customer("Brian");
            brian.OpenAccount(new Account(AccountType.Savings));
            brian.OpenAccount(new Account(AccountType.Maxi_Savings));
            brian.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(brian);

            String result = "Customer Summary - Total Customers: 2 - Printed by Peter\n - John (2 accounts)\n - Brian (3 accounts)";
            Console.WriteLine(result);

            Assert.AreEqual(result, manager.CustomerSummary());
        }

        [Test]
        public void BankTotalInterestPaid()
        {
            bank = new Bank();
            Manager manager = new Manager("Peter");
            Account billAccount = new Account(AccountType.Checking);
            Customer bill = new Customer("Bill");
            bill.OpenAccount(billAccount);
            bank.AddCustomer(bill);
            billAccount.Deposit(100.0);
            billAccount.Transactions[0].Date -= TimeSpan.FromDays(2);
            double billTotal = 100 * 0.001 * 1 / 365 * 2;

            Account johnAccount = new Account(AccountType.Savings);
            Customer john = new Customer("John");
            john.OpenAccount(johnAccount);
            bank.AddCustomer(john);
            johnAccount.Deposit(1500.0);
            johnAccount.Transactions[0].Date -= TimeSpan.FromDays(5);
            johnAccount.Withdraw(500.0);
            johnAccount.Transactions[1].Date -= TimeSpan.FromDays(2);
            double johnTotal = (1000 * 0.001 * 1 / 365 + 500 * 0.002 * 1 / 365) * 3 + 1000 * 0.001 * 1 / 365 * 2;

            Account oscarAccount = new Account(AccountType.Maxi_Savings);
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(oscarAccount);
            bank.AddCustomer(oscar);

            oscarAccount.Deposit(3000.0);
            oscarAccount.Transactions[0].Date -= TimeSpan.FromDays(30);
            oscarAccount.Withdraw(1100.0);
            oscarAccount.Transactions[1].Date -= TimeSpan.FromDays(27);
            oscarAccount.Withdraw(400.0);
            oscarAccount.Transactions[2].Date -= TimeSpan.FromDays(12);
            oscarAccount.Deposit(1000.0);
            oscarAccount.Transactions[3].Date -= TimeSpan.FromDays(8);
            double oscarTotal =
                   (3000.0 * 0.05 * 1 / 365) * (30 - 27)
                 + (1900.0 * 0.001 * 1 / 365) * 10 + (1900.0 * 0.05 * 1 / 365) * (27 - 12 - 10)
                 + (1500.0 * 0.001 * 1 / 365) * (12 - 8)
                 + (2500.0 * 0.001 * 1 / 365) * (10 - (12 - 8)) + (2500.0 * 0.05 * 1 / 365) * (8 - (10 - (12 - 8)));

            double total = billTotal + johnTotal + oscarTotal;
            String report = "Total Interest Paid by Bank: " + Utility.ToDollars(total) + " - Printed by Peter";
            Console.WriteLine(report);

            Assert.AreEqual(report, manager.TotalInterestPaidReport());
        }
    }
}
