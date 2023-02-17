namespace Gate.IO.Api.Converters;

internal class SpotPriceTriggerStatusConverter : BaseConverter<SpotPriceTriggerStatus>
{
    public SpotPriceTriggerStatusConverter() : this(true) { }
    public SpotPriceTriggerStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotPriceTriggerStatus, string>> Mapping => new()
    {
        new KeyValuePair<SpotPriceTriggerStatus, string>(SpotPriceTriggerStatus.Open, "open"),
        new KeyValuePair<SpotPriceTriggerStatus, string>(SpotPriceTriggerStatus.Cancelled, "cancelled"),
        new KeyValuePair<SpotPriceTriggerStatus, string>(SpotPriceTriggerStatus.Finished, "finish"),
        new KeyValuePair<SpotPriceTriggerStatus, string>(SpotPriceTriggerStatus.Failed, "failed"),
        new KeyValuePair<SpotPriceTriggerStatus, string>(SpotPriceTriggerStatus.Expired, "expired"),
    };
}