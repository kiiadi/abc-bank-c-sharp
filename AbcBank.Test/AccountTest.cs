using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        public void Test_Deposit()
        {
            SavingsAccount svgAccount = new SavingsAccount();
            svgAccount.Deposit(100);
            Assert.AreEqual(1, svgAccount.transactions.Count);
        }

        [Test]
        [ExpectedException]
        public void Test_DepositNegativeAmount()
        {
            SavingsAccount svgAccount = new SavingsAccount();
            svgAccount.Deposit(-100);
            Assert.AreEqual(1, svgAccount.transactions.Count);
        }
        
        [Test]        
        public void Test_Withdraw()
        {
            SavingsAccount svgAccount = new SavingsAccount();
            svgAccount.Deposit(100);

            svgAccount.Withdraw(100);
            Assert.AreEqual(2, svgAccount.transactions.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount to withdraw is more than your current balance")]
        public void Test_WithdrawMoreThanBalance()
        {
            SavingsAccount svgAccount = new SavingsAccount();
            svgAccount.Withdraw(100);
            Assert.AreEqual(1, svgAccount.transactions.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void Test_WithdrawNegativeAmount()
        {
            SavingsAccount svgAccount = new SavingsAccount();
            svgAccount.Withdraw(-100);
            Assert.AreEqual(1, svgAccount.transactions.Count);
        }

        [Test]
        public void Test_SumTransactions()
        {
            SavingsAccount acc1 = new SavingsAccount();
            acc1.Deposit(100);            
            acc1.Deposit(200);

            Assert.AreEqual(300, acc1.SumTransactions());
        }
    }
}
