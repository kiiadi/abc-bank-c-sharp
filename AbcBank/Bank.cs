using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        private readonly List<Customer> customers = new List<Customer>();
        public Bank()
        {
            
        }
        public void AddCustomer(Customer customer)
        {
            if (this.customers.Contains(customer))
                throw new Exception("Customer already exists");

            this.customers.Add(customer);
            
        }
        public string GetCustomerSummary()
        {
            var summary = new StringBuilder("Customer Summary");
            foreach (var c in this.customers)
                summary.AppendFormat("\n {0}",c.GetSummary());
            
            return summary.ToString();
        }
        public IEnumerable<Customer> Customers
        {
            get {return this.customers; }
        }
        public double GetTotalPaidInterest()
        {
            return this.customers.Sum(a => a.GetEarnedInterest());
        }
    }
}
