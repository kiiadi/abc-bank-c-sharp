using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AbcBank
{
    public class Bank
    {
        #region private fields
        private List<Customer> _customers = new List<Customer>();
        #endregion

        #region ctor(s)
        public Bank()
        {
        }
        #endregion

        #region public methods
        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public String BuildCustomerSummary()
        {
            StringBuilder summary = new StringBuilder("Customer Summary:");
            _customers.ForEach(
                c => { summary.AppendFormat("{0} - {1} ({2})", System.Environment.NewLine, c.Name, Format(c.NumberOfAccounts, "account")); }
                );

            return summary.ToString();
        }
        public double TotalInterestPaid()
        {
            return _customers.Select(c => c.TotalInterestEarned()).Sum();
        }
        public String GetFirstCustomer()
        {
            return GetFirstCustomer(null);
        }
        /*"First customer" only makes sense if you specify the sorting criteria*/
        public String GetFirstCustomer(IComparer<Customer> customerComparer)
        {
            try
            {
                if (_customers.Count == 0)
                    throw new ApplicationException("No customers.  Things are not good :-(");

                List<Customer> clonedCustomers = new List<Customer>(_customers);
                clonedCustomers.Sort(customerComparer);

                return clonedCustomers[0].Name;
            }
            catch (Exception e)
            {
                /*error handling can be improved*/
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }
        #endregion

        #region private methods
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return string.Format("{0}{1}", word, number <= 1 ? string.Empty : "s");
        }
        #endregion
    }
}
