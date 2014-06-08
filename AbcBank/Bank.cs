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

        // generate text-based customer summary for all customers
        public String customerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        // calculate total interest paid to all accounts belonging to all customers
        public double totalInterestPaid()
        {
            double total = 0.0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }

        // determine name of bank's first customer
        public String getFirstCustomer()
        {
            try
            {
                customers = null;
                return customers[0].getName();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }


    }
}
