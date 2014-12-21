using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        #region Properties
        private Customer tmpCustomer;
        public Customer firstCustomer;
        public List<Customer> CustomersColl;
        private string custSourceName = @"Data\Customers.txt";
        #endregion

        public Bank()
        {
            Load();
        }

        private void Load()
        {
            CustomersColl = new List<Customer>();

            if (File.Exists(custSourceName))
            {
                string line;
                using (StreamReader str = new StreamReader(custSourceName))
                {
                    while ((line = str.ReadLine()) != null)
                    {
                        string[] strArray = line.Split(',');
                        if (strArray != null && strArray.Length == 7)
                        {
                            int tmpID = 0;
                            int custType = 0;

                            Int32.TryParse(strArray[3], out tmpID);
                            Int32.TryParse(strArray[2], out custType);

                            if (tmpID > 0 && custType == 2)
                            {
                                tmpCustomer = new Customer(tmpID);
                                this.CustomersColl.Add(tmpCustomer);

                                if (firstCustomer == null)
                                    firstCustomer = tmpCustomer;
                            }
                        }
                    }
                }
            }
        }

        public String customerSummary()
        {
            String summary = "USER ID\t CUSTOMER SUMMARY\n";
            summary += "--------------------------------------------------------------------------------\n";

            foreach (Customer c in CustomersColl)
            {
                //summary += "\n - " + c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")";
                summary += " " + c.UserID + "\t " + c.getName() + "  [# Accounts : " + c.getNumberOfAccounts() + " |  Total $ = $" + c.getTotalDollarsInAllAccounts() + "]\n";
            }
            return summary;
        }

        public String interestSummary()
        {
            String summary = "USER ID\t TOTAL INTERESET EARNED \t CUSTOMER NAME\n";
            summary += "--------------------------------------------------------------------------------\n";

            foreach (Customer c in CustomersColl)
                summary += " " + c.UserID + "\t\t" + "$" + c.totalInterestEarned() + "\t\t\t" + c.getName() + "\n";

            return summary;
        }

        public void updateInterestOnAllAccounts()
        {
            foreach (Customer cust in CustomersColl)
            {
                foreach (Account custAcc in cust.AccountsCollection)
                    custAcc.updateAccountInterest();
            }
        }

        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in CustomersColl)
            {
                total += c.totalInterestEarned();
            }
            return total;
        }

        public String getFirstCustomer()
        {
            return firstCustomer.getName();
        }
    }
}
