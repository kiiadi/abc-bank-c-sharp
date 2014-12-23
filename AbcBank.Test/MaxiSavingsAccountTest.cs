using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class MaxiSavingsAccountTest
    {
      [Test]
      public void iterestEarnedPastTenDays_maxiSavingsAccount_Test()
      {
          MaxiSavingsAccount account = new MaxiSavingsAccount(); 
          account.deposit(2000);
          Assert.AreEqual(70, account.iterestEarnedPastTenDays());

      }
    }
}
