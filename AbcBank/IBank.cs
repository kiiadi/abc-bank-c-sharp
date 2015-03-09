namespace AbcBank
{
    using System.Collections.Concurrent;

    public interface IBank
    {
        string Name { get; set; }
        ConcurrentDictionary<string, ICustomer> Customers { get; set; }
    }
}