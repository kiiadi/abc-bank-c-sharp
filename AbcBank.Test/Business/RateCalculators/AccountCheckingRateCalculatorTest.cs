using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;

namespace AbcBank.Test.Business.RateCalculators
{
    [TestFixture]
    public class AccountCheckingRateCalculatorTest
    {
        private AccountCheckingRateCalculator CreateTarget()
        {
            return new AccountCheckingRateCalculator();
        }

        [Test]
        public void Calculate_ForAnyAmount_IsFlat()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next();

            //Act
            double calculatedActual = target.Calculate(depositAmount);
            double calculatedExpected = depositAmount * 0.001;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
    }
}
