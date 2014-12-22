using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AbcBank
{
    public class Program
    {
        #region FilePaths
        public static string users = @"Data\Customers.txt";
        public static string accounts = @"Data\Accounts.txt";
        public static string transactions = @"Data\Transactions.txt";

        private static string[] userInfo;
        private static Customer Currentuser;
        private static Account tmpAccpunt;
        private static Transaction tmpTransaction;
        private static Bank tmpBank;
        #endregion

        static void Main(string[] args)
        {
            DisplayMainMenu();
        }

        public static void DisplayMainMenu()
        {
            //Logout currentUser if returning from portal.
            Logout();

            string input = "";
            bool validInput = false;
            do
            {
                Console.Clear();
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("\t\t\t      WELCOME TO ABC BANK ");
                Console.WriteLine("*******************************************************************************\n\n ");

                Console.WriteLine("What would you like to  do ?\n");
                Console.WriteLine("1. Existing member? Sign in.");
                Console.WriteLine("2. Don't have an account? Sign up.");
                Console.WriteLine("3. Exit.\n ");

                Console.Write("Please choose one of the 2 options above : ");

                input = Console.ReadLine();

                int inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 3)
                    validInput = true;

            } while (!validInput);


            if (input.Trim() == "1")
            {
                bool validated = false;
                int loopCnt = 0;

                while (!validated)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t     LOGIN ");
                    Console.WriteLine("********************************************************\n");

                    if (loopCnt > 0)
                    {
                        Console.WriteLine("Username and/or password do not match. Please try again\n");
                    }

                    Console.Write("Username : ");
                    string username = Console.ReadLine();
                    Console.Write("Password : ");
                    string password = ReadPassword();

                    if (checkIfUserExists(username, password))
                        validated = true;

                    loopCnt++;
                }

                int currentUserID = 0;
                Int32.TryParse(userInfo[3], out  currentUserID);
                Currentuser = new Customer(currentUserID);

                if (Currentuser.Type == Customer.CustomerType.User.ToString())
                    LaunchCustomerPortal();
                else if (Currentuser.Type == Customer.CustomerType.Employee.ToString())
                    LaunchEmployeePortal();
            }
            else if (input.Trim() == "2") //Create a new User
            {
                Console.Clear();
                bool userNameValid = false;
                int count = 0;
                string username = "";
                string password = "";

                while (!userNameValid)
                {
                    if (count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Username already exists. Please choose a different username.");
                    }


                    Console.Write("Choose a username : ");
                    username = Console.ReadLine();

                    if (!checkIfUserExists(username))
                        userNameValid = true;

                    count++;
                }

                //reset Count
                count = 0;

                do
                {
                    if (count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose a username : " + username);
                    }
                    Console.Write("Password : ");
                    password = ReadPassword();

                    count++;

                } while (password.Trim().Length == 0);

                Console.Write("First Name : ");
                string firstName = Console.ReadLine();
                Console.Write("Last  Name : ");
                string lastName = Console.ReadLine();

                Currentuser = new Customer();
                Currentuser.UserName = username;
                Currentuser.Password = password;
                //userInfo[2] = "" + (Int32)Customer.CustomerType.User;
                Currentuser.Type = Customer.CustomerType.User.ToString();
                Currentuser.FirstName = firstName;
                Currentuser.LastName = lastName;
                Currentuser.Save();

                foreach (Account acc in Currentuser.AccountsCollection)
                    acc.Save();

                LaunchCustomerPortal();
            }
            else if (input.Trim() == "3")
            {
                Environment.Exit(0);
            }
        }

        public static void LaunchCustomerPortal()
        {
            string input = "";
            bool validInput = false;
            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("********************************************************************************\n");

                Console.WriteLine("What would you like to  do ?\n");
                Console.WriteLine("1. View Account Balance \n");
                Console.WriteLine("2. Deposit Funds \n");
                Console.WriteLine("3. Withdraw Cash \n");
                Console.WriteLine("4. Transfer Funds between your accounts \n");
                Console.WriteLine("5. Request an Account Statement \n");
                Console.WriteLine("6. Add a new account \n");
                Console.WriteLine("7. Logout \n\n");

                Console.Write("Please choose one of the options above : ");

                input = Console.ReadLine();

                int inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 7)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                ViewAccountBalance();
            else if (input.Trim() == "2")
                DepositFunds();
            else if (input.Trim() == "3")
                WithdrawCash();
            else if (input.Trim() == "4")
                TransferBetweenAccounts();
            else if (input.Trim() == "5")
                RequestStatement();
            else if (input.Trim() == "6")
                AddNewAccount();
            else if (input.Trim() == "7")
                DisplayMainMenu();
        }

        public static void ViewAccountBalance()
        {
            string input = "";
            bool validInput = false;
            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("********************************************************************************\n ");

                if (Currentuser.AccountsCollection.Count > 0)
                {
                    Console.WriteLine("You currently have " + Currentuser.AccountsCollection.Count + " open with ABC Bank \n");

                    Console.WriteLine("ACCOUNT NO.\t ACCOUNT TYPE \t\t\t BALANCE");
                    Console.WriteLine("--------------------------------------------------------------------------------");

                    foreach (Account acc in Currentuser.AccountsCollection)
                    {
                        string accType = "";
                        switch (acc.AccountType)
                        {
                            case Account.CHECKING:
                                accType = "Checking Account";
                                break;
                            case Account.SAVINGS:
                                accType = "Savings Account";
                                break;
                            case Account.MAXI_SAVINGS:
                                accType = "Maxi Savings Account";
                                break;
                        }

                        Console.WriteLine(" " + acc.AccountNumber + "\t\t " + accType + "\t\t " + Currentuser.toDollars(acc.AccountBalance));
                    }
                }
                else
                {
                    Console.WriteLine("You have not opened any account as yet. Please return to main screen and add a new account first.\n\n");
                }

                Console.WriteLine("\n\n\n");
                Console.WriteLine("1. Return Home");
                Console.WriteLine("2. Logout \n");

                Console.Write("What would you like to do : ");
                input = Console.ReadLine();

                int inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 2)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                LaunchCustomerPortal();
            else if (input.Trim() == "2")
                DisplayMainMenu();

        }

        public static void DepositFunds()
        {
            bool validInput = false;
            string input = "";
            int inputValue;

            if (Currentuser.AccountsCollection.Count > 0)
            {
                Dictionary<int, int> accountMap = new Dictionary<int, int>();
                bool checkingFound = false;
                bool savingsFound = false;
                bool maxiSavingsFound = false;

                int option = 1;
                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountType == Account.CHECKING)
                        checkingFound = true;
                    else if (acc.AccountType == Account.SAVINGS)
                        savingsFound = true;
                    else if (acc.AccountType == Account.MAXI_SAVINGS)
                        maxiSavingsFound = true;

                    accountMap.Add(option, acc.AccountNumber);
                    option++;
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");

                    Console.WriteLine("Which account would you like to deposit funds into ?\n");

                    option = 1;
                    if (checkingFound)
                    {
                        Console.WriteLine(option + ". CHECKING ACCOUNT");
                        option++;
                    }

                    if (savingsFound)
                    {
                        Console.WriteLine(option + ". SAVINGS ACCOUNT");
                        option++;
                    }

                    if (maxiSavingsFound)
                    {
                        Console.WriteLine(option + ". MAXI-SAVINGS ACCOUNT");
                        option++;
                    }

                    Console.Write("\nPlease choose an option from the available accounts : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue < option)
                        validInput = true;

                } while (!validInput);

                Console.WriteLine();
                validInput = false;


                string output = "";
                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountNumber == accountMap[inputValue])
                    {
                        do
                        {
                            Console.Write("\n\n How much money do you want to deposit into this account : ");
                            input = Console.ReadLine();
                            validInput = acc.Deposit(input);

                            if (!validInput)
                                Console.Write("\n" + acc.ValidationMessages);

                        } while (!validInput);

                        output = "Your " + acc.getAccountType() + " was credited with $" + input + " successfully. \nYour current account balance is $" + acc.AccountBalance;
                        break;
                    }
                }

                validInput = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n");

                    Console.WriteLine("> " + output);
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
            else
            {
                do
                {

                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");
                    Console.WriteLine("You don't have any open accounts right now.\nPlease return to Home screen and setup a new account first.\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
        }

        public static void WithdrawCash()
        {
            bool validInput = false;
            string input = "";
            int inputValue;

            if (Currentuser.AccountsCollection.Count > 0)
            {
                Dictionary<int, int> accountMap = new Dictionary<int, int>();
                bool checkingFound = false;
                bool savingsFound = false;
                bool maxiSavingsFound = false;

                int option = 1;
                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountType == Account.CHECKING)
                        checkingFound = true;
                    else if (acc.AccountType == Account.SAVINGS)
                        savingsFound = true;
                    else if (acc.AccountType == Account.MAXI_SAVINGS)
                        maxiSavingsFound = true;

                    accountMap.Add(option, acc.AccountNumber);
                    option++;
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");

                    Console.WriteLine("Which account would you like to withdraw cash from ?\n");

                    option = 1;
                    if (checkingFound)
                    {
                        Console.WriteLine(option + ". CHECKING ACCOUNT");
                        option++;
                    }

                    if (savingsFound)
                    {
                        Console.WriteLine(option + ". SAVINGS ACCOUNT");
                        option++;
                    }

                    if (maxiSavingsFound)
                    {
                        Console.WriteLine(option + ". MAXI-SAVINGS ACCOUNT");
                        option++;
                    }

                    Console.Write("\nPlease choose an option from the available accounts : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue < option)
                        validInput = true;

                } while (!validInput);

                Console.WriteLine();

                string output = "";
                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountNumber == accountMap[inputValue])
                    {
                        validInput = false;

                        do
                        {
                            Console.Write("\n\n How much money do you want to withdraw from this account : ");
                            input = Console.ReadLine();
                            validInput = acc.Withdraw(input);

                            if (!validInput)
                                Console.Write("\n" + acc.ValidationMessages);

                        } while (!validInput);

                        output = "Your " + acc.getAccountType() + " was debited for $" + input + " \nYour current account balance is $" + acc.AccountBalance;

                        break;
                    }
                }


                validInput = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n");

                    Console.WriteLine("> " + output);
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
            else
            {
                do
                {

                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");
                    Console.WriteLine("You don't have any open accounts right now.\nPlease return to Home screen and setup a new account first.\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
        }

        public static void TransferBetweenAccounts()
        {
            bool validInput = false;
            string input = "";
            int inputValue = 0;
            int inputValueTransferFrom = 0;
            int inputValueTransferTo = 0;

            if (Currentuser.AccountsCollection.Count > 1)
            {
                Dictionary<int, Tuple<int, string>> accountMap = new Dictionary<int, Tuple<int, string>>();
                Tuple<int, string> tmpAccInfo;

                int transferFromAccNum = 0;
                int transferToAccNum = 0;
                double transferAmount = 0;

                //Create a Dictionary of available accounts
                int option = 1;
                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountType == Account.CHECKING)
                        tmpAccInfo = new Tuple<int, string>(acc.AccountNumber, "CHECKING ACCOUNT");
                    else if (acc.AccountType == Account.SAVINGS)
                        tmpAccInfo = new Tuple<int, string>(acc.AccountNumber, "SAVINGS ACCOUNT");
                    else //if (acc.AccountType == Account.MAXI_SAVINGS)
                        tmpAccInfo = new Tuple<int, string>(acc.AccountNumber, "MAXI-SAVINGS ACCOUNT");

                    accountMap.Add(option, tmpAccInfo);
                    option++;
                }

                do
                {
                    #region Transfer From Account
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");
                    Console.WriteLine("> Which account would you like to transfer from ?\n");

                    foreach (KeyValuePair<int, Tuple<int, string>> accountInfo in accountMap)
                        Console.WriteLine(accountInfo.Key + ". " + accountMap[accountInfo.Key].Item2);

                    Console.Write("\nPlease choose an option from the available accounts : ");
                    input = Console.ReadLine();

                    inputValueTransferFrom = 0;
                    Int32.TryParse(input, out inputValueTransferFrom);

                    if (inputValueTransferFrom > 0 && inputValueTransferFrom <= accountMap.Count)
                    {
                        validInput = true;
                        transferFromAccNum = accountMap[inputValueTransferFrom].Item1;
                    }
                    #endregion

                    #region Transfer To Account
                    if (validInput)
                    {
                        validInput = false;
                        int loopcount = 0;
                        Console.WriteLine();
                        Console.WriteLine("> Which account would you like to transfer to ?\n");

                        foreach (KeyValuePair<int, Tuple<int, string>> accountInfo in accountMap)
                            if (accountMap[accountInfo.Key].Item1 != transferFromAccNum)
                                Console.WriteLine(accountInfo.Key + ". " + accountMap[accountInfo.Key].Item2);

                        do
                        {
                            if (loopcount > 0)
                                Console.WriteLine(">Not a valid option. ");
                            Console.Write("\nPlease choose an option from the available accounts : ");
                            input = Console.ReadLine();

                            inputValueTransferTo = 0;
                            Int32.TryParse(input, out inputValueTransferTo);

                            if (inputValueTransferTo > 0 && inputValueTransferTo <= accountMap.Count
                                && inputValueTransferTo != inputValueTransferFrom)
                            {
                                validInput = true;
                                transferToAccNum = accountMap[inputValueTransferTo].Item1;
                            }

                            loopcount++;

                        } while (!validInput);
                    }
                    #endregion

                    #region Transfer Amount
                    if (validInput)
                    {
                        validInput = false;
                        int loopcount = 0;
                        string errorMsg = "";

                        Console.WriteLine();

                        do
                        {
                            if (loopcount > 0)
                                Console.WriteLine(errorMsg);
                            Console.Write("> Enter the amount that you want to transfer :");
                            input = Console.ReadLine();

                            transferAmount = 0;
                            Double.TryParse(input, out transferAmount);

                            if (transferAmount <= 0)
                                errorMsg = "**ERROR - Amount must be greater than 0.";
                            else
                            {
                                #region Withdraw
                                foreach (Account tmpAcc in Currentuser.AccountsCollection)
                                {
                                    if (tmpAcc.AccountNumber == accountMap[inputValueTransferFrom].Item1)
                                    {
                                        if (tmpAcc.AccountBalance >= transferAmount)
                                        {
                                            tmpAcc.Withdraw(transferAmount);
                                            validInput = true;
                                        }
                                        else
                                            errorMsg = "**ERROR - The account you are transferring form has $" + tmpAcc.AccountBalance + "\n            Amount cannot be greater than current account balance.";

                                        break;
                                    }
                                }
                                #endregion

                                #region Deposit
                                if (validInput)
                                {
                                    foreach (Account tmpAcc in Currentuser.AccountsCollection)
                                    {
                                        if (tmpAcc.AccountNumber == accountMap[inputValueTransferTo].Item1)
                                        {
                                            tmpAcc.Deposit(transferAmount);
                                            break;
                                        }
                                    }
                                }
                                #endregion
                            }

                            loopcount++;

                        } while (!validInput);
                    }
                    #endregion

                } while (!validInput);

                validInput = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n");

                    Console.WriteLine("$" + transferAmount + " transferred successfully.");
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
            else
            {
                do
                {

                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");
                    Console.WriteLine("You need to have at  least 2 open accounts in order to be able to transer funds.\nPlease return to Home screen and setup a new account first.\n\n\n");
                    Console.WriteLine("1. Return Home");
                    Console.WriteLine("2. Logout \n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();
            }
        }

        public static void RequestStatement()
        {
            bool validInput = false;
            string input = "";
            int inputValue;

            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("************************************************************\n\n ");

                Console.WriteLine(Currentuser.getStatement());

                Console.WriteLine("\n\n\n");
                Console.WriteLine("1. Return Home");
                Console.WriteLine("2. Logout \n");

                Console.Write("What would you like to do : ");
                input = Console.ReadLine();

                inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 2)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                LaunchCustomerPortal();
            else if (input.Trim() == "2")
                DisplayMainMenu();
        }

        public static void AddNewAccount()
        {
            string input = "";
            bool validInput = false;
            bool checkingFound = false;
            bool savingsFound = false;
            bool maxiSavingsFound = false;

            if (Currentuser.AccountsCollection.Count > 0 && Currentuser.AccountsCollection.Count == 3)
            {
                checkingFound = true;
                savingsFound = true;
                maxiSavingsFound = true;

                Console.WriteLine("\n You already have a Checking, Savings & MaxiSavings Account. Cannot add any more accounts.");
                input = Console.ReadLine();

                LaunchCustomerPortal();
            }
            else
            {
                Dictionary<int, int> accountMap;
                int inputValue;
                double amount;

                foreach (Account acc in Currentuser.AccountsCollection)
                {
                    if (acc.AccountType == Account.CHECKING)
                        checkingFound = true;
                    else if (acc.AccountType == Account.SAVINGS)
                        savingsFound = true;
                    else if (acc.AccountType == Account.MAXI_SAVINGS)
                        maxiSavingsFound = true;
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                    Console.WriteLine("************************************************************\n\n ");

                    Console.WriteLine("What type of account would you like to create. \n");
                    accountMap = new Dictionary<int, int>();
                    int option = 1;

                    if (!checkingFound)
                    {
                        Console.WriteLine(option + ". CHECKING ACCOUNT");
                        accountMap.Add(option, Account.CHECKING);
                        option++;
                    }

                    if (!savingsFound)
                    {
                        Console.WriteLine(option + ". SAVINGS ACCOUNT");
                        accountMap.Add(option, Account.SAVINGS);
                        option++;
                    }

                    if (!maxiSavingsFound)
                    {
                        Console.WriteLine(option + ". MAXI-SAVINGS ACCOUNT");
                        accountMap.Add(option, Account.MAXI_SAVINGS);
                        option++;
                    }

                    Console.Write("\nPlease choose an option from the available account types : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue < option)
                        validInput = true;

                } while (!validInput);

                Console.WriteLine();
                validInput = false;
                int iterations = 0;

                do
                {
                    if (iterations > 0)
                        Console.Write("\n> INVALID DOLLAR AMOUNT.");

                    Console.Write("\n\n How much money do you want to deposit into your new account : ");
                    input = Console.ReadLine();

                    amount = 0;
                    Double.TryParse(input, out amount);

                    if (amount > 0)
                        validInput = true;

                    iterations++;

                } while (!validInput);

                Currentuser.openAccount(accountMap[inputValue], amount);

                validInput = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine("*** A new account with $" + amount + " was successfully added ***\n");
                    Console.WriteLine("1. Return Home \n");
                    Console.WriteLine("2. Logout \n\n");

                    Console.Write("What would you like to do : ");
                    input = Console.ReadLine();

                    inputValue = 0;
                    Int32.TryParse(input, out inputValue);

                    if (inputValue > 0 && inputValue <= 2)
                        validInput = true;

                } while (!validInput);

                if (input.Trim() == "1")
                    LaunchCustomerPortal();
                else if (input.Trim() == "2")
                    DisplayMainMenu();

            }
        }

        public static void LaunchEmployeePortal()
        {
            string input = "";
            bool validInput = false;

            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("************************************************************\n\n ");

                Console.WriteLine("What would you like to  do ?\n");
                Console.WriteLine("1. View Customers Report \n");
                Console.WriteLine("2. View Interest Report \n");
                Console.WriteLine("3. Update interest on all acounts [**SHOULD BE RUN ONCE A YEAR ONLY**]\n");
                Console.WriteLine("4. Logout \n\n");

                Console.Write("Please choose one of the options above : ");

                input = Console.ReadLine();

                int inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 4)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                DisplayCustomerSummary();
            else if (input.Trim() == "2")
                DisplayInterestSummary();
            else if (input.Trim() == "3")
                UpdateInterestOnAllAccounts();
            else if (input.Trim() == "4")
                DisplayMainMenu();
        }

        public static void DisplayCustomerSummary()
        {
            tmpBank = new Bank();
            bool validInput = false;
            string input = "";
            int inputValue;

            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("************************************************************\n\n ");

                Console.WriteLine(tmpBank.customerSummary());

                Console.WriteLine("\n\n\n");
                Console.WriteLine("1. Return Home");
                Console.WriteLine("2. Logout \n");

                Console.Write("What would you like to do : ");
                input = Console.ReadLine();

                inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 2)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                LaunchEmployeePortal();
            else if (input.Trim() == "2")
                DisplayMainMenu();
        }

        public static void DisplayInterestSummary()
        {
            tmpBank = new Bank();
            bool validInput = false;
            string input = "";
            int inputValue;

            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("************************************************************\n\n ");

                Console.WriteLine(tmpBank.interestSummary());

                Console.WriteLine("\n\n\n");
                Console.WriteLine("1. Return Home");
                Console.WriteLine("2. Logout \n");

                Console.Write("What would you like to do : ");
                input = Console.ReadLine();

                inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 2)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                LaunchEmployeePortal();
            else if (input.Trim() == "2")
                DisplayMainMenu();
        }

        public static void UpdateInterestOnAllAccounts()
        {
            tmpBank = new Bank();
            bool validInput = false;
            string input = "";
            int inputValue;

            do
            {
                Console.Clear();
                Console.WriteLine(Currentuser.getName().ToUpper() + "\t\t\t\t User ID : " + Currentuser.UserID);
                Console.WriteLine("************************************************************\n\n ");

                tmpBank.updateInterestOnAllAccounts();
                Console.WriteLine("All customer accounts have been updated");

                Console.WriteLine("\n\n\n");
                Console.WriteLine("1. Return Home");
                Console.WriteLine("2. Logout \n");

                Console.Write("What would you like to do : ");
                input = Console.ReadLine();

                inputValue = 0;
                Int32.TryParse(input, out inputValue);

                if (inputValue > 0 && inputValue <= 2)
                    validInput = true;

            } while (!validInput);

            if (input.Trim() == "1")
                LaunchEmployeePortal();
            else if (input.Trim() == "2")
                DisplayMainMenu();
        }


        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);

                        // get the location of the cursor
                        int pos = Console.CursorLeft;

                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);

                        // replace it with space
                        Console.Write(" ");

                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        public static bool checkIfUserExists(string username)
        {
            bool retVal = false;

            if (username.Trim().Length == 0)
                return true;

            if (!File.Exists(users))
            {
                File.Create(users);
                return false;
            }

            using (StreamReader str = new StreamReader(users))
            {
                string line;

                while ((line = str.ReadLine()) != null)
                {
                    string[] usersArray = line.Split(',');

                    if (usersArray != null && usersArray.Length > 0 && usersArray[0].Trim().ToUpper() == username.Trim().ToUpper())
                    {
                        retVal = true;
                        //userInfo = usersArray;
                        break;
                    }
                }
            }

            //if (!retVal)
            //{
            //    //Update array with new account number
            //    userInfo = new string[6];
            //    string[] lastLine = (File.ReadLines(users).Last()).Split(',');

            //    if (lastLine != null && lastLine.Length == 6)
            //    {
            //        userInfo[3] = "" + Convert.ToInt32(lastLine[3]) + 1;
            //    }
            //}

            return retVal;
        }

        public static bool checkIfUserExists(string username, string password)
        {
            bool retVal = false;

            if (!File.Exists(users))
            {
                File.Create(users);
                return retVal;
            }

            using (StreamReader str = new StreamReader(users))
            {
                string line;

                while ((line = str.ReadLine()) != null)
                {
                    string[] usersArray = line.Split(',');

                    if (usersArray != null && usersArray.Length > 0
                        && usersArray[0].Trim().ToUpper() == username.Trim().ToUpper()
                        && usersArray[1].Trim().ToUpper() == password.Trim().ToUpper()
                       )
                    {
                        userInfo = usersArray;
                        retVal = true;
                        break;
                    }
                }
            }

            return retVal;
        }

        public static void Logout()
        {
            userInfo = null;
            Currentuser = null; ;
            tmpAccpunt = null; ;
            tmpTransaction = null;
            tmpBank = null;
        }
    }
}