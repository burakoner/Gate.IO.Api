namespace Gate.IO.Api.Converters;

internal class FuturesPerpetualSettleConverter : BaseConverter<FuturesPerpetualSettle>
{
    public FuturesPerpetualSettleConverter() : this(true) { }
    public FuturesPerpetualSettleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesPerpetualSettle, string>> Mapping => new()
    {
        new KeyValuePair<FuturesPerpetualSettle, string>(FuturesPerpetualSettle.BTC, "btc"),
        new KeyValuePair<FuturesPerpetualSettle, string>(FuturesPerpetualSettle.USD, "usd"),
        new KeyValuePair<FuturesPerpetualSettle, string>(FuturesPerpetualSettle.USDT, "usdt"),
    };
}