namespace Банкомат.Money
{
    internal sealed class Money100 : MoneyBase
    {
#region Overrides of MoneyBase

        /// <inheritdoc />
        internal override decimal ValueMoney { get; } = 100;

#endregion
    }
}