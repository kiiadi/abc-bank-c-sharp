using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        private readonly double ANNUAL_INTEREST_RATE1 = 0.001d;
        private readonly double ANNUAL_INTEREST_RATE2 = 0.002d;
        private readonly double ANNUAL_INTEREST_RATE3 = 0.05d;
        public readonly double MAX_OVERDRAFT_AMOUNT = 0d;
        public const string INVALID_TRANSACTION_AMOUNT = "Amount must be greater than zero";
        public const string OVERDRAFT_ERROR = "Overdraft Error";        
        private readonly List<Transaction> transactions = new List<Transaction>();        
        public Account (AccountType accountType, IEnumerable<Transaction> transactions = null) 
        {   
            AccountType = accountType;
            if (transactions != null)
                this.transactions.AddRange(transactions);
        }
        public void Deposit(double amount)
        {
            if (amount <= 0d)
            {
                throw new ArgumentException(INVALID_TRANSACTION_AMOUNT);
            }
            else
            {
                this.transactions.Add(new Transaction(Utility.GetCurrentDate(), amount));
            }
        }
        public void Withdraw(double amount)
        {
            if (amount <= 0d)
            {
                throw new ArgumentException(INVALID_TRANSACTION_AMOUNT);
            }
            else
            {
                var balance = GetCurrentBalance();
                if (balance - amount < MAX_OVERDRAFT_AMOUNT)
                    throw new Exception(OVERDRAFT_ERROR);

                this.transactions.Add(new Transaction(Utility.GetCurrentDate(), -amount));
            }
        }
        public IEnumerable<Transaction> Transactions
        {
            get { return this.transactions; }
        }
        public double GetSumOfTransactions()
        {
            return this.transactions.Sum(t => t.Amount);
        }
        public double GetCurrentBalance()
        {
            return GetSumOfTransactions() + GetEarnedInterest();
        }
        public string GetStatement()
        {
            var statement = new StringBuilder();
            statement.AppendLine(Title);
            double total = 0.0;
            foreach (var t in this.transactions)
            {
                statement.AppendLine("  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + Utility.ToDollars(t.Amount));
                total += t.Amount;
            }
            statement.Append("Total " + Utility.ToDollars(total));
            return statement.ToString();
        }
        public double GetEarnedInterest(DateTime? fromDate = null, DateTime? toDate = null)
        {
            double totalInterest = 0d;
            if (this.transactions.Any())
            {
                if (!fromDate.HasValue)
                    fromDate = this.transactions.Min(t => t.Date);

                if (!toDate.HasValue)
                    toDate = DateTime.Today;

                var dailySumsDictionary = GetDailySumsMapping(fromDate.Value, toDate.Value);
               
                double totalPrincipal = 0d;
                var currentDate = fromDate.Value;
                while (currentDate < toDate.Value)
                {
                    if (dailySumsDictionary.ContainsKey(currentDate))
                        totalPrincipal += dailySumsDictionary[currentDate];

                    var dailyInterest = CalculateDailyInterest(totalPrincipal, currentDate);      
              
                    totalInterest += dailyInterest;
                    totalPrincipal += dailyInterest;

                    currentDate = currentDate.AddDays(1);
                }
            }

            return totalInterest;
        }

        private Dictionary<DateTime, double> GetDailySumsMapping(DateTime fromDate, DateTime toDate)
        {
            return (from t in this.transactions
                                       where t.Date >= fromDate && t.Date <= toDate
                                       select t).GroupBy(t => t.Date.Date).ToDictionary(i => i.Key, v => v.Sum(t => t.Amount));
        }

        private double CalculateDailyInterest(double totalPrincipal, DateTime currentDate)
        {
            switch (AccountType)
            {
                case AccountType.Savings:
                    return SavingsDailyInterestCalculator(totalPrincipal);
                case AccountType.MaxiSavings:
                    return MaxiSavingsDailyInterestCalculator(totalPrincipal, currentDate);
                default:
                    return CheckingDailyInterestCalculator(totalPrincipal);
            }            
        }
        private double MaxiSavingsDailyInterestCalculator(double totalPrincipal, DateTime date)
        {
            if (this.transactions.Any(t => t.Amount < 0d && (date.Subtract(t.Date).TotalDays <= 10)))
                return totalPrincipal * (ANNUAL_INTEREST_RATE1 / 365);
            else
            {
                return totalPrincipal * (ANNUAL_INTEREST_RATE3 / 365);
            }
        }
        private double CheckingDailyInterestCalculator(double totalPrincipal)
        {
            return totalPrincipal * (ANNUAL_INTEREST_RATE1 / 365);
        }
        private double SavingsDailyInterestCalculator(double totalPrincipal)
        {
            if (totalPrincipal <= 1000d)
                return totalPrincipal * (ANNUAL_INTEREST_RATE1 / 365);
            else
            {
                return 1000d * (ANNUAL_INTEREST_RATE1 / 365)
                              + (totalPrincipal - 1000d) * (ANNUAL_INTEREST_RATE2 / 365);
            }
        }
        public string Title
        {
            get
            {
                switch (AccountType)
                {
                    case AccountType.Checking:
                        return "Checking Account";
                    case AccountType.Savings:
                        return "Savings Account";
                    case AccountType.MaxiSavings:
                        return "Maxi Savings Account";
                    default:
                        return "[Invalid Account Type]";
                }
            }
        }
        public AccountType AccountType { get; private set; }
    }
}
