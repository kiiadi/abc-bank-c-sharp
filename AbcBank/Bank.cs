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

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String CustomerSummary()
        {            
            StringBuilder sbld = new StringBuilder();
            sbld.Append("Customer Summary");
            if (customers.Count > 0)
            {
                foreach (Customer c in customers)
                {
                    sbld.Append("\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")");
                }
            }
            else
            {
                sbld.Append("\n - No accounts found");
            }            
            
            return sbld.ToString();
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String GetFirstCustomerName()
        {
            try
            {                
                return customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }
    }
}
