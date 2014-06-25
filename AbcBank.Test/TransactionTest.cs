using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    // Using naming convention <Method-Name Under Test>_<Scenario>_<Expected-Outcome>
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void Transaction_Constructor_InstanceCreated()
        {
            DateTime time = DateTime.UtcNow;
            Transaction t = new Transaction(5, Transaction.transactionType.DEPOSIT, time);
            Assert.AreEqual(true, t is Transaction);
            Assert.AreEqual(5.0, t.Amount);
            Assert.AreEqual(t.UtcDate, time);
         }
    }
}
