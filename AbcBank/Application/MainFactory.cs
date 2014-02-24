using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Business;

namespace AbcBank.Application
{
    public interface IAccountFactory
    {
        IAccount GetNewAccount(AccountTypes accountType);
    }

    public interface ITransactionFactory
    {
        ITransaction GetNewTransaction(double amount);
    }

    public interface IAccountRateCalculatorFactory
    {
        IAccountRateCalculator GetAccountRateCalculator(AccountTypes accountType);
    }


    public class MainFactory : IAccountFactory, ITransactionFactory, IAccountRateCalculatorFactory
    {
        #region ITransactionFactory
        public ITransaction GetNewTransaction(double amount)
        {
            return new Transaction(amount);
        }
        #endregion

        #region IAccountFactory
        public IAccount GetNewAccount(AccountTypes accountType)
        {
            return new Account(accountType, this as ITransactionFactory, GetAccountStatement(), GetAccountRateCalculator(accountType), GetTransactionsToPeriodsConverter());
        }

        private IAccountStatement GetAccountStatement()
        {
            return new AccountStatement();
        }

        private ITransactionsToPeriodsConverter GetTransactionsToPeriodsConverter()
        {
            return new TransactionsToPeriodsConverter();
        }
        #endregion

        #region IAccountRateCalculatorFactory
        public IAccountRateCalculator GetAccountRateCalculator(AccountTypes accountType)
        {
            switch (accountType)
            {
                case AccountTypes.CHECKING:
                    return new AccountCheckingRateCalculator();
                case AccountTypes.SAVINGS:
                    return new AccountSavingsRateCalculator();
                case AccountTypes.MAXI_SAVINGS:
                    return new AccountMaxiSavingsRateCalculator();
                default:
                    throw new ArgumentException(string.Format("Invalid AccountType: {0}", accountType.ToString()));
            }
        }
        #endregion
    }
}
