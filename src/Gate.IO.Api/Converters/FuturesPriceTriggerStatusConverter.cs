namespace Gate.IO.Api.Converters;

internal class FuturesPriceTriggerStatusConverter : BaseConverter<FuturesPriceTriggerStatus>
{
    public FuturesPriceTriggerStatusConverter() : this(true) { }
    public FuturesPriceTriggerStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesPriceTriggerStatus, string>> Mapping => new()
    {
        new KeyValuePair<FuturesPriceTriggerStatus, string>(FuturesPriceTriggerStatus.Open, "open"),
        new KeyValuePair<FuturesPriceTriggerStatus, string>(FuturesPriceTriggerStatus.Finished, "finished"),
        new KeyValuePair<FuturesPriceTriggerStatus, string>(FuturesPriceTriggerStatus.Inactive, "inactive"),
        new KeyValuePair<FuturesPriceTriggerStatus, string>(FuturesPriceTriggerStatus.Invalid, "invalid"),
    };
}