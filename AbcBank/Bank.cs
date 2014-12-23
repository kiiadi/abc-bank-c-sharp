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
            StringBuilder summary = new StringBuilder();
            summary.Append("Customer Summary");
            foreach (Customer c in customers)
                summary.Append("\n - " + c.getName() + " (" + Utility.format(c.getNumberOfAccounts(), "account") + ")");
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
            String name = string.Empty;
            try
            {
               name = customers[0].getName();
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving first customer", e);
            }
            return name;
        }
    }
}
