using System.Collections.Generic;

using Банкомат.Money;

namespace Банкомат.Data
{
    internal interface IData
    {
        public IDictionary<MoneyBase, uint> GetMoney();
    }
}