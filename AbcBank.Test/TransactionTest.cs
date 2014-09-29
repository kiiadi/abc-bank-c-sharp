using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture, Description("Tests for Transaction class")]
    public class TransactionTest
    {
        [Test]
        public void Should_Have_Expected_Transaction_Date()
        {
            var date = new DateTime(2012, 9, 28);
            var transaction = new Transaction(date, 5);
            Assert.AreEqual(date, transaction.Date);
        }
        [Test]
        public void Should_Set_the_Expected_Transaction_Amount([Values(-1000, -0.99, 0.0d, 1.01, 100, 999.99, 1000, 10000.09)] double amount)
        {
            var transaction = new Transaction(DateTime.Now.Date, amount);
            Assert.AreEqual(amount, transaction.Amount, Helper.DOUBLE_DELTA);
        }
    }
}
