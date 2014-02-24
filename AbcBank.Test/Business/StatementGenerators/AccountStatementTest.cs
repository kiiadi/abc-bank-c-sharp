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
    public class AccountStatementTest
    {
        private AccountStatement CreateTarget()
        {
            return new AccountStatement();
        }

        [Test]
        public void Generate_WhenCalled_ReturnsStatement()
        {
            //Arrange
            var target = CreateTarget();

            List<ITransaction> transactionStubs = new List<ITransaction>() { MockRepository.GenerateStub<ITransaction>() };
            transactionStubs[0].Stub(t => t.Type).Return("CHECKING");
            transactionStubs[0].Stub(t => t.Amount).Return(100.0);
            
            //Act
            string actualStatement = target.Generate(transactionStubs);
            string expectedStatement = "  CHECKING $100.00";

            

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }
    }
}
