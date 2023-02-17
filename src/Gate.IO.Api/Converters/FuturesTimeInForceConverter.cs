namespace Gate.IO.Api.Converters;

internal class FuturesTimeInForceConverter : BaseConverter<FuturesTimeInForce>
{
    public FuturesTimeInForceConverter() : this(true) { }
    public FuturesTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<FuturesTimeInForce, string>(FuturesTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<FuturesTimeInForce, string>(FuturesTimeInForce.ImmediateOrCancel, "ioc"),
        new KeyValuePair<FuturesTimeInForce, string>(FuturesTimeInForce.FillOrKill, "fok"),
        new KeyValuePair<FuturesTimeInForce, string>(FuturesTimeInForce.PendingOrCancelled, "poc"),
    };
}