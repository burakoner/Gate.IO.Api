namespace Gate.IO.Api.Converters;

internal class FuturesSettleConverter : BaseConverter<FuturesSettle>
{
    public FuturesSettleConverter() : this(true) { }
    public FuturesSettleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesSettle, string>> Mapping => new()
    {
        new KeyValuePair<FuturesSettle, string>(FuturesSettle.BTC, "btc"),
        new KeyValuePair<FuturesSettle, string>(FuturesSettle.USD, "usd"),
        new KeyValuePair<FuturesSettle, string>(FuturesSettle.USDT, "usdt"),
    };
}