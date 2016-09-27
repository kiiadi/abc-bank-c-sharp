using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException() { }
        public InsufficientBalanceException(String Argument) { }
    }
   
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;
        
        private readonly int accountType;
        public List<Transaction> transactions;


        public Account(int accountType)
        {
            if (accountType.CompareTo(Account.CHECKING) == 0 ||
                 accountType.CompareTo(Account.SAVINGS) == 0 ||
                 accountType.CompareTo(Account.MAXI_SAVINGS) == 0)
            {
                this.accountType = accountType;
                this.transactions = new List<Transaction>();
            }
            else
                throw new ArgumentException("Account Type must be one of CHCECKING, SAVINGS and MAXI_SAVINGS");
            
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
                if (sumTransactions() <= 0)
                    throw new InsufficientBalanceException("Insuffiecient Balance in the account");
                transactions.Add(new Transaction(-amount));
                
            }
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            if (amount <=0)
                throw new InsufficientBalanceException("Insuffiecient Balance to earn interest");
            switch (accountType)
            {
                case CHECKING:
                    return amount * 0.001;

                case SAVINGS:
                    {
                        if (amount <= 1000)
                            return amount * 0.001;
                        else
                            return 1 + (amount - 1000) * 0.002;
                    }
                    
                   
                case MAXI_SAVINGS:
                    {
                       /* if (amount <= 1000)
                            return amount * 0.02;
                        if (amount <= 2000)
                            return 20 + (amount - 1000) * 0.05;
                        return 70 + (amount - 2000) * 0.1;
                        *****/
                        /**** New InterestRate Calculation ********/
                        if (DrawnInTenDays())
                        {
                            System.Console.WriteLine("Yes");
                            return amount * 0.001;
                        }
                        else
                        {
                            System.Console.WriteLine("No");
                            return amount * 0.05;
                        }
                    }
                                        
                default:
                    return 0.0;
                    
            }
        }

        public double sumTransactions()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
         }

        private bool checkIfTransactionsExist()
        {
            if (!transactions.Any())
                return false;
            return true;
        }

        public int getAccountType()
        {
            return accountType;
        }

        public bool  DrawnInTenDays()
        {
            foreach (Transaction t in transactions)
            {
                if (t.amount < 0 && (DateTime.Now - t.GetTransactionDate()).TotalDays <= 10)
                    return true;
            }
            return false;
       }
        public void TransferFunds(Account destination, double amount)
        {
            if (this.sumTransactions() <= 0)
                throw new InsufficientBalanceException("Not enough funds to transfer");
            withdraw(amount);
            destination.deposit(amount);
        }
        //Assumption is daily avarage balance is the same as the total balance in the account 
        public double AccruedInterest()
        {
            double amount = sumTransactions();
            if (amount <= 0)
                throw new InsufficientBalanceException("Insuffiecient Balance to earn interest");
           
            switch (accountType)
            {
                case CHECKING:
                    {
                        return amount * (0.001 / 12);

                    }
                  

                case SAVINGS:
                    {
                        if (amount <= 1000)
                            return amount * (0.001 /12);
                        else
                            return (1 + (amount - 1000) * 0.002)/12;
                    }


                case MAXI_SAVINGS:
                    {
                        /* if (amount <= 1000)
                             return amount * 0.02;
                         if (amount <= 2000)
                             return 20 + (amount - 1000) * 0.05;
                         return 70 + (amount - 2000) * 0.1;
                         *****/
                        /**** New InterestRate Calculation ********/
                        if (DrawnInTenDays())
                        {
                            System.Console.WriteLine("Yes");
                            return amount * (0.001/12);
                        }
                        else
                        {
                            System.Console.WriteLine("No");
                            return amount * (0.05/12);
                        }
                    }

                default:
                    return 0.0;

            }

        }
    }
}
