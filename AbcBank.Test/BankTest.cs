using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    public class BankTest : BankTestBase
    {
        #region global Variables
        private static readonly double DOUBLE_DELTA = 1e-15;
        #endregion 

        [Test]
        public void customerSummariesTest()
        {
            bank.addCustomer(CreateCustomer("John"));
            var customerSumList = bank.customerSummaries();
            Assert.AreEqual(2, customerSumList.Count);
            customerSumList.ForEach(s => Assert.AreEqual("Customer Summary\n - John (1 account)", s));
        }

        [Test]
        public void checkingAccountTest()
        {
            addMoneyToAccount(0,100.0);
            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_accountTest()
        {
            createAdditionalAccount(1);
            addMoneyToAccount(1,1500.0);
            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            createAdditionalAccount(2);
            addMoneyToAccount(2,3000.0);
            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }




    }
}
