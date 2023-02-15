namespace Gate.IO.Api.Converters;

internal class SpotOrderTimeInForceConverter : BaseConverter<SpotOrderTimeInForce>
{
    public SpotOrderTimeInForceConverter() : this(true) { }
    public SpotOrderTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderTimeInForce, string>(SpotOrderTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<SpotOrderTimeInForce, string>(SpotOrderTimeInForce.ImmediateOrCancel, "ioc"),
        new KeyValuePair<SpotOrderTimeInForce, string>(SpotOrderTimeInForce.FillOrKill, "fok"),
        new KeyValuePair<SpotOrderTimeInForce, string>(SpotOrderTimeInForce.PendingOrCancelled, "poc"),
    };
}

internal class SpotPriceOrderTimeInForceConverter : BaseConverter<SpotPriceOrderTimeInForce>
{
    public SpotPriceOrderTimeInForceConverter() : this(true) { }
    public SpotPriceOrderTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotPriceOrderTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<SpotPriceOrderTimeInForce, string>(SpotPriceOrderTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<SpotPriceOrderTimeInForce, string>(SpotPriceOrderTimeInForce.ImmediateOrCancel, "ioc"),
    };
}