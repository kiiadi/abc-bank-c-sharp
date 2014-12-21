using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        #region Properties
        private int _transID = 0;
        public int TransactionID
        {
            get
            {
                return _transID;
            }
            set
            {
                _transID = value;
            }
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

        private int _accountNumber = 0;
        public int AccountNumber
        {
            get
            {
                return _accountNumber;
            }
            set
            {
                _accountNumber = value;
            }
        }

        public enum TransType
        {
            Unknown = 0,
            OpenNewAccount = 1,
            Deposit = 2,
            Withdrawal = 3,
            InterestAdded = 4
        }

        private string _transactionType = "";
        public string TransactionType
        {
            get { return _transactionType; }
            set { _transactionType = value; }
        }

        private double _amount = 0;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        private DateTime? _CreateDate = null;
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private string sourceName = @"Data\Transactions.txt";
        #endregion

        public Transaction()
        {
            //this.TransactionID = NextTransID();
        }

        public Transaction(int transID)
        {
            this.TransactionID = transID;
            Load();
        }

        public void Load()
        {
            Load(this.TransactionID);
        }

        public void Load(int TransID)
        {
            bool transactionFound = false;

            if (TransID > 0)
            {
                if (File.Exists(sourceName))
                {
                    string line;
                    using (StreamReader str = new StreamReader(sourceName))
                    {
                        while ((line = str.ReadLine()) != null)
                        {
                            string[] strArray = line.Split(',');
                            if (strArray != null && strArray.Length == 6)
                            {
                                int tmpID = 0;
                                Int32.TryParse(strArray[0], out tmpID);

                                if (tmpID == TransID)
                                {
                                    this.TransactionID = tmpID;
                                    Int32.TryParse(strArray[1], out this._userID);
                                    Int32.TryParse(strArray[2], out this._accountNumber);

                                    int transType;
                                    Int32.TryParse(strArray[3], out transType);

                                    if (transType == (Int32)Transaction.TransType.OpenNewAccount)
                                        this.TransactionType = Transaction.TransType.OpenNewAccount.ToString();
                                    else if (transType == (Int32)Transaction.TransType.Deposit)
                                        this.TransactionType = Transaction.TransType.Deposit.ToString();
                                    else if (transType == (Int32)Transaction.TransType.Withdrawal)
                                        this.TransactionType = Transaction.TransType.Withdrawal.ToString();
                                    else if (transType == (Int32)Transaction.TransType.InterestAdded)
                                        this.TransactionType = Transaction.TransType.InterestAdded.ToString();
                                    if (transType == (Int32)Transaction.TransType.Unknown)
                                        this.TransactionType = Transaction.TransType.Unknown.ToString();

                                    Double.TryParse(strArray[4], out this._amount);

                                    if (strArray[5] != null)
                                        this.CreateDate = Convert.ToDateTime(strArray[5].ToString());

                                    transactionFound = true;

                                    break;
                                }
                            }
                        }
                    }

                    if (!transactionFound)
                        ResetToDefaultValues();
                }
            }
        }

        public void ResetToDefaultValues()
        {
            _transID = 0;
            _userID = 0;
            _accountNumber = 0;
            _transactionType = "";
            _amount = 0;
            _CreateDate = null;
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
                this.TransactionID = NextTransID();

                accountUpdate += this.TransactionID.ToString();
                accountUpdate += "," + this.UserID.ToString();
                accountUpdate += "," + this.AccountNumber.ToString();

                if (this.TransactionType == Transaction.TransType.OpenNewAccount.ToString())
                    accountUpdate += "," + (Int32)Transaction.TransType.OpenNewAccount;
                else if (this.TransactionType == Transaction.TransType.Deposit.ToString())
                    accountUpdate += "," + (Int32)Transaction.TransType.Deposit;
                else if (this.TransactionType == Transaction.TransType.Withdrawal.ToString())
                    accountUpdate += "," + (Int32)Transaction.TransType.Withdrawal;
                else if (this.TransactionType == Transaction.TransType.InterestAdded.ToString())
                    accountUpdate += "," + (Int32)Transaction.TransType.InterestAdded;
                else
                    accountUpdate += "," + (Int32)Transaction.TransType.Unknown;

                accountUpdate += "," + this.Amount.ToString();

                this.CreateDate = DateTime.Now;
                accountUpdate += "," + this.CreateDate;

                if (!File.Exists(sourceName))
                    File.Create(sourceName);

                //using (StreamWriter str = new StreamWriter(sourceName))
                //    str.WriteLine(accountUpdate);

                File.AppendAllText(sourceName, accountUpdate + Environment.NewLine);
            }

            return retVal;
        }

        public List<int> getTransactionIDsByAccount(int accountNumber)
        {
            List<int> transIDList = new List<int>();

            if (accountNumber > 0)
            {
                if (File.Exists(sourceName))
                {
                    string line;
                    using (StreamReader str = new StreamReader(sourceName))
                    {
                        while ((line = str.ReadLine()) != null)
                        {
                            string[] strArray = line.Split(',');
                            if (strArray != null && strArray.Length == 6)
                            {
                                int tmpAccountNumber = 0;
                                Int32.TryParse(strArray[2], out tmpAccountNumber);

                                if (tmpAccountNumber == accountNumber)
                                {
                                    int tmpTransactionNum;
                                    Int32.TryParse(strArray[0], out tmpTransactionNum);

                                    if (tmpTransactionNum > 0)
                                        transIDList.Add(tmpTransactionNum);
                                }
                            }
                        }
                    }
                }
            }

            return transIDList;
        }

        private int NextTransID()
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

