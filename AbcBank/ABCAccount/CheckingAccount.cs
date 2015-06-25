using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AbcAccount
{
    public class CheckingAccount : AccountBase
    {

        public CheckingAccount(Double amount):base(amount)
        {
            accountAmount = amount;
        }
       
        public override Double InterestEarned()
        {
            return accountAmount * Constants.MultipleAmount_1;
        }
    }
}
