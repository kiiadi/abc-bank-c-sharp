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
        void Deposit(double amount, DateTime depositDate);
        void Withdraw(double amount);
        void Withdraw(double amount, DateTime withdrawalDate);
        double InterestEarned();
        double InterestEarned(DateTime now);

        //double DailyInterest();

        double Balance();

    }
}
