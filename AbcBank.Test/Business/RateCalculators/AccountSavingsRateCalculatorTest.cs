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
    public class AccountSavingsRateCalculatorTest
    {
        private AccountSavingsRateCalculator CreateTarget()
        {
            return new AccountSavingsRateCalculator();
        }

        [Test]
        public void Calculate_Below1000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next(1, 1001);

            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);
            double calculatedExpected = depositAmount * 0.001;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_Above1000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next(1001, int.MaxValue);

            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);
            double calculatedExpected = 1 + (depositAmount - 1000) * 0.002;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
    }
}
