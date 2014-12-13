using System;
using System.Collections.Generic;
using System.Text;

namespace AbcBank
{
    public class Bank
    {
        //Interest rates should accrue daily (incl. weekends), rates above are per-annum

        private List<Customer> customers;
        private List<string> customerSummaryList = new List<string>();
        private string customerSummary;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }
        
        public List<string> GetCustomerSummaryList()  
        {            
            foreach (Customer c in customers)
                this.customerSummaryList.Add(c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")");                      
            return this.customerSummaryList;
        }

        public string GetCustomerSummary()
        {
            foreach (Customer c in customers)
            {
                if (this.customerSummary != null) { this.customerSummary += ","; }
                this.customerSummary +=  c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")"; 
            }
            return this.customerSummary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private static String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
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
