using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public interface IAccount
    {
        string nameofaccount {get;}
        List<Transaction> gettrans { get; }
        void deposit(double amt);
        void withdraw(double amt);
        double interestearned();
        double sumtransactions();
    }
}
