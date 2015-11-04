using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AbcAccount
{
    /// <summary>
    /// Base class to get the interest earned.
    /// </summary>
    public abstract class AccountBase
    {
        public Double accountAmount;
        public AccountBase(Double amount)
        {
            accountAmount = amount;
        }
        /// <summary>
        /// Returns interest earned based on amount
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual Double InterestEarned()
        {
            return accountAmount * Constants.MultipleAmount_1;

        }


    }
}
