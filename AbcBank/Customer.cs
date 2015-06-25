using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AbcAccount;

namespace AbcBank
{
    public class Customer
    {
        private String name;
        
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String getName()
        {
            return name;
        }

        
      
    }
}
