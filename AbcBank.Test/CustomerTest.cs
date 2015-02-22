using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    public class CustomerTest : BankTestBase
    {
        [Test]
        public void testApp()
        {

            createAdditionalAccount(1);
            addMoneyToAccount(0, 100.0);
            addMoneyToAccount(1, 4000.0);
            withdrawMoneyFromAccount(1, 200.0);

            Assert.AreEqual("Statement for John\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", bank.getFirstCustomer().getStatement());
        }


        [TestCase(0,0,1)]
        [TestCase(1,1,2)]
        [TestCase(2,2,3)]
        public void testAmountofAccounts(int accountCount, int accountType,int expectedValue)
        {
            for(int x =0; x <= accountCount; x++)
            {
                createAdditionalAccount(x);
            }
            Assert.AreEqual(expectedValue, bank.getFirstCustomer().getNumberOfAccounts());
                
        }

        [TestCase(100.0,200.0,100.0,200.0,100.0,"Transfer Compelete:  amount 100 moved from Saving (current balance 100) to Checking (current balance 200) ")]
        [TestCase(100.0,50.0,100.0,100.0,50.0,"Transfer Failed : Amount 100 greater difference (second account - first account) Saving (50) - Checking (100) ")]
        public void InterAccountTransfer(double DespositCheckingAmount, double DespositSavingsAmount, double TransferAmount, double expectedAmountChecking, double expectedAmountSavings, string ExpectedMessage )
        {
            createAdditionalAccount(1);
            addMoneyToAccount(0, DespositCheckingAmount);
            addMoneyToAccount(1, DespositSavingsAmount);
            var message = bank.getFirstCustomer().TransferBetweenAccounts(0, 1, TransferAmount);

            Assert.AreEqual(expectedAmountChecking, bank.getFirstCustomer().AccountBalance(0));
            Assert.AreEqual(expectedAmountSavings,bank.getFirstCustomer().AccountBalance(1));
            Assert.AreEqual(ExpectedMessage, message);


        }
    }
}
