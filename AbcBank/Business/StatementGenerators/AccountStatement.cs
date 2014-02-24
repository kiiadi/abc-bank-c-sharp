using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbcBank.Application;

namespace AbcBank.Business
{
    public interface IAccountStatement
    {
        string Generate(IEnumerable<ITransaction> transactions);
    }

    public class AccountStatement : IAccountStatement
    {
        public string Generate(IEnumerable<ITransaction> transactions)
        {
            StringBuilder statement = new StringBuilder();
            string newLine = string.Empty;
            foreach (var t in transactions)
            {
                statement.Append(newLine)
                        .Append(string.Format("  {0} {1}", t.Type, t.Amount.ToDollarString()));
                newLine = Environment.NewLine;
            }
            return statement.ToString();
        }
    }
}
