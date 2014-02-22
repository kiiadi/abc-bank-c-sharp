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
        private IAccountForRateCalculators GetAccountStub(double depositAmount, List<Tuple<string, int>> tuples)
        {
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();

            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            List<ITransaction> transactionStubs = new List<ITransaction>();
            foreach (var pair in tuples)
            {
                ITransaction transactionStub = MockRepository.GenerateStub<ITransaction>();
                transactionStub.Stub(t => t.Type).Return(pair.Item1);
                transactionStub.Stub(t => t.AgeInDays).Return(pair.Item2);
                transactionStubs.Add(transactionStub);
            }
            accountStub.Stub(a => a.Transactions).Return(transactionStubs);

            return accountStub;
        }

        private AccountMaxiSavingsRateCalculator CreateTarget()
        {
            return new AccountMaxiSavingsRateCalculator();
        }

        [Test]
        public void Calculate_WithdrawalsWithin10Days_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next();
            IAccountForRateCalculators accountStub = GetAccountStub(depositAmount, new List<Tuple<string, int>>() 
            {
                Tuple.Create<string, int>(TransactionTypes.DEPOSIT, 20),
                Tuple.Create<string, int>(TransactionTypes.WITHDRAWAL, 5),
                Tuple.Create<string, int>(TransactionTypes.DEPOSIT, 2),
            });
            
            //Act
            double calculatedActual = target.Calculate(accountStub);
            double calculatedExpected = depositAmount * 0.001;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_NoWithdrawalsWithin10Days_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = new Random().Next();
            IAccountForRateCalculators accountStub = GetAccountStub(depositAmount, new List<Tuple<string, int>>() 
            {
                Tuple.Create<string, int>(TransactionTypes.DEPOSIT, 35),
                Tuple.Create<string, int>(TransactionTypes.WITHDRAWAL, 20),
                Tuple.Create<string, int>(TransactionTypes.DEPOSIT, 5),
            });

            //Act
            double calculatedActual = target.Calculate(accountStub);
            double calculatedExpected = depositAmount * 0.05;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
        
    }
}
