using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AbcAccount
{
    public class SavingAccount : AccountBase
    {

        public SavingAccount(Double amount)
            : base(amount)
        {
            accountAmount = amount;
        }

        public override Double InterestEarned()
        {
            if (accountAmount <= Constants.CompareAmount)
                return accountAmount * Constants.MultipleAmount_1;
            else
                return 1 + (accountAmount - Constants.CompareAmount) * Constants.MultipleAmount_2;

        }
    }
}
