using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using Банкомат.Money;

namespace Банкомат.Data
{
    internal sealed class TXTData : IData
    {
        private readonly string _path;

        private MoneyBase GetMoneyBase(uint value)
        {
            MoneyBase money = value switch
            {
                20  => new Money20(), 50    => new Money50(), 100 => new Money100(),
                500 => new Money500(), 1000 => new Money1000(),
                _   => throw new NotImplementedException("Значение купюры не определенно")
            };

            return money;
        }

        internal TXTData(string pathFolder) => _path = pathFolder;
        private const string NameFile = "ATM.txt";
        internal TXTData() => _path = Path.Combine(Environment.CurrentDirectory, NameFile);
        private const string Pattern = @"(\d+):(\d+)";

#region Implementation of IData

        /// <inheritdoc />
        public IDictionary<MoneyBase, uint> GetMoney()
        {
            IDictionary<MoneyBase, uint> cacheData = new Dictionary<MoneyBase, uint>();

            using (StreamReader streamReader = new(_path))
            {
                string text = streamReader.ReadToEnd();
                MatchCollection mathes = Regex.Matches
                    (text, Pattern, RegexOptions.Compiled & RegexOptions.IgnoreCase);

                foreach (Match mathe in mathes)
                {
                    string key = mathe.Groups[1].Value;
                    string count = mathe.Groups[2].Value;
                    MoneyBase money = GetMoneyBase(Convert.ToUInt32(key));
                    cacheData.Add(money, Convert.ToUInt32(count));
                }
            }

            return cacheData;
        }

#endregion
    }
}