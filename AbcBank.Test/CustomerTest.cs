using NUnit.Framework;
using System.Linq;
using System.Text;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {
            
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);

            StringBuilder accountStatement = new StringBuilder();

            accountStatement.Append("Statement for Henry\n\nChecking Account\n  deposit $100.00\nTotal $100.00\n\nSavings Account\n  deposit $4,000.00\n  withdrawal $200.00\nTotal $3,800.00\n\nTotal In All Accounts $3,900.00");
            Assert.AreEqual(accountStatement.ToString(), henry.getStatement());
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.Accounts.Count());
        }

        [Test]
        public void testInterestCalculationNoWithdraw()
        {
            Customer bryan = new Customer("Bryan").openAccount(new Account(AccountType.MAXI_SAVINGS));
            bryan.Accounts[0].deposit(1000);
            Assert.AreEqual(50, bryan.totalInterestEarned());
        }

        [Test]
        public void testInterestCalculationWithWithdraw()
        {
            Customer bryan = new Customer("Bryan").openAccount(new Account(AccountType.MAXI_SAVINGS));
            bryan.Accounts[0].deposit(1500);
            bryan.Accounts[0].withdraw(500);
            Assert.AreEqual(10, bryan.totalInterestEarned());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(AccountType.SAVINGS));
            oscar.openAccount(new Account(AccountType.CHECKING));
            Assert.AreEqual(2, oscar.Accounts.Count());
        }

        [Ignore]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(AccountType.SAVINGS));
            oscar.openAccount(new Account(AccountType.CHECKING));
            Assert.AreEqual(3, oscar.Accounts.Count());
        }
    }
}
