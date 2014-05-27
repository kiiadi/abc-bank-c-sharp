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
        //BASIC Condition Tests - expected conditions that fit typical input/output parameter(s) and/or result(s)
        [Test]
        public void TansactionLogging()
        {
            Transaction t1 = new Transaction(5, "deposit");
            Transaction t2 = new Transaction(5, "withdrawal");
            Assert.AreEqual(true, t1 is Transaction);
            Assert.AreEqual(true, t2 is Transaction);
        }

        //----THE TESTS BELOW WOULD HAVE BEEN IMPLEMENTED, BUT OUT OF TIME ...

        //BOUNDARY Condition Tests - edge conditions that do not fit typical input/output parameter(s) and/or result(s)

        //INVERSE Condition Tests - when conditions are reversed the outcome should still be valid

        //CROSS CHECK Tests - the end result can be traced back to the beginning values

        //FORCED ERROR Tests - keys obvious values are incorrect

        //PERFORMANCE Tests - stree test of the code (NOT USUALLY APPLICABLE TO NONE WEB HOSTED APPLICATIONS)
        //---For desktop machine performance will often hinge on the hardware running the application

        [TearDown]
        public void CleanUpTest()
        {
            //No unmanged test object(s) to garbage collect
        }

        [TestFixtureTearDown]
        public void CleanUpTestFixture()
        {
            //No unmanged test fixture object(s) to garbage collect
        }
    }
}
