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

            CheckingAccount checkingAccount = new CheckingAccount();
            SavingsAccount savingsAccount = new  SavingsAccount();

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.deposit(300.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);
            henry.TransferFunds(checkingAccount, savingsAccount, 200.00);
            henry.GetStatement();

            //Assert.AreEqual("Statement for Henry" + Environment.NewLine+
            //                "Checking Account"+
            //                "  deposit $300.00" +  Environment.NewLine+
            //                "  withdrawal $200.00" +      Environment.NewLine+         
            //                "Total $100.00" +  Environment.NewLine+
            //                "Saving Account" +
            //                "  deposit $4,000.00" + Environment.NewLine +
            //                "  withdrawal $200.00" + Environment.NewLine +
            //                "  deposit $200.00" + Environment.NewLine +
            //                "Total $4,000.00" + Environment.NewLine +
            //                "Total In All Accounts $4,100.00", henry.GetStatement());
            Assert.AreEqual("$4,100.00", henry.GetStatement());


        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingsAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [Ignore]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
