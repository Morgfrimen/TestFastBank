namespace Банкомат.Money
{
    internal sealed class Money500 : MoneyBase
    {
#region Overrides of MoneyBase

        /// <inheritdoc />
        internal override decimal ValueMoney { get; } = 500;

#endregion
    }
}