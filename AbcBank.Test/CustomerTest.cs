using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class When_customer_opens_accounts
    {
         
        [Test]
        public void it_should_return_total_of_one_account()
        {
            Customer oscar = new Customer("Oscar");
            oscar.openAccount(new Account(AccountType.Savings, ""));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void it_should_return_total_of_two_accounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.openAccount(new Account(AccountType.Savings, ""));
            oscar.openAccount(new Account(AccountType.Checking, ""));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }
    }
    
    [TestFixture]
    public class When_customer_deposits_or_withdraws_funds_from_account
    {
        [Test]
        public void it_should_deposit_the_right_amount()
        {
            var savings = new Account(AccountType.Savings, "sav-123");
            var checking = new Account(AccountType.Checking, "chk-321");
            var oscar = new Customer("Oscar").
                openAccount(savings).
                openAccount(checking);

            oscar.deposit("sav-123", 42.0);
            oscar.deposit("chk-321", 22.0);
            Assert.AreEqual(42, savings.sumTransactions());
            Assert.AreEqual(22, checking.sumTransactions());
        }

        [Test]
        public void it_should_withdraw_the_right_amount()
        {
            var savings = new Account(AccountType.Savings, "sav");
            var checking = new Account(AccountType.Checking, "chk");
            var bill = new Customer("Bill").
                openAccount(checking).
                openAccount(savings);

            bill.withdraw("sav", 80.0);
            bill.withdraw("sav", 20.0);
            bill.withdraw("chk", 50.0);
            
            Assert.AreEqual(-100.0, savings.sumTransactions());
            Assert.AreEqual(-50.0, checking.sumTransactions());
        }

        [Test]
        public void it_should_deposit_to_savings()
        {
            var savings = new Account(AccountType.Savings, "sav-123");
            var oscar = new TestableCustomer("Oscar");
            oscar.FindFunc = delegate { return savings; };

            oscar.deposit("sav", 42.0);
            oscar.deposit("sav", 22.0);
            Assert.AreEqual(64, savings.sumTransactions());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void it_should_throw_argument_exception_for_invalid_account_numbers()
        {
            var oscar = new Customer("Oscar").openAccount(new Account(AccountType.Checking, "checking"));

            oscar.withdraw("other_checking", 20.0);
        }
    }

    public class TestableCustomer : Customer
    {
        public Func<string, IAccount> FindFunc { get; set; }

        public TestableCustomer(string name)
            : base(name)
        {
            FindFunc = base.findAccount;
        }

        public override IAccount findAccount(string accountName)
        {
            return FindFunc(accountName);
        }
    }
}
