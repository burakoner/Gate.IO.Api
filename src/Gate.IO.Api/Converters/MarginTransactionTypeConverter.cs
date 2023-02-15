namespace Gate.IO.Api.Converters;

internal class MarginTransactionTypeConverter : BaseConverter<MarginTransactionType>
{
    public MarginTransactionTypeConverter() : this(true) { }
    public MarginTransactionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarginTransactionType, string>> Mapping => new()
    {
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.TransferIn, "in"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.TransferOut, "out"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.Repay, "repay"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.Borrow, "borrow"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.Interest, "interest"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.NewOrder, "new_order"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.OrderFill, "order_fill"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.ReferralFee, "referral_fee"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.OrderFee, "order_fee"),
        new KeyValuePair<MarginTransactionType, string>(MarginTransactionType.Unknown, "unknown"),
    };
}