using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class CheckingAccount : Account
    {        
        public CheckingAccount()
        {
        }

        public override double InterestEarned()
        {
            double amount = SumTransactions();
            return amount * 0.001;
        }
    }
}
