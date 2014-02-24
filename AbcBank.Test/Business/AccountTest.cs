using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;

namespace AbcBank.Test.Business
{
    [TestFixture]
    public class AccountTest
    {
        ITransactionFactory transactionFactoryStub;
        IAccountRateCalculator accountRateCalculatorStub;
        IAccountStatement accountStatementStub;

        private Account CreateTarget(AccountTypes accountType)
        {
            transactionFactoryStub = MockRepository.GenerateStub<ITransactionFactory>();
            accountStatementStub = MockRepository.GenerateStub<IAccountStatement>();
            accountRateCalculatorStub = MockRepository.GenerateStub<IAccountRateCalculator>();
            return new Account(accountType, transactionFactoryStub, accountStatementStub, accountRateCalculatorStub, new TransactionsToPeriodsConverter());
        }

        [Test]
        public void Name_WhenAccountConstructed_NotEmpty()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act

            //Assert
            Assert.IsNotNullOrEmpty(target.Name);
        }

        [Test]
        public void Deposit_IfCalled_IncrementsAmount()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);
            
            //Act
            double originalAmount = target.CurrentAmount;
            target.Deposit(500);
            double newAmount = target.CurrentAmount;

            //Assert
            Assert.AreEqual(newAmount, originalAmount + 500);
        }

        [Test]
        public void Deposit_IfLessThan0_Throws()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            //Assert
            Assert.Throws < ArgumentException>(delegate {target.Deposit(-500);} );
        }

        [Test]
        public void Deposit_IfCalled_TransactionIsCreated()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            double amount = 1000;
            target.Deposit(amount);

            //Assert
            transactionFactoryStub.AssertWasCalled(f => f.GetNewTransaction(amount));
        }

        [Test]
        public void Withdraw_IfCalled_DecrementsAmount()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);
            target.Deposit(10000);
            
            //Act
            double originalAmount = target.CurrentAmount;
            target.Withdraw(500);
            double newAmount = target.CurrentAmount;

            //Assert
            Assert.AreEqual(newAmount, originalAmount - 500);
        }

        [Test]
        public void Withdraw_IfLessThan0_Throws()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(delegate { target.Withdraw(-500); });
        }

        [Test]
        public void Withdraw_IfLessThanCurrent_Throws()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            target.Deposit(500);
            double amountToWithdraw = target.CurrentAmount + 1;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(delegate { target.Withdraw(amountToWithdraw); });
        }

        [Test]
        public void Withdraw_IfCalled_TransactionIsCreated()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            double amount = 1000;
            target.Deposit(amount * 2);
            target.Withdraw(amount);

            //Assert
            transactionFactoryStub.AssertWasCalled(f => f.GetNewTransaction(Arg<double>.Is.Anything), options => options.Repeat.Twice());
        }

        [Test]
        public void InterestEarned_IfRequested_Calculates()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            double dummy = target.InterestEarned;

            //Assert
            accountRateCalculatorStub.AssertWasCalled(c => c.Calculate(Arg<IAccountForRateCalculators>.Is.Anything));
        }

        [Test]
        public void TransactionsStatement_IfRequested_Generated()
        {
            //Arrange
            var target = CreateTarget(AccountTypes.CHECKING);

            //Act
            string dummy = target.TransactionsStatement;

            //Assert
            accountStatementStub.AssertWasCalled(c => c.Generate(Arg<List<ITransaction>>.Is.Anything));
        }
    }
}
