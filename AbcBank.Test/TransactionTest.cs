using System;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class When_transaction_is_created
    {
        [Test]
        public void it_should_set_correct_amount()
        {
            double expected = new Random().Next();

            Transaction t = new Transaction(expected);
            Assert.AreEqual(expected, t.Amount);
        }


        [Test]
        public void it_should_set_correct_transaction_date()
        {
            DateProvider.now = () => new DateTime(2014, 1, 1, 1, 1, 1, 1);
            var t = new Transaction(10);
            Assert.AreEqual(new DateTime(2014, 1, 1, 1, 1, 1, 1), t.TransactionDate);
            Assert.AreNotEqual(new DateTime(2014, 1, 1, 1, 1, 1, 2), t.TransactionDate);

        }

        [TearDown]
	    public void run_after_tests()
	    {
	        DateProvider.reset();
	    }

    }
}
