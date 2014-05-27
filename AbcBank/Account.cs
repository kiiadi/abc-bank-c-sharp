using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        /** Constant members that represents each type of account object that may be instantiated **/
        public const int CHECKING = 0; //checking account
        public const int SAVINGS = 1; //savings account
        public const int MAXI_SAVINGS = 2; //maxi_savings account

        /** Readonly member that represents the account type of the current/instant account object **/
        private readonly int accountType;
        
        /** Generic list collection of all the transactions for the current/instant account object **/
        public List<Transaction> transactions;

        /** Construct an account of the type indicated by value of passed parameter
         * @param accountType - The integer representing the account type for the current/instant account object
         **/
        public Account(int accountType)
        {
            if (accountType > 2 || accountType < 0)
            {
                throw new ArgumentException("Error: The account type id must be a value between 0 and 2");
            }
            else
            {
                this.accountType = accountType;
                this.transactions = new List<Transaction>();
            }

        }

        /** Deposit funds into the instant account object
         * @param amount - The integer representing the amount of funds/money to be deposited into the current/instant account object
         **/
        public void deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Error: Deposit amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount,"deposit"));
            }
        }

        /** Deposit funds into the instant account object using a test method that allows the date to be set
         * @param amount - The integer representing the amount of funds/money to be deposited into the current/instant account object
         **/
        public void testDeposit(double amount, DateTime date)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Error: Deposit amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, "deposit", date));
            }
        }

        /** Withdraw funds from the instant account object
         * @param amount - The value of the funds being deposited into the current/instant account object
         **/
        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Error: Withdrawal amount must be greater than zero");
            }
            if (amount > sumTransactions())
            {
                throw new ArgumentException("Error: Insufficient funds available for the desired withdrawal amount");
            }
            else
            {
                transactions.Add(new Transaction(-amount, "withdrawal"));
            }
        }

        /** Calculate the interest earned by the instant account object **/
        public double interestEarned()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    {
                        if (amount <= 1000)
                        {
                            return (amount * 0.001 * 365); //Implementation of 365 day per year accrual
                        }
                        else
                        {
                            return ((1 + (amount - 1000)) * 0.002 * 365); //Implementation of 365 day per year accrual
                        }
                    }
                case MAXI_SAVINGS:
                    {
                        //Interest rates are now 5% provided there have not been any withdrawals in the last 10 days
                        if (highInterestWorthy())
                        {
                            return (amount * 0.05 * 365); //Implementation of 365 day per year accrual
                        }
                        else
                        {
                            return (amount * 0.001 * 365); //Implementation of 365 day per year accrual
                        }
                    }
                
                default:
                    {
                        return (amount * 0.001 * 365); //Implementation of 365 day per year accrual
                    }
            }
        }

        /** Determine if the instant account meets the criteria for earning 5% interest on deposited funds **/
        public bool highInterestWorthy()
        {
            DateTime today = DateTime.Now;
            TimeSpan dayWindow = today.Subtract(transactions.ElementAt((transactions.Count - 1)).getDate());

            /** Calculate the number of days since last transaction 
             *  If dayWindow is > 10 then the account is high interest worthy - return true
             *  If dayWindow is < 10 then check if a withdrawal was made in the last 10 days
             *  --If a withdrawal has been made then the account is not high interest worthy - return false
             *  --If a withdrawal has not been made then the account is high interest worthy - return true
             **/

            if (dayWindow.Days <= 10)
            {
                //If the days windows is within 10, then determine if a withdrawal transaction occurred
                for (int i = transactions.Count - 1; i >= 0; i--)
                {
                    if (transactions[i].getTransactionType().Equals("deposit"))
                    {
                        continue;
                    }
                    else if (transactions[i].getTransactionType().Equals("withdrawal"))
                    {   return false;
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid transaction type");
                        throw new ArgumentException("Error: Invalid transaction type");
                    }
                }
            }
            return true;
        }

        /** Return the calculated net funds in the instant account object **/
        public double sumTransactions()
        {
            return checkIfTransactionsExist();
        }

        /** Determine if transactions exist and then calculate the total net funds in the instant account object 
         * @param checkAll - The boolean determinate for whether or not 
         **/
        private double checkIfTransactionsExist() //REMOVED bool checkALL - it was not needed/used at all by the method
        {
            double amount = 0.0;
            
            foreach (Transaction t in transactions)
            {
                amount += t.amount;
            }

            return amount;
        }

        /** Return the account type of the instant account object **/
        public int getAccountType()
        {
            return accountType;
        }
    }
}
