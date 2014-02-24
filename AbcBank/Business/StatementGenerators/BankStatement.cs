using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface IBankStatement
    {
        string Generate(IEnumerable<ICustomer> customers);
    }

    public class BankStatement : IBankStatement
    {
        public string Generate(IEnumerable<ICustomer> customers)
        {
            StringBuilder summary = new StringBuilder("Customer Summary");
            summary.Append(Environment.NewLine);
            string newLine = string.Empty;
            foreach (var c in customers)
            {
                summary.Append(newLine)
                    .Append(string.Format(" - {0} ({1} account{2})", c.Name, c.NumberOfAccounts, c.NumberOfAccounts > 1 ? "s" : ""));
                newLine = Environment.NewLine;
            }
            return summary.ToString();
        }
    }
}
