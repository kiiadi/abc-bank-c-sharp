using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        /** Readonly member that represents the amount of the current/instant transaction **/
        public readonly double amount;

        /** DateTime member that represents the date and time of current/instant transaction **/
        private DateTime transactionDate;

        /** Simple string member that represents the transaction type of the current/instant transaction object **/
        private string type;

        /** Construct a transaction object that is the value of the passped amount parameter and is of the type 
         * indicated by the passed type parameter 
         **/
        public Transaction(double amount, string type)
        {
            this.amount = amount;
            this.transactionDate = DateTime.Now; //NOT using the 'DateProvider Class' - it is not necessary.
            this.type = type;
        }

        /** Constructor a test transaction object where the transaction date value is passed as a parameter **/
        public Transaction(double amount, string type, DateTime date)
        {
            this.amount = amount;
            this.transactionDate = date;
            this.type = type;
        }

        /** Return the value of the private DateTime member that Simple string member that represents the date and time of current/instant transaction **/
        public DateTime getDate()
        {
            return this.transactionDate;
        }

        /** String member that represents the transaction type of the current/instant transaction object **/
        public string getTransactionType()
        {
            return type;
        }

    }
}
