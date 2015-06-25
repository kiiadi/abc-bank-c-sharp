using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AbcAccount
{
   public class MaxiSavingsAccount : AccountBase
    {

       public MaxiSavingsAccount(Double amount)
           : base(amount)
        {
            accountAmount = amount;
        }
        public override Double InterestEarned()
        {
            if (accountAmount <= Constants.CompareAmount)
                return accountAmount * Constants.MultipleAmount_2;
            if (accountAmount <= 2000)
                return 20 + (accountAmount - Constants.CompareAmount) * 0.05;
            return 70 + (accountAmount - 2000) * Constants.MultipleAmount_1;
        }
    }
}
