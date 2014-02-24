using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    public class Bank
    {
        private List<ICustomer> customers;

        public Bank()
        {
            customers = new List<ICustomer>();
        }

        public void addCustomer(ICustomer customer)
        {
            customers.Add(customer);
        }

        public String customerSummary()
        {
            return new BankReports().customerSummary(customers);
        }

        public double totalInterestPaid()
        {
            return customers.Sum(c => c.totalInterestEarned());
        }

        public List<ICustomer> getCustomers()
        {
            return customers;
        }
    }
}
