using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Implementation;

namespace AbcBank.AccountsInterface
{
    public interface ICheckingInterface
    {
        double CheckingCalculations(double amount);
        int AccountType { get; }
    }
}
