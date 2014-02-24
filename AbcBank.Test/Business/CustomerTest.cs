using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;
using System.Diagnostics;


namespace AbcBank.Test.Business
{
    [TestFixture]
    public class CustomerTest
    {
        private readonly IBank bank;
        private readonly ICustomerStatement customerStatementStub;
        private readonly ICustomerInterestCalculator customerInterestCalculatorStub;

        public CustomerTest()
        {
            bank = MockRepository.GenerateStub<IBank>();
            customerStatementStub = MockRepository.GenerateStub<ICustomerStatement>();
            customerInterestCalculatorStub = MockRepository.GenerateStub<ICustomerInterestCalculator>();

            IAccount accountStub = MockRepository.GenerateStub<IAccount>();
            accountStub.Stub(a => a.Name).Return("Name");

            bank.Stub(b => b.OpenAccount(Arg<ICustomer>.Is.Anything, Arg<AccountTypes>.Is.Anything)).Return(accountStub);
        }

        private Customer CreateTarget()
        {
            return new Customer("TestName", bank, customerStatementStub, customerInterestCalculatorStub);
        }

        [Test]
        public void OpenAccount_Checking_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            string account = target.OpenAccount(AccountTypes.CHECKING);

            //Assert
            bank.AssertWasCalled(b => b.OpenAccount(target, AccountTypes.CHECKING));
        }

        [Test]
        public void Deposit_ExistingAccount_Succeeds()
        {
            //Arrange
            var target = CreateTarget();
            string accountName = target.OpenAccount(AccountTypes.CHECKING);
            
            //Act
            //Assert
            Assert.DoesNotThrow(delegate { target.Deposit(accountName, 1000.0); });
        }

        [Test]
        public void Deposit_NonExistingAccount_Throws()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            //Assert
            Assert.Throws<KeyNotFoundException>(delegate { target.Deposit("Non-Existing Account", 1000); });
        }

        [Test]
        public void Withdraw_ExistingAccount_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            string account = target.OpenAccount(AccountTypes.CHECKING);
            target.Deposit(account, 1000);

            //Act
            //Assert
            Assert.DoesNotThrow(delegate { target.Withdraw(account, 500); });
        }

        [Test]
        public void Withdraw_NonExistingAccount_Throws()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            //Assert
            Assert.Throws<KeyNotFoundException>(delegate { target.Withdraw("Non-Existing Account", 1000); });
        }

        [Test]
        public void AccountsAndTotalsStatement_IfRequested_Generated()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            string dummy = target.AccountsAndTotalsStatement;

            //Assert
            customerStatementStub.AssertWasCalled(c => c.Generate(Arg<string>.Is.Anything, Arg<List<IAccount>>.Is.Anything));
        }

        [Test]
        public void AccountsAndTotalsStatement_Request_MatchesExpectedResult()
        {
            //Arrange
            var target = CreateTarget();
            string checkingAccount = target.OpenAccount(AccountTypes.CHECKING);
            string savingsAccount = target.OpenAccount(AccountTypes.SAVINGS);

            target.Deposit(checkingAccount, 100.0);
            target.Deposit(savingsAccount, 4000.0);
            target.Withdraw(savingsAccount, 200.0);

            string expectedStatement = "Statement";
            customerStatementStub.Stub(c => c.Generate(Arg<string>.Is.Anything, Arg<List<IAccount>>.Is.Anything)).Return(expectedStatement);

            //Act
            string actualStatement = target.AccountsAndTotalsStatement;

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }

        [Test]
        public void TotalInterestEarned_IfRequested_Calculated()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            double dummy = target.TotalInterestEarned;

            //Assert
            customerInterestCalculatorStub.AssertWasCalled(c => c.Calculate(Arg<IEnumerable<IAccount>>.Is.Anything));
        }

        [Test]
        public void Transfer_BetweenValidAccounts_Succeeds()
        {
            //Arrange
            var target = CreateTarget();
            string checkingAccount = target.OpenAccount(AccountTypes.CHECKING);
            string savingsAccount = target.OpenAccount(AccountTypes.SAVINGS);

            target.Deposit(checkingAccount, 100.0);
            target.Deposit(savingsAccount, 4000.0);
            
            //Act
            //Assert
            Assert.DoesNotThrow(delegate {target.Transfer(savingsAccount, checkingAccount, 700.0);});
        }

        [Test]
        public void Transfer_FromInvalidAccounts_Throws()
        {
            //Arrange
            var target = CreateTarget();
            
            string checkingAccount = target.OpenAccount(AccountTypes.CHECKING);
            target.Deposit(checkingAccount, 100.0);

            //Act
            //Assert
            Assert.Throws<KeyNotFoundException>(delegate { target.Transfer("Invalid Account", checkingAccount, 700.0); });
        }

        [Test]
        public void Transfer_ToInvalidAccounts_Throws()
        {
            //Arrange
            var target = CreateTarget();
            string savingsAccount = target.OpenAccount(AccountTypes.SAVINGS);
            target.Deposit(savingsAccount, 4000.0);

            //Act
            //Assert
            Assert.Throws<KeyNotFoundException>(delegate { target.Transfer(savingsAccount, "Invalid Account", 700.0); });
        }
    }
}
