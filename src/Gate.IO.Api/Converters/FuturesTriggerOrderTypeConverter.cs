namespace Gate.IO.Api.Converters;

internal class FuturesTriggerOrderTypeConverter : BaseConverter<FuturesTriggerOrderType>
{
    public FuturesTriggerOrderTypeConverter() : this(true) { }
    public FuturesTriggerOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesTriggerOrderType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.CloseLongOrder, "close-long-order"),
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.CloseShortOrder, "close-short-order"),
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.CloseLongPosition, "close-long-position"),
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.CloseShortPosition, "close-short-position"),
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.PlanCloseLongPosition, "plan-close-long-position"),
        new KeyValuePair<FuturesTriggerOrderType, string>(FuturesTriggerOrderType.PlanCloseShortPosition, "plan-close-short-position"),
    };
}