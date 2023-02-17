namespace Gate.IO.Api.Converters;

internal class SpotTriggerOrderTimeInForceConverter : BaseConverter<SpotTriggerOrderTimeInForce>
{
    public SpotTriggerOrderTimeInForceConverter() : this(true) { }
    public SpotTriggerOrderTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotTriggerOrderTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<SpotTriggerOrderTimeInForce, string>(SpotTriggerOrderTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<SpotTriggerOrderTimeInForce, string>(SpotTriggerOrderTimeInForce.ImmediateOrCancel, "ioc"),
    };
}