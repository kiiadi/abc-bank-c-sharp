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
        public void Test_GetStatement()
        {   
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
                        
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n\n  deposit $100.00\nTotal $100.00\n\n  deposit $4,000.00\n  withdrawal $200.00\nTotal $3,800.00\n\nTotal In All Accounts $3,900.00", henry.GetStatement());
        }

        [Test]
        public void Test_GetStatement_ZeroAccounts()
        {
            Customer henry = new Customer("Henry");
            Assert.AreEqual("Statement for Henry\n\nTotal In All Accounts $0.00", henry.GetStatement());
        }

        [Test]
        public void Test_OneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingsAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [Test]
        public void Test_TwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }


        [Ignore]
        public void TestThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [Test]
        public void Test_TransferFunds()
        {
            Customer customer = new Customer("Jim");
            SavingsAccount svgsAccount = new SavingsAccount();
            svgsAccount.Deposit(100);
            customer.OpenAccount(svgsAccount);          
  
            CheckingAccount chkAccount = new CheckingAccount();
            customer.OpenAccount(chkAccount);

            customer.TransferFunds(svgsAccount, chkAccount, 100);

            Assert.AreEqual(100, chkAccount.SumTransactions());
        }

        [Test]
        [ExpectedException("System.Exception")]
        public void Test_TransferFundsException()
        {
            Customer customer = new Customer("Jim");
            SavingsAccount svgsAccount = new SavingsAccount();            
            customer.OpenAccount(svgsAccount);

            CheckingAccount chkAccount = new CheckingAccount();
            customer.OpenAccount(chkAccount);

            customer.TransferFunds(svgsAccount, chkAccount, 100);            

            Assert.AreEqual(100, chkAccount.SumTransactions());
        }
    }

}
