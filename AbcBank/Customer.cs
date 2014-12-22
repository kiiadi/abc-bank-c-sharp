using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Customer
    {
        #region Properties

        private string _userName = "";
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        private string _password = "";
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _type = "";
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _userID = 0;
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        private string _fName = "";
        public string FirstName
        {
            get
            {
                return _fName;
            }
            set
            {
                _fName = value;
            }
        }

        private string _lName = "";
        public string LastName
        {
            get
            {
                return _lName;
            }
            set
            {
                _lName = value;
            }
        }

        public enum CustomerType
        {
            Employee = 1,
            User = 2
        }

        private DateTime? _CreateDate = null;
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private Account tmpAccount;
        private Transaction tmpTransaction;

        public List<Account> AccountsCollection = new List<Account>();
        private string sourceName = @"Data\Customers.txt";

        #endregion

        public Customer()
        {
            setupNewCustomer();
        }

        public Customer(int userID)
        {
            this.UserID = userID;
            Load();
        }

        private void setupNewCustomer()
        {
            this.UserID = NextUserID();
            AccountsCollection.Clear();
        }

        private void Load()
        {
            Load(this.UserID);
        }

        private void Load(int userID)
        {
            bool customerFound = false;

            if (userID > 0)
            {
                if (File.Exists(sourceName))
                {
                    string line;
                    using (StreamReader str = new StreamReader(sourceName))
                    {
                        while ((line = str.ReadLine()) != null)
                        {
                            string[] strArray = line.Split(',');
                            if (strArray != null && strArray.Length == 7)
                            {
                                int tmpID = 0;
                                Int32.TryParse(strArray[3], out tmpID);

                                if (tmpID == userID)
                                {
                                    this.UserID = tmpID;
                                    this.UserName = strArray[0];
                                    this.Password = strArray[1];

                                    int custType;
                                    Int32.TryParse(strArray[2], out custType);
                                    if (custType == (Int32)Customer.CustomerType.Employee)
                                        this.Type = Customer.CustomerType.Employee.ToString();
                                    else if (custType == (Int32)Customer.CustomerType.User)
                                        this.Type = Customer.CustomerType.User.ToString();

                                    this.FirstName = strArray[4];
                                    this.LastName = strArray[5];
                                    if (strArray[6] != null)
                                        this.CreateDate = Convert.ToDateTime(strArray[6].ToString());

                                    customerFound = true;

                                    //Load Account Details
                                    tmpAccount = new Account();
                                    AccountsCollection.Clear();
                                    List<int> accountNumbers = tmpAccount.getAccountNumbersByUser(this.UserID);

                                    foreach (int accNum in accountNumbers)
                                    {
                                        tmpAccount = new Account(accNum);
                                        AccountsCollection.Add(tmpAccount);
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    if (!customerFound)
                        ResetToDefaultValues();
                }
            }
        }

        private void ResetToDefaultValues()
        {
            _userName = "";
            _password = "";
            _type = "";
            _userID = 0;
            _fName = "";
            _lName = "";
            _CreateDate = null;

            AccountsCollection.Clear();
        }

        public bool Validate()
        {
            bool retVal = true;
            //Input any validations necesaary before save.

            return retVal;
        }

        public bool Save()
        {
            bool retVal = true;

            if (Validate())
            {
                string accountUpdate = "";
                accountUpdate += this.UserName;
                accountUpdate += "," + this.Password;

                if (this.Type == Customer.CustomerType.User.ToString())
                    accountUpdate += "," + (Int32)Customer.CustomerType.User;
                else if (this.Type == Customer.CustomerType.Employee.ToString())
                    accountUpdate += "," + (Int32)Customer.CustomerType.Employee;

                accountUpdate += "," + this.UserID.ToString();
                accountUpdate += "," + this.FirstName;
                accountUpdate += "," + this.LastName;

                if (!File.Exists(sourceName))
                {
                    this.CreateDate = DateTime.Now;
                    accountUpdate += "," + this.CreateDate;

                    File.Create(sourceName);
                    using (StreamWriter str = new StreamWriter(sourceName))
                    {
                        str.WriteLine(accountUpdate);
                    }
                }
                else
                {
                    //If user exists. Update.
                    List<string> lines = new List<string>(File.ReadAllLines(sourceName));
                    int lineIndex = lines.FindIndex(line => line.StartsWith(this.UserName.Trim() + ","));
                    if (lineIndex != -1)
                    {
                        accountUpdate += "," + this.CreateDate;
                        lines[lineIndex] = accountUpdate;
                        File.WriteAllLines(sourceName, lines);
                    }
                    else //Else create a new user.
                    {
                        this.CreateDate = DateTime.Now;
                        accountUpdate += "," + this.CreateDate;

                        //using (StreamWriter str = new StreamWriter(sourceName))
                        //    str.WriteLine(accountUpdate);
                        File.AppendAllText(sourceName, accountUpdate + Environment.NewLine);
                    }
                }

                //Save Transaction Summary
                foreach (Account acc in AccountsCollection)
                    acc.Save();
            }
            else
                retVal = false;

            return retVal;
        }

        private int NextUserID()
        {
            int nextID = 0;

            if (!File.Exists(sourceName))
            {
                File.Create(sourceName);
                return 1;
            }

            if (File.ReadLines(sourceName).Count() > 0)
            {
                string line = File.ReadLines(sourceName).Last();
                string[] lastLine = line.Split(',');

                if (lastLine != null)
                {
                    int maxID = Convert.ToInt32(lastLine[3]);
                    nextID = maxID + 1;
                }
            }
            else
            {
                return 1;
            }

            return nextID;
        }

        public String getName()
        {
            return this.FirstName + ", " + this.LastName;
        }

        public int getNumberOfAccounts()
        {
            return AccountsCollection.Count;
        }

        public double getTotalDollarsInAllAccounts()
        {
            double total = 0;

            foreach (Account custAccount in AccountsCollection)
                total += custAccount.AccountBalance;

            return total;
        }

        public void openAccount(int accountType, double accountBalance)
        {
            tmpAccount = new Account();
            tmpAccount.UserID = this.UserID;
            tmpAccount.AccountType = accountType;
            tmpAccount.AccountBalance = accountBalance;
            tmpAccount.Save();

            tmpTransaction = new Transaction();
            tmpTransaction.UserID = this.UserID;
            tmpTransaction.AccountNumber = tmpAccount.AccountNumber;
            tmpTransaction.TransactionType = Transaction.TransType.OpenNewAccount.ToString();
            tmpTransaction.Amount = accountBalance;
            tmpTransaction.Save();

            tmpAccount.TransactionCollection.Add(tmpTransaction);
            AccountsCollection.Add(tmpAccount);
        }

        public double totalInterestEarned()
        {
            double total = 0;

            foreach (Account a in AccountsCollection)
                total += a.totalInterestEarned();

            return total;
        }

        public String getStatement()
        {
            String statement = null;

            statement = "Statement for " + this.getName() + "\n";
            double total = 0.0;
            foreach (Account a in AccountsCollection)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.AccountBalance;
            }
            statement += "\n\nTotal In All Accounts = " + toDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = "";

            switch (a.AccountType)
            {
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.TransactionCollection)
            {
                s += ">  " + t.TransactionType + ((t.TransactionType == "Deposit" || t.TransactionType == "Withdrawal") ? "\t\t" : "\t") + toDollars(t.Amount) + ((toDollars(t.Amount).Length <= 7) ? "\t\t" : "\t") + t.CreateDate + "\n";
                total += t.Amount;
            }
            s += "-------------------------------------------------------------\n";
            s += "Total Balance  = " + toDollars(total);
            return s;
        }

        public String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
