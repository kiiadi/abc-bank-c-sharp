using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.AccountsInterface
{
    public interface ISavingsInterface
    {
        double SavingsCalculations(double amount);
        int AccountType { get; }
    }
}
