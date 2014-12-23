using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
      [Test]
        public void deposit_greaterthanzero_test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Savings); 
            account.deposit(100);
            Assert.AreEqual(100.0, account.sumTransactions());
           
        }
        [Test]
        public void deposit_lessthanzero_test()
        {
            try{
              Account account = AccountFactory.CreateAccount(AccountType.Savings); 
              account.deposit(-2);
            }
            catch(Exception ex){
                Assert.IsTrue(ex is ArgumentException);
            }
        }
        
        [Test]
        public void getAccountTypeTest()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Savings);
           
            string expected = "Savings Account"; 
            string actual;
            actual = account.getAccountType();
            Assert.AreEqual(expected, actual);
          
        }

        [Test]
        public void interestEarned_savingsAccount_Test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Savings); 
            account.deposit(2000);
            Assert.AreEqual(3, account.interestEarned());
        }
        [Test]
        public void interestEarned_superSavingsAccount_Test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Super_Savings); 
            account.deposit(2000);
            Assert.AreEqual(70, account.interestEarned());
        }
        [Test]
        public void interestEarned_maxiSavingsAccount_Test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Maxi_Savings); 
            account.deposit(2000);
            Assert.AreEqual(70, account.interestEarned());
        }
       
        [Test]
        public void interestEarned_checkingAccount_Test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Checking); 
            account.deposit(2000.90);
            Assert.AreEqual(20.009, account.interestEarned());
        }
       
        [Test]
        public void withdraw_lessThanTotalAmount_test()
        {
            Account account = AccountFactory.CreateAccount(AccountType.Maxi_Savings); 

            account.deposit(1000);
            account.withdraw(100);
            Assert.AreEqual(900.0, account.sumTransactions());
        }
        [Test]
        public void withdraw_greaterThanTotalAmount_test()
        {
            try
            {
                Account account = AccountFactory.CreateAccount(AccountType.Savings); 
                account.deposit(500);
                account.withdraw(510);

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }
    }
}
