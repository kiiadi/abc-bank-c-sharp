using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture]
    public class DateProviderTest
    {
        [Test]
        public void TestDummyDateProvider()
        {
            DummyDateProvider.SetNow("1/1/2001");
            Assert.AreEqual("1/1/2001", DummyDateProvider.GetInstance().Now().ToShortDateString());
        }
    }
}
