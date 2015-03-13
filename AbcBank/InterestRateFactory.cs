using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class InterestRateFactory<T>
    {
        private InterestRateFactory() { }

        static readonly Dictionary<InterestType, Func<T>> Dict
             = new Dictionary<InterestType, Func<T>>();

        public static T Create(InterestType id_)
        {
            Func<T> constructor = null;
            if (Dict.TryGetValue(id_, out constructor))
                return constructor();

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(InterestType id_, Func<T> ctor_)
        {
            if (!Dict.ContainsKey(id_))
                Dict.Add(id_, ctor_);
        }
    }
}
