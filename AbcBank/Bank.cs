using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        /** Generic list collection of customer objects **/
        private List<Customer> customers;

        /** Bank name **/
        private string name;

        /** Construct the current/instant bank object and initialize the customer collection object **/
        public Bank(string name)
        {
            customers = new List<Customer>();
            this.name = name;
        }

        /** Add a customer object to the collection of customer objects **/
        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        /** Return a summary of each customer associated with the current/instant back object **/
        public string customerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")";
            return summary;
        }

        /** Return a properly formatted reference to the number of accounts associated with a customer using the appropriate plurality 
         * if applicable 
         **/
        private string format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        /** Return the total interest earned by each customer across all accounts at the current/instant bank **/
        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }
    }
}
