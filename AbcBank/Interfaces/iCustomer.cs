using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Enums;

namespace AbcBank.Interfaces
{
    public interface ICustomer
    {
        void OpenAccount(IAccount account);
        string GetStatement();
        int GetNumberOfAccounts();
        TransferResult TransferFunds(IAccount accountFrom, IAccount accountTo, double amount);
    }
}
