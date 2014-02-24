using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class When_asking_for_an_account_statement
    {
        [Test]
        public void it_should_produce_a_statement()
        {
            var henry = new Customer("Henry").
                openAccount(new Account(AccountType.Checking, "checking")).
                openAccount(new Account(AccountType.Savings, "savings"));

            henry.deposit("checking", 100.0);
            henry.withdraw("checking", 80.0);
            henry.deposit("savings", 4000.0);
            henry.withdraw("savings", 200.0);

            var result = new AccountStatement().getStatement(henry.getName(), henry.getAccounts());

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  withdrawal $80.00\n" +
                    "Total $20.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,820.00", result);
        }

    }
}
