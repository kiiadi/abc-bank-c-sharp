using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Interfaces;
using System.Linq.Expressions;

namespace AbcBank
{
    public class Bank: iBank
    {
        private List<iCustomer> Customers;

        public Bank()
        {
            Customers = new List<iCustomer>();
        }

        public void AddCustomer(iCustomer customer)
        {
            Customers.Add(customer);
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in Customers)
                summary += "\n - " + c.Name + " (" + Format(c.GetNumberOfAccounts(), "account") + ")";
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
            return Customers.Sum(x => (x as Customer).TotalInterestEarned());
        }

        public double TotalInterestPaid(DateTime now)
        {
            return Customers.Sum(x => (x as Customer).TotalInterestEarned(now));
        }

        public String GetFirstCustomer()
        {
            try
            {
                Customers = null;
                return (Customers[0] as Customer).Name;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }
    }
}
