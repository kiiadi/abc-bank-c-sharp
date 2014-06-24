using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void transaction()
        {
            Transaction t = new Transaction(5);
            Assert.AreEqual(true, t is Transaction);
            Assert.AreEqual(5.0, t.Amount);
            t = new Transaction(-5);
            Assert.AreEqual(-5.0, t.Amount);
            // Assumption is the suite of tests will complete in under an hour and therefore
            // year, month and hour will be same as that of the transaction, the minute and second portions of the time may not.
            Assert.AreEqual(DateTime.UtcNow.Year, t.UtcDate.Year);
            Assert.AreEqual(DateTime.UtcNow.Month, t.UtcDate.Month);
            Assert.AreEqual(DateTime.UtcNow.Hour, t.UtcDate.Hour);
        }
    }
}
