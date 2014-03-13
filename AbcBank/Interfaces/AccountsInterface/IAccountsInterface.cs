using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AccountsInterface
{
    public interface IAccountsInterface
    {
        int AccountType { get; }
        //List<int> AccountList { get; }
        double PerformInterestCalculations(double values);
        void EnterTransactions(double amount);
        Dictionary<DateTime, double> AccountTransactions { get; }
        String Name { get; }
    }
}
