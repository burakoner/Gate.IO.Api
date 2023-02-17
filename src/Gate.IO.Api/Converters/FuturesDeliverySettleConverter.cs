namespace Gate.IO.Api.Converters;

internal class FuturesDeliverySettleConverter : BaseConverter<FuturesDeliverySettle>
{
    public FuturesDeliverySettleConverter() : this(true) { }
    public FuturesDeliverySettleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesDeliverySettle, string>> Mapping => new()
    {
        new KeyValuePair<FuturesDeliverySettle, string>(FuturesDeliverySettle.BTC, "btc"),
        new KeyValuePair<FuturesDeliverySettle, string>(FuturesDeliverySettle.USDT, "usdt"),
    };
}