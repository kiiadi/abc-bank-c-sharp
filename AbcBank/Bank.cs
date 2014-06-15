using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class Bank
    {
        private List<Customer> Customers;

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary - Total Customers: " + Customers.Count;
            foreach (Customer c in Customers)
                summary += "\n - " + c.Name + " (" + Format(c.NumberOfAccounts, "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        //  Observe:
        // TODO Ask Business if number of types of accounts grows to 11 or more, then a customer
        //      can have 11 accounts (plural word for number ending with 1) 
        //  Also:
        // TODO Ask Business if a customer can have more than one account of any type.
        //      if "yes", then retain the provisionary changes below.  E.g. 11 accounts
        //          this also would require, if any distiction between accouts of the same type was needed, 
        //              TODO having unique ID/Account number
        // 
        //      elae - can go back to just number == 1
        //
        private String Format(int number, String word)
        {
            int remainder10;
            Math.DivRem(number, 10, out remainder10);
            int remainder100;
            Math.DivRem(number, 100, out remainder100);
            return number + " " + (remainder10 == 1 && remainder100 != 11 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in Customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String TotalInterestPaidReport()
        {
            return "Total Interest Paid by Bank: " + TotalInterestPaid().ToString();
        }
    }
}
