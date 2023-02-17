namespace Gate.IO.Api.Converters;

internal class PriceTriggerFilterConverter : BaseConverter<PriceTriggerFilter>
{
    public PriceTriggerFilterConverter() : this(true) { }
    public PriceTriggerFilterConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<PriceTriggerFilter, string>> Mapping => new()
    {
        new KeyValuePair<PriceTriggerFilter, string>(PriceTriggerFilter.Open, "open"),
        new KeyValuePair<PriceTriggerFilter, string>(PriceTriggerFilter.Finished, "finished"),
    };
}