using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;

namespace AbcBank
{
    public class Bank: iBank
    {
        private List<iCustomer> customers;

        public Bank()
        {
            customers = new List<iCustomer>();
        }

        public void AddCustomer(iCustomer customer)
        {
            customers.Add(customer);
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
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

        public String GetFirstCustomer()
        {
            try
            {
                customers = null;
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
