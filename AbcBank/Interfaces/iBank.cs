using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Interfaces
{
    public interface iBank
    {
        void AddCustomer(iCustomer customer);
        string CustomerSummary();        
        double TotalInterestPaid();
        double TotalInterestPaid(DateTime now);
    }
}
