using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AbcBank.Business;
using AbcBank.Application;
using Rhino.Mocks;

namespace AbcBank.Test.Business.StatementGenerators
{
    [TestFixture]
    public class BankStatementtTest
    {
        private BankStatement CreateTarget()
        {
            return new BankStatement();
        }

        [Test]
        public void Generate_WhenOneAccount_ReturnsStatement()
        {
            //Arrange
            var target = CreateTarget();

            List<ICustomer> customerStubs = new List<ICustomer>() { MockRepository.GenerateStub<ICustomer>() };
            customerStubs[0].Stub(t => t.Name).Return("John");
            customerStubs[0].Stub(t => t.NumberOfAccounts).Return(1);

            //Act
            string actualStatement = target.Generate(customerStubs);
            string expectedStatement = "Customer Summary\r\n" +
                                        " - John (1 account)";

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }

        [Test]
        public void Generate_WhenManyAccounts_ReturnsStatement()
        {
            //Arrange
            var target = CreateTarget();

            List<ICustomer> customerStubs = new List<ICustomer>() { MockRepository.GenerateStub<ICustomer>() };
            customerStubs[0].Stub(t => t.Name).Return("John");
            customerStubs[0].Stub(t => t.NumberOfAccounts).Return(5);

            //Act
            string actualStatement = target.Generate(customerStubs);
            string expectedStatement = "Customer Summary\r\n" +
                                        " - John (5 accounts)";

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }

        [Test]
        public void Generate_WhenTwoCustomers_ReturnsStatement()
        {
            //Arrange
            var target = CreateTarget();

            List<ICustomer> customerStubs = new List<ICustomer>() 
            { 
                MockRepository.GenerateStub<ICustomer>(),
                MockRepository.GenerateStub<ICustomer>() 
            };
            customerStubs[0].Stub(t => t.Name).Return("John");
            customerStubs[0].Stub(t => t.NumberOfAccounts).Return(5);

            customerStubs[1].Stub(t => t.Name).Return("Mary");
            customerStubs[1].Stub(t => t.NumberOfAccounts).Return(2);

            //Act
            string actualStatement = target.Generate(customerStubs);
            string expectedStatement = "Customer Summary\r\n" +
                                        " - John (5 accounts)\r\n" +
                                        " - Mary (2 accounts)";

            //Assert
            Assert.AreEqual(expectedStatement, actualStatement);
        }
    }
}
