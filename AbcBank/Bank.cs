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

        public List<string> customerSummaries()
        {
            var customerSummaries = new List<string>();
            foreach (Customer c in customers)
            {
                customerSummaries.Add(string.Format("{0}{1}{2} ({3}{4})","Customer Summary","\n - ",c.getName(),c.getNumberOfAccounts(),string.Format(" account{0}",c.getNumberOfAccounts()==1 ? "" : "s")));
            }
            return customerSummaries;
        }


        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }

        public Customer getFirstCustomer()
        {
            return customers != null ? customers[0] : null;
        }
    }
}
