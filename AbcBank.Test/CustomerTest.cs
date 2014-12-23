using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {
            Account savingsAccount = AccountFactory.CreateAccount(AccountType.Savings); 
            Account superSavingsAccount = AccountFactory.CreateAccount(AccountType.Super_Savings);

            Customer henry = new Customer("Henry").openAccount(savingsAccount).openAccount(superSavingsAccount);

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
        public void OpenAccount_One_Test()
        {
            Customer oscar = new Customer("Oscar").openAccount(AccountFactory.CreateAccount(AccountType.Savings));//Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void OpenAccount_Two_Test()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(AccountFactory.CreateAccount(AccountType.Savings));//Account.SAVINGS));
            oscar.openAccount(AccountFactory.CreateAccount(AccountType.Savings));//Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Ignore]
        public void OpenAccount_Three_Test()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(AccountFactory.CreateAccount(AccountType.Savings));//Account.SAVINGS));
            oscar.openAccount(AccountFactory.CreateAccount(AccountType.Maxi_Savings));//Account.CHECKING));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }
        [Test]
        public void TransferFunds_lessThanAmount_Test()
        {
            Account savingsAccount = AccountFactory.CreateAccount(AccountType.Savings);
            Account maxiSavingsAccount = AccountFactory.CreateAccount(AccountType.Maxi_Savings);
            Customer oscar = new Customer("Oscar")
                    .openAccount(savingsAccount);//Account.SAVINGS));
            oscar.openAccount(maxiSavingsAccount);//Account.CHECKING));
            savingsAccount.deposit(8000);
            maxiSavingsAccount.deposit(100);
            
            Assert.IsTrue(oscar.TransferFunds(savingsAccount, maxiSavingsAccount, 100));
        }
        [Test]
        public void TransferFunds_greaterThanTotalAmount_Test()
        {
            try
            {
                Account savingsAccount = AccountFactory.CreateAccount(AccountType.Savings);
                Account maxiSavingsAccount = AccountFactory.CreateAccount(AccountType.Maxi_Savings);
                Customer oscar = new Customer("Oscar")
                        .openAccount(savingsAccount);//Account.SAVINGS));
                oscar.openAccount(maxiSavingsAccount);//Account.CHECKING));
                savingsAccount.deposit(8000);
                maxiSavingsAccount.deposit(100);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
            
        }
        [Test]
        public void CustomerCreate_Test()
        {
            Customer oscar = new Customer("Oscar");
            Assert.AreEqual(0, oscar.getNumberOfAccounts());
        }
    }
}
