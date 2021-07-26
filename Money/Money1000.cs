namespace Банкомат.Money
{
    internal sealed class Money1000 : MoneyBase
    {
#region Overrides of MoneyBase

        /// <inheritdoc />
        internal override decimal ValueMoney { get; } = 1000;

#endregion
    }
}