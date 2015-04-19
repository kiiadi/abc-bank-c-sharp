using System.Collections.Generic;

namespace AbcBank
{
    public class Bank
    {
        public static List<Customer> Customers;
        private static List<Manager> Managers;

        public Bank()
        {
            Customers = new List<Customer>();
            Managers = new List<Manager>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }
    }
}
