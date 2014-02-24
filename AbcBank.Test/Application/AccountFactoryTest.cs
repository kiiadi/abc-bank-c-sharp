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
    public class AccountFactoryTest
    {
        private IAccountFactory CreateTarget()
        {
            return new MainFactory() as IAccountFactory;
        }

        [Test]
        public void GetNewAccount_WhenCalled_Succeeds()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var accountTypes = Enum.GetValues(typeof(AccountTypes));
            int r = new Random().Next(0, accountTypes.Length);
            var account = target.GetNewAccount((AccountTypes)accountTypes.GetValue(r));

            //Assert
            Assert.NotNull(account);
        }

        [Test]
        public void GetNewAccount_ForUnknown_Throws()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(delegate { var account = target.GetNewAccount((AccountTypes)int.MaxValue); });
        }
    }
}
