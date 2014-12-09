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
    public class CheckingAccountTest
    {
        [Test]
        public void IsCheckingAccountType()
        {
            var account = new CheckingAccount();


            Assert.AreEqual(AccountType.Checking, account.getAccountType());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void ExceptionThrown_WhenDepositing_AndAmountIsInvalid()
        {
            var account = new CheckingAccount();
            account.deposit(-5);
        }

        [Test]
        public void ValidAmount_IsDeposited_AndNoException_IsThrown()
        {
            var account = new CheckingAccount();
    

            Assert.DoesNotThrow(()=> account.deposit(120));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void ExceptionThrown_WhenWithDrawing_AndWhenAmountIsInvalid()
        {
            var account = new CheckingAccount();
            account.withdraw(-5);
        }

        [Test]
        public void ValidAmount_IsWithDrawn_AndNoException_IsThrown()
        {
            var account = new CheckingAccount();
            account.setAccountBalance(121);

            Assert.DoesNotThrow(() => account.withdraw(120));
        }

        [Test]
        public void ValidAmount_IsWithDrawn_AndAccountBalanceisUpdated_AndNoException_IsThrown()
        {
            var account = new CheckingAccount();
            account.setAccountBalance(121);


            Assert.DoesNotThrow(() => account.withdraw(120));
            Assert.AreEqual(1, account.getAccountBalance());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Insufficient funds for withdrawal")]
        public void ExcessAmount_IsWithDrawn_AndException_IsThrown()
        {
            var account = new CheckingAccount();
            account.setAccountBalance(56);
            account.withdraw(121);
        }
    }
}
