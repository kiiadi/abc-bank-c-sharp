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
    public class CustomerInterestCalculatorTest
    {
        private CustomerInterestCalculator CreateTarget()
        {
            return new CustomerInterestCalculator();
        }

        [Test]
        public void Calculate_ForAnyNumber_IsSumOfIndividual()
        {
            //Arrange
            var target = CreateTarget();

            int randomMax = new Random().Next(0, 11);
            double interest = 10;
            var accountStubs = Enumerable.Range(0, randomMax).Select(i => MockRepository.GenerateStub<IAccount>()).ToList();
            accountStubs.ForEach(a => a.Stub(s => s.InterestEarned).Return(interest));

            //Act
            double actualResult = target.Calculate(accountStubs);
            double expectedResult = interest * accountStubs.Count;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
