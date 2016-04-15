using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class CheckingAccount: IAccount
    {
        string IAccount.nameofaccount
        {
            get { return "Checking"; }
        }

        Transaction objTrans;
        public List<Transaction> transactions;
        public CheckingAccount() 
        {
            this.transactions = new List<Transaction>();
        }
        
        public List<Transaction> gettrans
        {
            get { return transactions; }
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

        public double interestearned()
        { 
            double amount = sumtransactions();
            DateTime today = DateProvider.Instance.now();
            DateTime acctOpenedOn = GetAcctOpenedDate();
            TimeSpan t = today - acctOpenedOn;
            double NumDays = t.TotalDays;

            return (amount * 0.001 * NumDays)/365;
        }
        public DateTime GetAcctOpenedDate()
        {
            var alldate =
                 from tran in transactions
                 orderby tran.transactionDate
                 select tran.transactionDate;

            return alldate.First<DateTime>();

        }
        public double sumtransactions()
        {
            return checkIfTransactionsExist(true);
        }

        private double checkIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }
    }
}
