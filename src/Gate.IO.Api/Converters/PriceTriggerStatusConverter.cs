namespace Gate.IO.Api.Converters;

internal class PriceTriggerStatusConverter : BaseConverter<PriceTriggerStatus>
{
    public PriceTriggerStatusConverter() : this(true) { }
    public PriceTriggerStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<PriceTriggerStatus, string>> Mapping => new()
    {
        new KeyValuePair<PriceTriggerStatus, string>(PriceTriggerStatus.Open, "open"),
        new KeyValuePair<PriceTriggerStatus, string>(PriceTriggerStatus.Cancelled, "cancelled"),
        new KeyValuePair<PriceTriggerStatus, string>(PriceTriggerStatus.Finished, "finish"),
        new KeyValuePair<PriceTriggerStatus, string>(PriceTriggerStatus.Failed, "failed"),
        new KeyValuePair<PriceTriggerStatus, string>(PriceTriggerStatus.Expired, "expired"),
    };
}