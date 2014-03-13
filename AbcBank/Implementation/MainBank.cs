using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.CustomerInterface;

namespace AbcBank.Implementation
{
    public class MainBank
    {
        private List<ICustomerInterface> customers;
        public MainBank()
        {
            customers = new List<ICustomerInterface>();
        }

        public void AddCustomer(ICustomerInterface Customer)
        {
            customers.Add(Customer);
        }

        public String CustomerSummary()
        {
            StringBuilder customerSummary = new StringBuilder();
            customerSummary.AppendLine("Customer Summary");
            foreach (Customers c in customers)
                customerSummary.AppendLine(String.Format(" - Customer {0} maintains {1}", c.CustomerName, format(c.TotalAccounts, "account")));
            return customerSummary.ToString();
        }

        public String InterestSummary()
        {
            StringBuilder interestSummary = new StringBuilder("Interest Summary");
            interestSummary.AppendLine("Interest Summary");
            interestSummary.AppendLine(String.Format(" - Interest paid on all accounts held by the bank is {0} ", toDollars(totalInterestPaid())));
            return interestSummary.ToString();
        }

        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid()
        {
            double total = 0.0; 
            foreach (Customers c in customers) 
                total += c.totalInterestEarned();
            return total;
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
