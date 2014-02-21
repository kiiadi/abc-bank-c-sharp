using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Interfaces
{
    public interface iAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        double InterestEarned();
        double Balance();

    }
}
