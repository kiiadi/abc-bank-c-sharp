using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Text;

    public class Bank : IBank, IDisposable
    {
        public Bank(string name_)
        {
            Name = name_;
            Customers = new ConcurrentDictionary<string, ICustomer>();

            // register all possible accounts
            BankAccountFactory<BankAccount>.Register<CheckingBankAccount>(AccountType.Checking);
            BankAccountFactory<BankAccount>.Register<SavingBankAccount>(AccountType.Saving);
            BankAccountFactory<BankAccount>.Register<MaxiSavingBankAccount>(AccountType.MaxiSaving);

            // interest rate terms
            InterestRateFactory<InterestRateTerms>.Register(InterestType.CheckingPrimary, () => new InterestRateTerms(decimal.MaxValue, 0.001));
            InterestRateFactory<InterestRateTerms>.Register(InterestType.SavingPrimary, () => new InterestRateTerms(1000, 0.001, decimal.MaxValue, 0.002));
            InterestRateFactory<InterestRateTerms>.Register(InterestType.MaxiSavingPrimary, () => new InterestRateTerms(1000, 0.02, 2000, 0.05, decimal.MaxValue, 0.10));
            InterestRateFactory<InterestRateTerms>.Register(InterestType.MaxiSavingPenalty, () => new InterestRateTerms(decimal.MaxValue, 0.001));

            // create interest rate terms
            var checkingPrimaryRates = InterestRateFactory<InterestRateTerms>.Create(InterestType.CheckingPrimary);
            var savingPrimaryRates = InterestRateFactory<InterestRateTerms>.Create(InterestType.SavingPrimary);
            var maxiSavingPrimaryRates = InterestRateFactory<InterestRateTerms>.Create(InterestType.MaxiSavingPrimary);
            var maxiSavingPenaltyRates = InterestRateFactory<InterestRateTerms>.Create(InterestType.MaxiSavingPenalty);

            // register rate terms per Account
            InterestRateContainer.GetInstance().Register(AccountType.Checking, checkingPrimaryRates);
            InterestRateContainer.GetInstance().Register(AccountType.Saving, savingPrimaryRates);
            InterestRateContainer.GetInstance().Register(AccountType.MaxiSaving, maxiSavingPrimaryRates, maxiSavingPenaltyRates);
        }

        public void CreateNewCustomer(string fname_, string lname_, string ssn_, string address_, string phone_, string gender_ = "", string title_ = "")
        {
            Debug.Assert(ssn_ != null, "ssn_ != null");
            ICustomer person = new Customer(fname_, lname_, ssn_, address_, phone_, gender_, title_);
            if (Customers.ContainsKey(ssn_)) throw new ArgumentException(string.Format("Failed to create new Customer {0} - reason: already exist", ssn_));

            try
            {
                bool tryAdd = this.Customers.TryAdd(ssn_, person);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public string OpenNewAccount(string ssn_, AccountType type_, DateTime time_, decimal ib_, AccountHistory accountHistory_)
        {
            if (!Customers.ContainsKey(ssn_))
            {
                return null;
            }

            var customer = Customers[ssn_];
            var interstRateTerms = InterestRateContainer.GetInstance().Get(type_);
            var account = BankAccountFactory<BankAccount>.Create(type_, ssn_, time_, ib_, interstRateTerms, accountHistory_);

            customer.Accounts.Add(account);

            return account.OwnerAccountId;
        }

        public string Name { get; set; }

        public ConcurrentDictionary<string, ICustomer> Customers { get; set; }

        public BankAccount GetAccount(string ssn_, string checkingAccountId_)
        {
            var customer = Customers.ContainsKey(ssn_) ? Customers[ssn_] : null;
            if (customer == null) return null;

            var account = customer.Accounts.FirstOrDefault(x_ => x_.OwnerAccountId == checkingAccountId_);

            return account;
        }

        public ICustomer GetCustomer(string ssn_)
        {
            return Customers.ContainsKey(ssn_) ? Customers[ssn_] : null;
        }

        public IDictionary<string, IDictionary<AccountType, int>> GetCustomersAccountsReport(IList<string> customerList_)
        {
            IDictionary<string, IDictionary<AccountType, int>> customerAccountsDict = new Dictionary<string, IDictionary<AccountType, int>>();
            foreach (var ssn in customerList_)
            {
                if (!Customers.ContainsKey(ssn)) continue;
                var customer = Customers[ssn];
                var accounts = customer.Accounts;
                foreach (var account in accounts)
                {
                    var ownerId = account.OwnerAccountId;
                    if (!customerAccountsDict.ContainsKey(ownerId))
                    {
                        customerAccountsDict.Add(ownerId, new Dictionary<AccountType, int>());
                    }

                    var customerAccounts = customerAccountsDict[ownerId];
                    var accountType = account.AccountType;
                    if (!customerAccounts.ContainsKey(accountType))
                    {
                        customerAccounts.Add(accountType, 1);
                    }
                    else
                    {
                        customerAccounts[accountType]++;
                    }
                }
            }

            return customerAccountsDict;
        }

        public double GetTotalInterestRatePaidReport()
        {
            return this.Customers.Values.Sum(customer_ => (double)customer_.TotalInterestEarned());
        }

        public IDictionary<string, IDictionary<AccountType, AccountHistory>> GetCustomerStatementReport(string ssn_)
        {
            IDictionary<string, IDictionary<AccountType, AccountHistory>> customerAccountsDict = new Dictionary<string, IDictionary<AccountType, AccountHistory>>();

            if (!Customers.ContainsKey(ssn_)) return null;

            var customer = Customers[ssn_];
            var accounts = customer.Accounts;

            foreach (var account in accounts)
            {
                var ownerId = account.OwnerAccountId;
                if (!customerAccountsDict.ContainsKey(ownerId))
                {
                    customerAccountsDict.Add(ownerId, new Dictionary<AccountType, AccountHistory>());
                }

                var customerAccounts = customerAccountsDict[ownerId];
                var accountType = account.AccountType;
                if (!customerAccounts.ContainsKey(accountType))
                {
                    customerAccounts.Add(accountType, account.ActionsHistory);
                }
            }

            return customerAccountsDict;
        }

        public string GetGlobalAccountsReport()
        {
            var sb = new StringBuilder();

            foreach (var customerAccount in this.Customers.Values.Select(customer_ => customer_.TotalAccountsStatement()))
            {
                sb.Append(customerAccount).Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            Customers.Clear();
        }
    }
}
