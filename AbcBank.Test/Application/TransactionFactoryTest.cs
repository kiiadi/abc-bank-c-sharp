using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;

namespace AbcBank.Test.Application
{
    [TestFixture]
    public class TransactionFactoryTest
    {
        private ITransactionFactory CreateTarget()
        {
            return new MainFactory() as ITransactionFactory;
        }

        [Test]
        public void GetNewTransaction_WhenCalled_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var transaction = target.GetNewTransaction((double)new Random().Next());

            //Assert
            Assert.NotNull(transaction);
        }
    }
}
