namespace Банкомат.Money
{
    internal sealed class Money20 : MoneyBase
    {
#region Overrides of MoneyBase

        /// <inheritdoc />
        internal override decimal ValueMoney { get; } = 20;

#endregion
    }
}