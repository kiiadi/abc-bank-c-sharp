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
    public class AccountRateCalculatorFactoryTest
    {
        private IAccountRateCalculatorFactory CreateTarget()
        {
            return new MainFactory() as IAccountRateCalculatorFactory;
        }

        [Test]
        public void GetAccountRateCalculator_ForChecking_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var rateCalculator = target.GetAccountRateCalculator(AccountTypes.CHECKING);

            //Assert
            Assert.IsInstanceOf<AccountCheckingRateCalculator>(rateCalculator);
        }

        [Test]
        public void GetAccountRateCalculator_ForSavings_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var rateCalculator = target.GetAccountRateCalculator(AccountTypes.SAVINGS);

            //Assert
            Assert.IsInstanceOf<AccountSavingsRateCalculator>(rateCalculator);
        }

        [Test]
        public void GetAccountRateCalculator_ForMaxiSavings_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var rateCalculator = target.GetAccountRateCalculator(AccountTypes.MAXI_SAVINGS);

            //Assert
            Assert.IsInstanceOf<AccountMaxiSavingsRateCalculator>(rateCalculator);
        }

        [Test]
        public void GetAccountRateCalculator_ForUnknown_Throws()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(delegate { var rateCalculator = target.GetAccountRateCalculator((AccountTypes)int.MaxValue); });
        }
    }
}
