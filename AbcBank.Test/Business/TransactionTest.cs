using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Business;
using AbcBank.Application;

namespace AbcBank.Test.Business
{
    [TestFixture]
    public class TransactionTest
    {
        private Transaction CreateTarget(double amount)
        {
            return new Transaction(amount);
        }

        [Test]
        public void Amount_WhenRetrieved_EqualToInitial()
        {
            //Arrange
            //Act
            double initialAmount = (double)new Random().Next();
            var target = CreateTarget(initialAmount);

            double retrievedAmount = target.Amount;

            //Assert
            Assert.AreEqual(initialAmount, retrievedAmount);
        }

        [Test]
        public void Type_ForPositive_IsDeposit()
        {
            //Arrange
            //Act
            double initialAmount = (double)new Random().Next();
            var target = CreateTarget(initialAmount);

            string actualType = target.Type;
            string expectedType = TransactionTypes.DEPOSIT;

            //Assert
            Assert.AreEqual(expectedType, actualType);
        }

        [Test]
        public void Type_ForNegative_IsWithdrawal()
        {
            //Arrange
            //Act
            double initialAmount = -(double)new Random().Next();
            var target = CreateTarget(initialAmount);

            string actualType = target.Type;
            string expectedType = TransactionTypes.WITHDRAWAL;

            //Assert
            Assert.AreEqual(expectedType, actualType);
        }
        
    }
}
