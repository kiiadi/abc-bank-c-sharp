using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Interfaces
{
    public interface IBank
    {
        void AddCustomer(Customer customer);
        string CustomerSummary();        
        double TotalInterestPaid();
        double TotalInterestPaid(DateTime now);
    }
}
