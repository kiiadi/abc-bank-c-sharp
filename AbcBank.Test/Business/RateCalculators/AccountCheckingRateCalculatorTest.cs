using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;
using System.Diagnostics;

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
        public void Calculate_OneDeposits_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 1000;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = depositAmount, days = 365},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);
            double calculatedExpected = CompoundInterestRate.Calculate(depositAmount, 0.001, 365) - depositAmount;

            //Assert
            Assert.AreEqual(calculatedActual, calculatedExpected);
        }

        [Test]
        public void Calculate_ThreeDeposits_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 1500;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 27},
                new InvestmentPeriod {amount = 200, days = 35},
                new InvestmentPeriod {amount = 1000, days = 5},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.001, 27);
            double period2amount = CompoundInterestRate.Calculate(200 + period1amount, 0.001, 35);
            double period3amount = CompoundInterestRate.Calculate(1000 + period2amount, 0.001, 5);
            double calculatedExpected = period3amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_DepositsAndWithdrawals_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 700;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 27},
                new InvestmentPeriod {amount = -200, days = 35},
                new InvestmentPeriod {amount = 1000, days = 4},
                new InvestmentPeriod {amount = -400, days = 1},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.001, 27);
            double period2amount = CompoundInterestRate.Calculate(-200 + period1amount, 0.001, 35);
            double period3amount = CompoundInterestRate.Calculate(1000 + period2amount, 0.001, 4);
            double period4amount = CompoundInterestRate.Calculate(-400 + period3amount, 0.001, 1);
            double calculatedExpected = period4amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
    }
}
