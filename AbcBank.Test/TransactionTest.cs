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
        public void TestCreateTransaction()
        {
            DummyDateProvider.SetNow("1/1/2001");

            Transaction t = new Transaction(5, DummyDateProvider.GetInstance().Now());
            Assert.AreEqual(true, t is Transaction);
            Assert.AreEqual("1/1/2001", DummyDateProvider.GetInstance().Now().ToShortDateString());
        }

    }
}
