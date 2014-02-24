using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class When_bank_manager_asks_for_reports
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void bank_should_provide_customer_summary_report()
        {
            Bank bank = new Bank();
            var john = new Customer("John").openAccount(new Account(AccountType.Checking, ""));
            var bill = new Customer("Bill")
                .openAccount(new Account(AccountType.Savings, ""))
                .openAccount(new Account(AccountType.Checking, ""));

            bank.addCustomer(john);
            bank.addCustomer(bill);

            var result = new BankReports().customerSummary(bank.getCustomers());
            Assert.AreEqual("Customer Summary\n - John (1 account)\n - Bill (2 accounts)", result);
        }

        [Test]
        public void bank_should_return_total_interest_paid_for_checking_accounts()
        {
            Bank bank = new Bank();
            var bill = new Customer("Bill").openAccount( new Account(AccountType.Checking, "checking"));
            var jill = new Customer("Jill").openAccount(new Account(AccountType.Checking, "checking1"));


            bank.addCustomer(bill);
            bank.addCustomer(jill);

            bill.deposit("checking", 100.0);
            jill.deposit("checking1", 100.0);

            Assert.AreEqual(0.2, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void bank_should_return_total_interest_paid_for_savings_accounts()
        {
            Bank bank = new Bank();
            var bill = new Customer("Bill").openAccount(new Account(AccountType.Savings, "sav"));
            bank.addCustomer(bill);

            bill.deposit("sav", 1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void bank_should_return_total_interest_paid_for_maxi_savings_accounts()
        {
            Bank bank = new Bank();
            var bill = new Customer("Bill").openAccount(new Account(AccountType.MaxiSavings, "maxsav"));
            bank.addCustomer(bill);

            bill.deposit("maxsav", 3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
