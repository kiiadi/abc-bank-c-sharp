using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {

        public enum AccountType {CHECKING, SAVINGS, MAXI_SAVINGS};

        private List<Transaction> transactions;

        public List<Transaction> Transactions
        {
            get { return transactions; }
        }

        private AccountType type;
        public AccountType Type
        {
            get { return type; }
        }

        public Account(AccountType type)
        {
            this.type = type;
            this.transactions = new List<Transaction>();
        }

        public void deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, Transaction.transactionType.DEPOSIT, DateTime.UtcNow));
            }
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, Transaction.transactionType.WITHDRAWAL, DateTime.UtcNow));
            }
        }

        public double interestEarned()
        {
            double sumOfTrans = this.sumTransactions();
            double dailyAccrualDivisor = 365;
            switch (this.Type)
            {
                case AccountType.CHECKING:
                    return sumTransactions() * (Constants.Checking_IntRate / dailyAccrualDivisor);

                case AccountType.SAVINGS:
                    if (sumOfTrans <= 1000)
                        return sumOfTrans * (Constants.MaxiSavings_IntRate_TranOcurredLessTenDays / dailyAccrualDivisor);
                    else
                        return (1 / dailyAccrualDivisor) + (sumOfTrans - 1000) * (Constants.Savings_IntRate_BalGreater1000 / dailyAccrualDivisor);

                case AccountType.MAXI_SAVINGS:
                    return Transactions.Where(x => x.UtcDate >= DateTime.UtcNow.AddDays(-10)).Count() > 0 ?
                        sumOfTrans * (Constants.MaxiSavings_IntRate_TranOcurredLessTenDays / dailyAccrualDivisor) :
                        sumOfTrans * (Constants.MaxiSavings_IntRate_TranOcurredGreaterTenDays / dailyAccrualDivisor);
            }
            return 0.0;
        }

        public double sumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }

        private bool checkIfTransactionsExist()
        {
            return transactions.Count() > 0 ? true : false;
        }

        public string getStringRepresentationForAccount()
        {
            string strRet = string.Empty;
            switch (Type)
            {
                case Account.AccountType.CHECKING:
                    strRet += "Checking Account\n";
                    break;
                case Account.AccountType.SAVINGS:
                    strRet += "Savings Account\n";
                    break;
                case Account.AccountType.MAXI_SAVINGS:
                    strRet += "Maxi Savings Account\n";
                    break;
                default:
                    throw new Exception("Account type is not valid");
            }
            return strRet;
        }
    }
}
