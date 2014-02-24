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

        private ITransaction GetTransactionSub(string transactionType, int days)
        {
            ITransaction transactionStub = MockRepository.GenerateStub<ITransaction>();
            transactionStub.Stub(t => t.Type).Return(transactionType);
            transactionStub.Stub(t => t.AgeInDays).Return(days);
            return transactionStub;
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

            double depositAmount = 100;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 30},
                new InvestmentPeriod {amount = -200, days = 5},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            List<ITransaction> transactionStubs = new List<ITransaction>()
            {
                GetTransactionSub(TransactionTypes.DEPOSIT, 35),
                GetTransactionSub(TransactionTypes.WITHDRAWAL, 5),
            };
            accountStub.Stub(a => a.Transactions).Return(transactionStubs);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.001, 30);
            double period2amount = CompoundInterestRate.Calculate(-200 + period1amount, 0.001, 5);
            double calculatedExpected = period2amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }

        [Test]
        public void Calculate_NoWithdrawalsWithin10Days_Correct()
        {
            //Arrange
            var target = CreateTarget();

            double depositAmount = 200;
            IAccountForRateCalculators accountStub = MockRepository.GenerateStub<IAccountForRateCalculators>();
            List<InvestmentPeriod> investmentPeriods = new List<InvestmentPeriod>() 
            {
                new InvestmentPeriod {amount = 300, days = 30},
                new InvestmentPeriod {amount = -200, days = 20},
                new InvestmentPeriod {amount = 100, days = 5},
            };
            accountStub.Stub(a => a.InvestmentPeriods).Return(investmentPeriods);
            List<ITransaction> transactionStubs = new List<ITransaction>()
            {
                GetTransactionSub(TransactionTypes.DEPOSIT, 55),
                GetTransactionSub(TransactionTypes.WITHDRAWAL, 25),
                GetTransactionSub(TransactionTypes.DEPOSIT, 5),
            };
            accountStub.Stub(a => a.Transactions).Return(transactionStubs);
            accountStub.Stub(a => a.CurrentAmount).Return(depositAmount);

            //Act
            double calculatedActual = target.Calculate(accountStub);

            double period1amount = CompoundInterestRate.Calculate(300, 0.05, 30);
            double period2amount = CompoundInterestRate.Calculate(-200 + period1amount, 0.05, 20);
            double period3amount = CompoundInterestRate.Calculate(100 + period2amount, 0.05, 5);
            double calculatedExpected = period3amount - depositAmount;

            //Assert
            Assert.AreEqual(calculatedExpected, calculatedActual);
        }
        
    }
}
