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
    public class AccountMaxiSavingsRateCalculatorTest
    {
        private AccountMaxiSavingsRateCalculator CreateTarget()
        {
            return new AccountMaxiSavingsRateCalculator();
        }

        [Test]
        public void Calculate_Below1000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next(1, 1001);

            //Act
            double calculatedActual = target.Calculate(depositAmount);
            double calculatedExpected = depositAmount * 0.02;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_Between1000And2000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next(1001, 2001);

            //Act
            double calculatedActual = target.Calculate(depositAmount);
            double calculatedExpected = 20 + (depositAmount - 1000) * 0.05;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_Above2000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next(2001, int.MaxValue);

            //Act
            double calculatedActual = target.Calculate(depositAmount);
            double calculatedExpected = 70 + (depositAmount - 2000) * 0.1;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
    }
}
