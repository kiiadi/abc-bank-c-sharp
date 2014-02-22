using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Enums;

namespace AbcBank.Interfaces
{
    public interface iCustomer
    {
        void OpenAccount(iAccount account);
        string GetStatement();
        int GetNumberOfAccounts();
        TransferResult TransferFunds(iAccount accountFrom, iAccount accountTo, double amount);
    }
}
