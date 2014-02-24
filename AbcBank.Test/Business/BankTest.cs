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
    public class BankTest
    {
        IAccountFactory accountFactoryStub;
        IBankStatement bankStatementStub;
        IBankInterestCalculator bankInterestCalculatorStub;
        private const double ONE_CUSTOMER_INTEREST = 20.0;

        public BankTest()
        {
            accountFactoryStub = MockRepository.GenerateStub<IAccountFactory>();
            bankStatementStub = MockRepository.GenerateStub<IBankStatement>();
            bankInterestCalculatorStub = MockRepository.GenerateStub<IBankInterestCalculator>();
        }

        private Bank CreateTarget()
        {
            return new Bank(accountFactoryStub, bankStatementStub, bankInterestCalculatorStub);
        }

        [Test]
        public void CustomersSummary_IfCalled_Generated()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            string dummy = target.CustomersSummary;

            //Assert
            bankStatementStub.AssertWasCalled(s => s.Generate(Arg<HashSet<ICustomer>>.Is.Anything));
        }

        [Test]
        public void TotalInterestPaid_IfCalled_Calculated()
        {
            //Arrange
            var target = CreateTarget();
            
            //Act
            double dummy = target.TotalInterestPaid;

            //Assert
            bankInterestCalculatorStub.AssertWasCalled(c => c.Calculate(Arg<HashSet<ICustomer>>.Is.Anything));
        }
        
        [Test]
        public void OpenAccount_IfCalled_CustomerIsAddedToList()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            target.OpenAccount(GetNewCustomerStub(), AccountTypes.CHECKING);
            target.OpenAccount(GetNewCustomerStub(), AccountTypes.CHECKING);
            target.OpenAccount(GetNewCustomerStub(), AccountTypes.CHECKING);

            //Assert
            Assert.AreEqual(target.CustomersCount, 3);
        }

        private ICustomer GetNewCustomerStub()
        {
            ICustomer customerStub = MockRepository.GenerateStub<ICustomer>();
            customerStub.Stub(c => c.TotalInterestEarned).Return(ONE_CUSTOMER_INTEREST);
            return customerStub;
        }

        /*
        private static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(AccountTypes.CHECKING));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.GetCustomersSummary());
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypes.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypes.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypes.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }
        */

    }
}
