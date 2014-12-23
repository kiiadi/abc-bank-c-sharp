using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcBank
{
    public class CheckingAccount:Account
    {
        private readonly string accountType;

        public CheckingAccount()
        {
            this.accountType = "Checking Account";
        }
        public override string getAccountType()
        {
            return accountType;
        }
    }
}
