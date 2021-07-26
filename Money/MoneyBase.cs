namespace Банкомат.Money
{
    internal abstract class MoneyBase
    {
        internal virtual decimal ValueMoney { get; } = uint.MaxValue;
        internal const uint CountAllMoneyValue = 5;
    }
}