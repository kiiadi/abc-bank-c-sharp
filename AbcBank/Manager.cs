using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class Manager
    {
        private String fName;
        public String Name { get { return fName; } set { fName = value; } }

        public Manager(String name)
        {
            Name = name;
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary - Total Customers: " + Bank.Customers.Count + " - Printed by " + Name;
            foreach (Customer c in Bank.Customers)
                summary += "\n - " + c.Name + " (" + Format(c.NumberOfAccounts, "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        // TODO Ask Business if a customer can have more than one account of any type.
        //      if "yes", then retain the provisionary changes below.  E.g. 11 accounts
        //          this also would require
        //              TODO having unique ID/Account number
        //      elae - can go back to just number == 1
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
            foreach (Customer c in Bank.Customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String TotalInterestPaidReport()
        {
            return "Total Interest Paid by Bank: " + Utility.ToDollars(TotalInterestPaid()) + " - Printed by " + Name;
        }
    }
}
