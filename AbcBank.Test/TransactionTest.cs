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
        }

        [Test] // Test added by jmk:
        public void testAmountIsDouble()
        {
            Transaction t = new Transaction(5);
            Assert.AreEqual(true, t.getAmount() is Double);
        }

        [Test] // Test added by jmk:
        public void testTransactionDateisDateTime()
        {
            Transaction t = new Transaction(5);
            Assert.AreEqual(true, t.getTransactionDate() is DateTime);
        }

    }
}
