using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface ICustomerStatement
    {
        string Generate(string customerName, IEnumerable<IAccount> accounts);
    }

    public class CustomerStatement : ICustomerStatement
    {
        public string Generate(string customerName, IEnumerable<IAccount> accounts)
        {
            StringBuilder statement = new StringBuilder("Statement for " + customerName);
            statement.Append(Environment.NewLine);

            double total = 0.0;
            foreach (var a in accounts)
            {
                statement.Append(Environment.NewLine);
                statement.AppendLine(a.Name);
                statement.AppendLine(a.TransactionsStatement);
                statement.AppendLine("Total " + a.CurrentAmount.ToDollarString());
                total += a.CurrentAmount;
            }

            statement.Append(Environment.NewLine);
            statement.Append("Total In All Accounts " + total.ToDollarString());

            return statement.ToString();
        }
    }
}
