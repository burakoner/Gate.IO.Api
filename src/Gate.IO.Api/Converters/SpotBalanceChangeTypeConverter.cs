namespace Gate.IO.Api.Converters;

internal class SpotBalanceChangeTypeConverter : BaseConverter<SpotBalanceChangeType>
{
    public SpotBalanceChangeTypeConverter() : this(true) { }
    public SpotBalanceChangeTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotBalanceChangeType, string>> Mapping => new()
    {
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.Deposit, "deposit"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.Withdraw, "withdraw"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.TradeFeeDeduct, "trade-fee-deduct"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.OrderCreate, "order-create"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.OrderMatch, "order-match"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.OrderUpdate, "order-update"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.MarginTransfer, "margin-transfer"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.FutureTransfer, "future-transfer"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.CrossMargin, "cross-margin"),
        new KeyValuePair<SpotBalanceChangeType, string>(SpotBalanceChangeType.Other, "other"),
    };
}