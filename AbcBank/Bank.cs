using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AbcAccount;

namespace AbcBank
{
    public class Bank
    {
        public List<CustomerAccount> customerAccounts;

        public Bank()
        {
            customerAccounts = new List<CustomerAccount>();
        }

        public void AddCustomer(CustomerAccount customerAccount)
        {
            customerAccounts.Add(customerAccount);
        }

        //get customer summary report
        public String GetCustomerSummary()
        {
            return BankUtility.GetCustomerSummary(customerAccounts);
        }

        public double totalInterestPaid()
        {
            double total = 0;
            foreach (CustomerAccount c in customerAccounts)
                total += c.totalInterestEarned();
            return total;
        }

        public String getFirstCustomer()
        {
            try
            {
                return customerAccounts.FirstOrDefault().customer.getName();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }
    }
}
