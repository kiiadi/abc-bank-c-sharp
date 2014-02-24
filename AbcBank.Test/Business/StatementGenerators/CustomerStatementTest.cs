using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;

namespace AbcBank.Test.Business.StatementGenerators
{
    [TestFixture]
    public class CustomerStatementTest
    {
        private CustomerStatement CreateTarget()
        {
            return new CustomerStatement();
        }

        [Test]
        public void Generate_WhenCalled_ReturnsStatement()
        {
            //Arrange
            var target = CreateTarget();

            List<IAccount> accountStubs = new List<IAccount>() { MockRepository.GenerateStub<IAccount>() };
            accountStubs[0].Stub(a => a.Name).Return("CHECKING");
            accountStubs[0].Stub(a => a.TransactionsStatement).Return("  deposit $100.00");
            accountStubs[0].Stub(a => a.CurrentAmount).Return(100.0);

            //System.Diagnostics.Debug.Assert(false);

            //Act
            string actualStatement = target.Generate("John", accountStubs);
            string expectedStatement = "Statement for John\r\n" +
                    "\r\n" +
                    "CHECKING\r\n" +
                    "  deposit $100.00\r\n" +
                    "Total $100.00\r\n" +
                    "\r\n" +
                    "Total In All Accounts $100.00";

            //string expectedStatement = "Statement for John\r\n" +
            //        "\r\n" +
            //        "CHECKING_10000001\r\n" +
            //        "  deposit $100.00\r\n" +
            //        "Total $100.00\r\n" +
            //        "\r\n" +
            //        "SAVINGS_10000002\r\n" +
            //        "  deposit $4,000.00\r\n" +
            //        "  withdrawal $200.00\r\n" +
            //        "Total $3,800.00\r\n" +
            //        "\r\n" +
            //        "Total In All Accounts $3,900.00";

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }
    }
}
