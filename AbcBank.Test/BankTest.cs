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
      
        [Test]
        public void Should_Add_New_Customer()
        {
            var bank = new Bank();
            var customer  = new Customer("John");
            bank.AddCustomer(customer);
            Assert.IsTrue(bank.Customers.Contains(customer));
        }
        [Test]
        [ExpectedException(ExpectedMessage = "Customer already exists")]
        public void Should_Raise_CustomerAlreadyAddedException()
        {
            var bank = new Bank();
            var customer = new Customer("John");
            bank.AddCustomer(customer);
            bank.AddCustomer(customer);
        }

        [Test]
        public void Should_Get_Summary_With_No_Customers()
        {
            var bank = new Bank();
            Assert.AreEqual("Customer Summary", bank.GetCustomerSummary());
        }
        [Test]
        public void Should_Get_Summary_For_One_Customer_With_No_Account()
        {
            var bank = new Bank();
            var customer = new Customer("John");
            bank.AddCustomer(customer);
            Assert.AreEqual("Customer Summary\n - John (no accounts)", bank.GetCustomerSummary());
        }

        [Test]
        public void Should_Get_Summary_For_One_Customer_With_One_Account()
        {
            var bank = new Bank();
            var customer = new Customer("Bill");
            bank.AddCustomer(customer);
            customer.AddAccount(new Account(AccountType.Checking));
            Assert.AreEqual("Customer Summary\n - Bill (1 account)", bank.GetCustomerSummary());
        }
        [Test]
        public void Should_Get_Summary_For_One_Customer_With_Two_Accounts()
        {
            var bank = new Bank();
            var customer = new Customer("Richard");
            bank.AddCustomer(customer);
            customer.AddAccount(new Account(AccountType.Checking));
            customer.AddAccount(new Account(AccountType.Checking));
            Assert.AreEqual("Customer Summary\n - Richard (2 accounts)", bank.GetCustomerSummary());
        }
        [Test]
        public void Should_Get_Summary_For_One_Customer_With_Three_Accounts()
        {
            var bank = new Bank();
            var customer = new Customer("George");
            bank.AddCustomer(customer);
            customer.AddAccount(new Account(AccountType.Checking));
            customer.AddAccount(new Account(AccountType.Savings));
            customer.AddAccount(new Account(AccountType.MaxiSavings));
            Assert.AreEqual("Customer Summary\n - George (3 accounts)", bank.GetCustomerSummary());
        }
        [Test]
        public void Should_Get_Summary_For_Two_Customers_With_No_Accounts()
        {
            var bank = new Bank();
            bank.AddCustomer(new Customer("Maria"));
            bank.AddCustomer(new Customer("Ellen"));
            Assert.AreEqual("Customer Summary\n - Maria (no accounts)\n - Ellen (no accounts)", bank.GetCustomerSummary());
        }
        [Test]
        [Ignore]
        public void Should_GetTotalPaidInterest()
        {
            var bank = new Bank();
            var maria = new Customer("Maria");
            var mariaChecking = new Account
           (
               AccountType.Checking,
               new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-200), 100000), 
                    new Transaction(DateTime.Today.AddDays(-190), 10000), 
                    new Transaction(DateTime.Today.AddDays(-150), -5000), 
                    new Transaction(DateTime.Today.AddDays(-100), 1000)
                }
          );

            maria.AddAccount(mariaChecking);

            var mariaSavings = new Account
          (
              AccountType.Savings,
              new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-200), 15000)
                }
         );

            maria.AddAccount(mariaSavings);
            bank.AddCustomer(maria);

            var ellen = new Customer("Ellen");
            ellen.AddAccount(new Account
          (
              AccountType.MaxiSavings,
              new Transaction[]
                {
                    new Transaction(DateTime.Today.AddDays(-20), 15000)
                }
         ));
            bank.AddCustomer(ellen);


            var paid = bank.GetTotalPaidInterest();
            Assert.Fail("Should figure out the expected amount");
        }
    }
}
