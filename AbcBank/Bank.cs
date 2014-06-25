using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String customerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.Name+ " (" + StringHelper.PluralizeStringBasedOnNumber(c.Accounts.Count(), "account") + ")";
            return summary;
        }

        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }

        public String getFirstCustomer()
        {
             if (customers.Count() > 0)
                 return customers[0].Name;
             else
                 throw new Exception("There are no customers in the bank, therefore the first customer cannot be returned.");
        }
    }
}
