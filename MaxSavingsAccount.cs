using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class MaxSavingsAccount:IAccount
    {
        
        Transaction objTrans;
        public List<Transaction> transactions;
        public bool isWithdrawalInLastTenDays = false;

        string IAccount.nameofaccount
        {
            get { return "MaxSaving"; }
        }
        public MaxSavingsAccount() 
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

            //if (amount <= 1000)
            //    return amount * 0.02;
            //if (amount <= 2000)
            //    return 20 + (amount - 1000) * 0.05;
            //return 70 + (amount - 2000) * 0.1;

            //changed by rds
            if (isWithdrawalInLastTenDays) //need to move this  to db for checking the transactions in past ten days
                return (amount * NumDays * 0.001 )/365;
            else
                return (amount * NumDays * 0.05) / 365; ;
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
        public DateTime GetAcctOpenedDate() 
        {
            var alldate =
                 from tran in transactions
                 orderby tran.transactionDate 
                 select tran.transactionDate;

            return  alldate.First<DateTime>();

        }
    }
}
