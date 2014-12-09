using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.AccountManager;

namespace AbcBank.Test
{
    [TestFixture]
    public class MaxiSavingsAccountTest
    {
        [Test]
        public void IsMaxiSavingsAccountType()
        {
            var account = new MaxiSavingsAccount();


            Assert.AreEqual(AccountType.MaxiSavings, account.getAccountType());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void ExceptionThrown_WhenDepositing_AndAmountIsInvalid()
        {
            var account = new MaxiSavingsAccount();
            account.deposit(-5);
        }

        [Test]
        public void ValidAmount_IsDeposited_AndNoException_IsThrown()
        {
            var account = new MaxiSavingsAccount();
    

            Assert.DoesNotThrow(()=> account.deposit(120));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void ExceptionThrown_WhenWithDrawing_AndWhenAmountIsInvalid()
        {
            var account = new MaxiSavingsAccount();
            account.withdraw(-5);
        }

        [Test]
        public void ValidAmount_IsWithDrawn_AndNoException_IsThrown()
        {
            var account = new MaxiSavingsAccount();
            account.setAccountBalance(121);

            Assert.DoesNotThrow(() => account.withdraw(120));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Insufficient funds for withdrawal")]
        public void ExcessAmount_IsWithDrawn_AndException_IsThrown()
        {
            var account = new MaxiSavingsAccount();
            account.setAccountBalance(56);
            account.withdraw(121);
        }
    }
}
