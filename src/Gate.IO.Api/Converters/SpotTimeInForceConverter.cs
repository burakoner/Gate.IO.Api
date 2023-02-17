namespace Gate.IO.Api.Converters;

internal class SpotTimeInForceConverter : BaseConverter<SpotTimeInForce>
{
    public SpotTimeInForceConverter() : this(true) { }
    public SpotTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.ImmediateOrCancel, "ioc"),
        new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.FillOrKill, "fok"),
        new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.PendingOrCancelled, "poc"),
    };
}