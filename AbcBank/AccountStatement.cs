using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class AccountStatement
    {

        public String getStatement(string name, List<IAccount> accounts)
        {
            var statement = String.Format("Statement for {0}\n", name);
            var total = 0.0;
            foreach (var a in accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

        private String statementForAccount(IAccount a)
        {
            var s = String.Format("{0}\n", getAccountTypeName(a));

            foreach (Transaction t in a.getTransactions())
            {
                s += String.Format("  {0} {1}\n", (t.Amount < 0 ? "withdrawal" : "deposit"), toDollars(t.Amount));
            }
            s += String.Format("Total {0}", toDollars(a.sumTransactions()));
            return s;
        }

        private string getAccountTypeName(IAccount a)
        {
            switch (a.getAccountType())
            {
                case AccountType.Checking:
                    return "Checking Account";
                case AccountType.Savings:
                    return "Savings Account";
                case AccountType.MaxiSavings:
                    return "Maxi-Savings Account";
            }
            return null;
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
