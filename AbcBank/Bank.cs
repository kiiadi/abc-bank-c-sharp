using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        public List<Customer> Customers { get; set; }

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public void addCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public String customerSummary()
        {
            String summary = "Customer Summary";

            Customers.ForEach(x => summary += string.Format("\n - {0} ({1})", x.Name, format(x.Accounts.Count, "account")));
            
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid()
        {
            double total = 0;

            total = Customers.Sum(x => x.totalInterestEarned());

            //foreach (Customer c in Customers)
            //    total += c.totalInterestEarned();
            return total;
        }

        public String getFirstCustomer()
        {
            if (Customers.Count == 0)
                return "";

            return Customers[0].Name;
        }
    }
}
