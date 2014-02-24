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

            double depositAmount = 500;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 27},
                new InvestmentPeriod {amount = 200, days = 35},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);


            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.001, 27);
            double period2amount = CompoundInterestRate.Calculate(200 + period1amount, 0.001, 35);
            double calculatedExpected = period2amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_Above1000_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 5000;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 3000, days = 27},
                new InvestmentPeriod {amount = 2000, days = 35},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);
            
            double period1amount = CompoundInterestRate.Calculate(3000, 0.002, 27);
            double period2amount = CompoundInterestRate.Calculate(2000 + period1amount, 0.002, 35);
            double calculatedExpected = period2amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_MixedAmount_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 1500;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 27},
                new InvestmentPeriod {amount = 1200, days = 35},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);


            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.001, 27);
            double period2amount = CompoundInterestRate.Calculate(1200 + period1amount, 0.002, 35);
            double calculatedExpected = period2amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
    }
}
