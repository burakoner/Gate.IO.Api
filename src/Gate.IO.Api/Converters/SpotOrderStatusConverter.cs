namespace Gate.IO.Api.Converters;

internal class SpotOrderStatusConverter : BaseConverter<SpotOrderStatus>
{
    public SpotOrderStatusConverter() : this(true) { }
    public SpotOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderStatus, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderStatus, string>(SpotOrderStatus.Open, "open"),
        new KeyValuePair<SpotOrderStatus, string>(SpotOrderStatus.Closed, "closed"),
        new KeyValuePair<SpotOrderStatus, string>(SpotOrderStatus.Cancelled, "cancelled"),
    };
}