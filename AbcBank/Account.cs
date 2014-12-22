using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        #region Accounts
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        //private readonly int accountType;

        private int _accountNumber = 0;
        public int AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        private int _accountType = 0;
        public int AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        private int _userID = 0;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private double _accountBal = 0;
        public double AccountBalance
        {
            get { return _accountBal; }
            set { _accountBal = value; }
        }

        private DateTime? _CreateDate = null;
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private string sourceName = @"Data\Accounts.txt";

        private Transaction tmpTransaction;
        public List<int> TransactionIDs = new List<int>();
        public List<Transaction> TransactionCollection = new List<Transaction>();

        public string ValidationMessages = "";
        #endregion

        public Account()
        {
            this.AccountNumber = NextAccountID();
        }

        public Account(int accountNumber)
        {
            this.AccountNumber = accountNumber;
            Load();
        }

        public void Load()
        {
            Load(this.AccountNumber);
        }

        public void Load(int AccntNum)
        {
            bool accountFound = false;

            if (AccntNum > 0)
            {
                if (File.Exists(sourceName))
                {
                    string line;
                    using (StreamReader str = new StreamReader(sourceName))
                    {
                        while ((line = str.ReadLine()) != null)
                        {
                            string[] strArray = line.Split(',');
                            if (strArray != null && strArray.Length == 5)
                            {
                                int tmpID = 0;
                                Int32.TryParse(strArray[0], out tmpID);

                                if (tmpID == AccntNum)
                                {
                                    this.AccountNumber = tmpID;
                                    Int32.TryParse(strArray[1], out this._accountType);
                                    Int32.TryParse(strArray[2], out this._userID);
                                    Double.TryParse(strArray[3], out this._accountBal);
                                    if (strArray[4] != null)
                                        this.CreateDate = Convert.ToDateTime(strArray[4].ToString());

                                    accountFound = true;

                                    //Load Account Details
                                    tmpTransaction = new Transaction();
                                    TransactionIDs.Clear();
                                    TransactionCollection.Clear();
                                    TransactionIDs = tmpTransaction.getTransactionIDsByAccount(this.AccountNumber);

                                    foreach (int transID in TransactionIDs)
                                    {
                                        tmpTransaction = new Transaction(transID);
                                        TransactionCollection.Add(tmpTransaction);
                                    }

                                    break;
                                }
                            }
                        }

                    }

                    if (!accountFound)
                        ResetToDefaultValues();
                }
            }

        }

        public void ResetToDefaultValues()
        {
            _accountNumber = 0;
            _accountType = 0;
            _userID = 0;
            _accountBal = 0;
            _CreateDate = null;

            TransactionIDs.Clear();
            TransactionCollection.Clear();
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
                accountUpdate += this.AccountNumber.ToString();
                accountUpdate += "," + this.AccountType.ToString();
                accountUpdate += "," + this.UserID.ToString();
                accountUpdate += "," + this.AccountBalance.ToString();

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
                    //If account exists - Update
                    List<string> lines = new List<string>(File.ReadAllLines(sourceName));
                    int lineIndex = lines.FindIndex(line => line.StartsWith(this.AccountNumber.ToString() + ","));
                    if (lineIndex != -1)
                    {
                        accountUpdate += "," + this.CreateDate;
                        lines[lineIndex] = accountUpdate;
                        File.WriteAllLines(sourceName, lines);
                    }
                    else //Else create a new account.
                    {
                        this.CreateDate = DateTime.Now;
                        accountUpdate += "," + this.CreateDate;

                        File.AppendAllText(sourceName, accountUpdate + Environment.NewLine);
                    }
                }

                ////Save Transaction Summary
                //foreach (Transaction t in TransactionCollection)
                //    t.Save();
            }
            else
                retVal = false;

            return retVal;
        }

        public List<int> getAccountNumbersByUser(int userID)
        {
            List<int> accountNumbersList = new List<int>();

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
                            if (strArray != null && strArray.Length == 5)
                            {
                                int tmpUserID = 0;
                                Int32.TryParse(strArray[2], out tmpUserID);

                                if (tmpUserID == userID)
                                {
                                    int tmpAccNum;
                                    Int32.TryParse(strArray[0], out tmpAccNum);

                                    if (tmpAccNum > 0)
                                        accountNumbersList.Add(tmpAccNum);
                                }
                            }
                        }
                    }
                }
            }

            return accountNumbersList;
        }

        public bool Deposit(double amount)
        {
            bool retVal = true;
            ValidationMessages = "";

            if (amount <= 0)
            {
                ValidationMessages += "- Amount must be greater than zero\n";
                retVal = false;
            }

            if (retVal)
            {
                this.AccountBalance = this.AccountBalance + amount;
                Save();

                tmpTransaction = new Transaction();
                tmpTransaction.UserID = this.UserID;
                tmpTransaction.AccountNumber = this.AccountNumber;
                tmpTransaction.TransactionType = Transaction.TransType.Deposit.ToString();
                tmpTransaction.Amount = amount;
                tmpTransaction.Save();
                this.TransactionCollection.Add(tmpTransaction);
            }

            return retVal;
        }

        public bool Deposit(string strAmnt)
        {
            bool retVal = true;
            double amount = 0;
            ValidationMessages = "";

            Double.TryParse(strAmnt, out amount);

            if (amount <= 0)
            {
                ValidationMessages += "ERROR - Invalid amount. Amount must be greater than zero.";
                retVal = false;
            }

            if (retVal)
            {
                this.AccountBalance = this.AccountBalance + amount;
                Save();

                tmpTransaction = new Transaction();
                tmpTransaction.UserID = this.UserID;
                tmpTransaction.AccountNumber = this.AccountNumber;
                tmpTransaction.TransactionType = Transaction.TransType.Deposit.ToString();
                tmpTransaction.Amount = amount;
                tmpTransaction.Save();
                this.TransactionCollection.Add(tmpTransaction);
            }

            return retVal;
        }

        public bool Withdraw(double amount)
        {
            bool retVal = true;
            ValidationMessages = "";

            if (amount <= 0)
            {
                ValidationMessages += "ERROR - Amount must be greater than zero.";
                retVal = false;
            }
            else if (amount > this.AccountBalance)
            {
                ValidationMessages += "ERROR - Amount must be less than total account balance. Current account balance is $" + this.AccountBalance;
                retVal = false;
            }

            if (retVal)
            {
                this.AccountBalance = this.AccountBalance - amount;
                Save();

                tmpTransaction = new Transaction();
                tmpTransaction.UserID = this.UserID;
                tmpTransaction.AccountNumber = this.AccountNumber;
                tmpTransaction.TransactionType = Transaction.TransType.Withdrawal.ToString();
                tmpTransaction.Amount = amount * -1;
                tmpTransaction.Save();

                this.TransactionCollection.Add(tmpTransaction);
            }

            return retVal;
        }

        public bool Withdraw(string strAmount)
        {
            bool retVal = true;
            double amount = 0;
            ValidationMessages = "";

            Double.TryParse(strAmount, out amount);

            if (amount <= 0)
            {
                ValidationMessages += "ERROR - Invalid amount. Amount must be greater than zero.";
                retVal = false;
            }
            else if (amount > this.AccountBalance)
            {
                ValidationMessages += "ERROR - Amount must be less than total account balance. Current account balance is $" + this.AccountBalance;
                retVal = false;
            }

            if (retVal)
            {
                this.AccountBalance = this.AccountBalance - amount;
                Save();

                tmpTransaction = new Transaction();
                tmpTransaction.UserID = this.UserID;
                tmpTransaction.AccountNumber = this.AccountNumber;
                tmpTransaction.TransactionType = Transaction.TransType.Withdrawal.ToString();
                tmpTransaction.Amount = amount * -1;
                tmpTransaction.Save();

                this.TransactionCollection.Add(tmpTransaction);
            }

            return retVal;
        }

        public void updateAccountInterest()
        {
            double interestOnCurrentBalance = calculateInterestOnCurrentBalance();
            this.AccountBalance = this.AccountBalance + interestOnCurrentBalance;
            Save();

            tmpTransaction = new Transaction();
            tmpTransaction.UserID = this.UserID;
            tmpTransaction.AccountNumber = this.AccountNumber;
            tmpTransaction.TransactionType = Transaction.TransType.InterestAdded.ToString();
            tmpTransaction.Amount = interestOnCurrentBalance;
            tmpTransaction.Save();

            this.TransactionCollection.Add(tmpTransaction);
        }

        private double calculateInterestOnCurrentBalance()
        {
            double interesetEarned = 0;

            if (this.AccountBalance > 0)
            {
                switch (AccountType)
                {
                    case CHECKING:
                        interesetEarned = AccountBalance * 0.001;
                        break;
                    case SAVINGS:
                        if (AccountBalance > 1000)
                            interesetEarned = 1 + ((AccountBalance - 1000) * 0.002);
                        else
                            interesetEarned = AccountBalance * 0.001;
                        break;
                    case MAXI_SAVINGS:
                        if (AccountBalance > 2000)
                            interesetEarned = 70 + ((AccountBalance - 2000) * 0.1);
                        else if (AccountBalance > 1000)
                            interesetEarned = 20 + ((AccountBalance - 1000) * 0.05);
                        else
                            interesetEarned = AccountBalance * 0.02;
                        break;
                }
            }

            return interesetEarned;
        }

        public double totalInterestEarned()
        {
            double total = 0;

            foreach (Transaction trans in TransactionCollection)
                if (trans.TransactionType == Transaction.TransType.InterestAdded.ToString())
                    total += trans.Amount;

            return total;
        }

        public string getAccountType()
        {
            string AccType = "";

            if (AccountType == Account.CHECKING)
                return "CHECKING";
            else if (AccountType == Account.SAVINGS)
                return "SAVINGS";
            else if (AccountType == Account.MAXI_SAVINGS)
                return "MAXI_SAVINGS";

            return AccType;
        }

        private int NextAccountID()
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
                    int maxID = Convert.ToInt32(lastLine[0]);
                    nextID = maxID + 1;
                }
            }
            else
            {
                return 1;
            }

            return nextID;
        }
    }
}
