namespace Gate.IO.Api.Converters;

internal class PriceTriggerFilterConverter : BaseConverter<GateSpotTriggerFilter>
{
    public PriceTriggerFilterConverter() : this(true) { }
    public PriceTriggerFilterConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<GateSpotTriggerFilter, string>> Mapping => new()
    {
        new KeyValuePair<GateSpotTriggerFilter, string>(GateSpotTriggerFilter.Open, "open"),
        new KeyValuePair<GateSpotTriggerFilter, string>(GateSpotTriggerFilter.Finished, "finished"),
    };
}