namespace Gate.IO.Api.Converters;

internal class FuturesOrderStatusConverter : BaseConverter<FuturesOrderStatus>
{
    public FuturesOrderStatusConverter() : this(true) { }
    public FuturesOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesOrderStatus, string>> Mapping => new()
    {
        new KeyValuePair<FuturesOrderStatus, string>(FuturesOrderStatus.Open, "open"),
        new KeyValuePair<FuturesOrderStatus, string>(FuturesOrderStatus.Finished, "finished"),
    };
}