using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank.AbcAccount;

namespace AbcBank
{
    /// <summary>
    /// utiltiy class contains functions related to getting summary and report related information.
    /// </summary>
   public static class BankUtility
    {

        //Get the cusomer sumary
        public static String GetCustomerSummary(List<CustomerAccount> customerAccounts)
        {
            String summary = "Customer Summary";
            foreach (CustomerAccount c in customerAccounts)
                summary += "\n - " + c.customer.getName() + " (" + Format(c.getNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private static String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }



        /*******************************
       * This method gets a statement
       *********************************/
        public static String getStatement(CustomerAccount accounts)
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement = "Statement for " + accounts.customer.getName() + "\n";
            double total = 0.0;
            foreach (Account a in accounts.GetAccounts())
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += CalculationHelper.SumTransactions(a.transactions);
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

       /// <summary>
       /// Statemenyt for account passed
       /// </summary>
       /// <param name="account"></param>
       /// <returns></returns>
        private static String statementForAccount(Account account)
        {
            String s = "";

            //Translate to pretty account type
            switch (account.getAccountType())
            {
                case AccountType.Checking:
                    s += "Checking Account\n";
                    break;
                case AccountType.Savings:
                    s += "Savings Account\n";
                    break;
                case AccountType.MaxiSavings:
                    s += "Maxi Savings Account\n";
                    break;
            }
           
            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction tran in account.transactions)
            {
                s += "  " + (tran.amount < 0 ? Constants.CONST_WITHDRAWAL : Constants.CONST_DEPOSIT) + " " + toDollars(tran.amount) + "\n";
                total += tran.amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }

        private static String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }


    }
}
