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
    public class BankInterestCalculatorTest
    {
        private BankInterestCalculator CreateTarget()
        {
            return new BankInterestCalculator();
        }

        [Test]
        public void Calculate_ForAnyNumber_IsSumOfIndividual()
        {
            //Arrange
            var target = CreateTarget();

            int randomMax = new Random().Next(0, 11);
            double interest = 10;
            var customerStubs = Enumerable.Range(0, randomMax).Select(i => MockRepository.GenerateStub<ICustomer>()).ToList();
            customerStubs.ForEach(c => c.Stub(s => s.TotalInterestEarned).Return(interest));

            //Act
            double actualResult = target.Calculate(customerStubs);
            double expectedResult = interest * customerStubs.Count;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
