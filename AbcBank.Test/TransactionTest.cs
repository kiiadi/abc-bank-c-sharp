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
        public void checkAmount()
        {
            Transaction t = new Transaction(5);

            Assert.AreEqual(t.Amount, 5);
        }
        public void checkTransactionDate()
        {
            DateTime testDate = new DateTime(2014, 06, 29, 20, 30, 0);
            DateProvider.Instance.SetFixed(testDate);
            
            Transaction t = new Transaction(10);

            Assert.AreEqual(t.TransactionDate, testDate);
        }
    }
}
