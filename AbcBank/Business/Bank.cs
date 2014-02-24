using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface IBank
    {
        double TotalInterestPaid { get; }
        string CustomersSummary { get; }
        int CustomersCount { get; }

        IAccount OpenAccount(ICustomer customer, AccountTypes accountType);
    }

    public class Bank : IBank
    {
        IAccountFactory accountFactory;
        IBankStatement bankStatement;
        IBankInterestCalculator interestCalculator;
        private readonly HashSet<ICustomer> customers = new HashSet<ICustomer>();

        #region Properties
        public double TotalInterestPaid
        {
            get { return interestCalculator.Calculate(customers); }
        }

        public String CustomersSummary
        {
            get { return bankStatement.Generate(customers); }
        }

        public int CustomersCount
        {
            get { return customers.Count; }
        }
        #endregion

        public Bank(IAccountFactory accountFactory, IBankStatement bankStatement, IBankInterestCalculator interestCalculator)
        {
            this.bankStatement = bankStatement;
            this.accountFactory = accountFactory;
            this.interestCalculator = interestCalculator;
        }

        #region Methods
        public IAccount OpenAccount(ICustomer customer, AccountTypes accountType)
        {
            customers.Add(customer);
            return accountFactory.GetNewAccount(accountType);
        }
        #endregion
    }
}
