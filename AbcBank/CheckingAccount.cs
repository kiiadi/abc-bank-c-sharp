using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class CheckingAccount: Account
    {
        public override string AccountType()
        {
            return "Checking Account";
        }

        public override double InterestEarned()
        {
            double amount = Balance();
            return amount * 0.001;
        }
                
        public override double DailyInterest(double principal, int withdrawalCount)
        {
            return (principal * 0.0001) / 365.0;            
        }


    }
}
