using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class BankAccountFactory<T>
    {
        private static int count = 1;

        private BankAccountFactory() { }

        static readonly Dictionary<AccountType, Type> Dict = new Dictionary<AccountType, Type>();

        public static T Create(AccountType id_, string owner_, DateTime time_, decimal ib_, Tuple<InterestRateTerms, InterestRateTerms> primarySecondaryIrs_, AccountHistory accountHistory_)
        {
            Type type = null;
            if (Dict.TryGetValue(id_, out type))
            {
                var ownerAccountId = owner_ + "_" + ((int)id_ * 1000 + count++).ToString("D4");
                var t = (T)Activator.CreateInstance(type, time_, ownerAccountId, ib_, primarySecondaryIrs_, accountHistory_);
                return t;
            }

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register<TDerived>(AccountType id_) where TDerived : T
        {
            var type = typeof(TDerived);

            if (type.IsInterface || type.IsAbstract)
                throw new ArgumentException("Type is Interface or Abstract, use concrete type ...");

            if (!Dict.ContainsKey(id_))
                Dict.Add(id_, type);
        }
    }
}
