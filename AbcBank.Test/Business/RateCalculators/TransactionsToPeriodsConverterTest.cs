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
    public class TransactionsToPeriodsConverterTest
    {
        private TransactionsToPeriodsConverter CreateTarget()
        {
            return new TransactionsToPeriodsConverter();
        }

        private ITransaction GetTransactionSub(string transactionType, double amount, int days)
        {
            ITransaction transactionStub = MockRepository.GenerateStub<ITransaction>();
            transactionStub.Stub(t => t.Type).Return(transactionType);
            transactionStub.Stub(t => t.Amount).Return(amount);
            transactionStub.Stub(t => t.AgeInDays).Return(days);
            return transactionStub;
        }

        [Test]
        public void Calculate_WhenDepositsAndWithdrawals_PeriodsCountCorrect()
        {
            //Arrange
            var target = CreateTarget();

            List<ITransaction> transactionStubs = new List<ITransaction>();
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 300.0, 67));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.WITHDRAWAL, -200.0, 40));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 1000.0, 5));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.WITHDRAWAL, -200.0, 1));

            //Act
            List<InvestmentPeriod> invetmentPeriods = target.Calculate(transactionStubs);

            //Assert
            Assert.AreEqual(transactionStubs.Count, invetmentPeriods.Count);
        }

        [Test]
        public void Calculate_WhenDeposits_PeriodsAmountsAndDaysCorrect()
        {
            //Arrange
            var target = CreateTarget();

            List<ITransaction> transactionStubs = new List<ITransaction>();
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 300.0, 67));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 200.0, 40));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 1000.0, 5));

            //Act
            List<InvestmentPeriod> invetmentPeriods = target.Calculate(transactionStubs);

            //Assert
            Assert.AreEqual(invetmentPeriods[0].amount, 300, 0);
            Assert.AreEqual(invetmentPeriods[0].days, 27, 0);

            Assert.AreEqual(invetmentPeriods[1].amount, 200, 0);
            Assert.AreEqual(invetmentPeriods[1].days, 35, 0);

            Assert.AreEqual(invetmentPeriods[2].amount, 1000, 0);
            Assert.AreEqual(invetmentPeriods[2].days, 5, 0);
        }

        [Test]
        public void Calculate_WhenDepositsAndWithdrawals_PeriodsAmountsAndDaysCorrect()
        {
            //Arrange
            var target = CreateTarget();

            List<ITransaction> transactionStubs = new List<ITransaction>();
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 300.0, 67));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.WITHDRAWAL, -200.0, 40));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.DEPOSIT, 1000.0, 5));
            transactionStubs.Add(GetTransactionSub(TransactionTypes.WITHDRAWAL, -400.0, 1));

            //Act
            List<InvestmentPeriod> invetmentPeriods = target.Calculate(transactionStubs);

            //Assert
            Assert.AreEqual(invetmentPeriods[0].amount, 300, 0);
            Assert.AreEqual(invetmentPeriods[0].days, 27, 0);

            Assert.AreEqual(invetmentPeriods[1].amount, -200, 0);
            Assert.AreEqual(invetmentPeriods[1].days, 35, 0);

            Assert.AreEqual(invetmentPeriods[2].amount, 1000, 0);
            Assert.AreEqual(invetmentPeriods[2].days, 4, 0);

            Assert.AreEqual(invetmentPeriods[3].amount, -400, 0);
            Assert.AreEqual(invetmentPeriods[3].days, 1, 0);
        }

    }
}
