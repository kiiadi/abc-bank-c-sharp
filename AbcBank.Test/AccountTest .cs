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
    public class AccountTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "amount must be greater than zero")]
        public void ExceptionIsThrown_WhenAmountIsIvalid_WhenDepositting()
        {
            var account = new CheckingAccount();

            account.deposit(-5);
        }

    }
}
