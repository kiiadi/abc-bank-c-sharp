using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank.Application
{
    public enum AccountTypes
    {
        CHECKING,
        SAVINGS,
        MAXI_SAVINGS
    }

    public static class TransactionTypes
    {
        public const string DEPOSIT = "deposit";
        public const string WITHDRAWAL = "withdrawal";
    }
}
