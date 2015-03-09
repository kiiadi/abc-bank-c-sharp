namespace AbcBank
{
    public interface ICustomer
    {
        ConcurrentList<BankAccount> Accounts { get; set; }

        void InternalTransfer(BankAccount fromAccount_, BankAccount toAccount_, decimal amount_);

        int NumberOfAccounts();

        decimal TotalInterestEarned();

        string TotalAccountsStatement();
    }
}