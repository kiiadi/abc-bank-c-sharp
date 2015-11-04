using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbcBank.AbcAccount;

namespace AbcBank
{
    public class CustomerAccount
    {
        private List<Account> accounts;
        public Customer customer;

        public List<Account>  GetAccounts()
        {
            return accounts;
        }
        public CustomerAccount(Customer customer)
        {
            this.customer = customer;
            this.accounts = new List<Account>();
        }

        public CustomerAccount openAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }
        /// <summary>
        /// Get the total interest earned for the customer.
        /// </summary>
        /// <returns></returns>
        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.interestEarned();
            return total;
        }


    }
}
