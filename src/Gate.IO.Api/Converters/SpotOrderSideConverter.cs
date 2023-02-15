namespace Gate.IO.Api.Converters;

internal class SpotOrderSideConverter : BaseConverter<SpotOrderSide>
{
    public SpotOrderSideConverter() : this(true) { }
    public SpotOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderSide, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell"),
    };
}