using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class BankReports
    {
 
        public String customerSummary(List<ICustomer> customers)
        {
            String summary = "Customer Summary";
            foreach (var c in customers)
            {
                summary += String.Format("\n - {0} ({1})", c.getName(), format(c.getNumberOfAccounts(), "account"));
            }
            return summary;
        }

        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

    }

}
