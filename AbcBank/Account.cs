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
                transactions.Add(new Transaction(amount));
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
                transactions.Add(new Transaction(-amount));
            }
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            double interestEarned = 0.0;
            switch (Type)
            {
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        interestEarned = amount * 0.001;
                    else
                        interestEarned = 1 + (amount - 1000) * 0.002;
                    break;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        interestEarned = amount * 0.02;
                    else if (amount <= 2000)
                        interestEarned = 20 + (amount - 1000) * 0.05;
                    else
                        interestEarned = 70 + (amount - 2000) * .1;
                    break;
                case AccountType.CHECKING:
                    interestEarned = amount * 0.001;
                    break;
                default:
                    break;
            }
            return interestEarned;
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

        public string getStringRepresentationForAccount(Account a)
        {
            string strRet = string.Empty;
            switch (a.Type)
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
