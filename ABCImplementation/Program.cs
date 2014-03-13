using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank;
using AbcBank.AccountsInterface;
using AbcBank.CustomerInterface;
using AbcBank.Implementation;

namespace ABCImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IAccountsInterface HenryChecking = new CheckingAccount();
            IAccountsInterface HenrySavings = new SavingsAccount();
            //IAccountsInterface HenryMaxiSavings = new MaxiSavingsAccount();
            ICustomerInterface Henry = new Customers("Henry");
            Henry.AddAccount(HenryChecking);
            Henry.AddAccount(HenrySavings);
            //Henry.AddAccount(HenryMaxiSavings);
            //Henry.Deposit(HenryChecking, 100.00);
            //Henry.Deposit(HenryChecking, 225.00);
            //Henry.Deposit(HenrySavings, 1550.00);
            //Henry.Withdraw(HenrySavings, 225.00);
            //Henry.Deposit(HenrySavings, 1225.00);
            //Henry.Withdraw(HenrySavings, 1225.00);
            //Henry.Deposit(HenryMaxiSavings, 1747.00);
            //Henry.Deposit(HenryMaxiSavings, 2750.00);
            //Henry.Transfer(HenrySavings, HenryChecking, 1250);
            Henry.Deposit(HenryChecking, 100.00);
            Henry.Deposit(HenryChecking, 225.00);
            Henry.Deposit(HenryChecking, 1750.00);
            Henry.Deposit(HenrySavings, 1550.00);
            Henry.Deposit(HenrySavings, 1225.00);
            Henry.Transfer(HenrySavings, HenryChecking, 1125.00);
            String HenryAccountStatement = Henry.GetAccountStatementforCustomer();
            IAccountsInterface JohnChecking = new CheckingAccount();
            IAccountsInterface JohnMaxiSavings = new MaxiSavingsAccount();
            ICustomerInterface John = new Customers("John");
            John.AddAccount(JohnChecking);
            John.AddAccount(JohnMaxiSavings);
            John.Deposit(JohnChecking, 1000.00);
            John.Deposit(JohnChecking, 1100.00);
            John.Withdraw(JohnChecking, 203.20);
            John.Withdraw(JohnChecking, 200.00);
            John.Deposit(JohnMaxiSavings, 1747.00);
            John.Deposit(JohnMaxiSavings, 27050.00);
            String JohnAccountStatement = John.GetAccountStatementforCustomer();
            IAccountsInterface JamesChecking = new CheckingAccount();
            IAccountsInterface JamesSavings = new SavingsAccount();
            IAccountsInterface JamesMaxiSavings = new MaxiSavingsAccount();
            ICustomerInterface James = new Customers("James");
            James.AddAccount(JamesChecking);
            James.AddAccount(JamesSavings);
            James.AddAccount(JamesMaxiSavings);
            James.Deposit(JamesChecking, 2679.72);
            James.Withdraw(JamesChecking, 500.00);
            James.Deposit(JamesSavings, 15000.00);
            James.Deposit(JamesMaxiSavings, 22500.00);
            James.Deposit(JamesMaxiSavings, 17000.00);
            James.Withdraw(JamesMaxiSavings, 20000.00);
            String JamesAccountStatement = James.GetAccountStatementforCustomer();
            MainBank bank = new MainBank();
            bank.AddCustomer(Henry);
            bank.AddCustomer(John);
            bank.AddCustomer(James);
            String CustomerSummary = bank.CustomerSummary();
            String InterestSummary = bank.InterestSummary();
        }
    }
}
