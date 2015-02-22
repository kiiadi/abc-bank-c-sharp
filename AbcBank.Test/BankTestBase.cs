using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    public class BankTestBase
    {
        public Bank bank;
        public Account checkingAccount;
        public Account SavingAccount;
        public Account MaxiSavings;

        [SetUp]
        public void Setup()
        {
            bank = new Bank();
            checkingAccount = new Account(Account.CHECKING);
            SavingAccount = new Account(Account.SAVINGS);
            MaxiSavings = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(CreateCustomer());
        }

        [TearDown]
        public void Teardown()
        {
            bank = null;
            checkingAccount = null;
            SavingAccount = null;
            MaxiSavings = null;

        }

        #region helperMethods

        protected Customer CreateCustomer(string name = null)
        {
            var customer = new Customer(string.IsNullOrEmpty(name) ? "John" : name);
            customer.openAccount(checkingAccount);
            return customer;
        }

        protected void createAdditionalAccount(int accountType)
        {
            switch (accountType)
            {
                case 0:
                    bank.getFirstCustomer().openAccount(checkingAccount);
                    break;
                case 1:
                    bank.getFirstCustomer().openAccount(SavingAccount);
                    break;
                case 2:
                    bank.getFirstCustomer().openAccount(MaxiSavings);
                    break;
                default:
                    break;
                //error;
            }
        }
        protected void addMoneyToAccount(int accountType, double Amount)
        {
            bank.getFirstCustomer().getAccount(accountType).deposit(Amount);
        }

        protected void withdrawMoneyFromAccount(int accountType, double Amount)
        {
            bank.getFirstCustomer().getAccount(accountType).withdraw(Amount);
        }
        #endregion 
    }
}
