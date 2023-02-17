namespace Gate.IO.Api.Converters;

internal class FuturesTriggerOrderStrategyTypeConverter : BaseConverter<FuturesTriggerOrderStrategyType>
{
    public FuturesTriggerOrderStrategyTypeConverter() : this(true) { }
    public FuturesTriggerOrderStrategyTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesTriggerOrderStrategyType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesTriggerOrderStrategyType, string>(FuturesTriggerOrderStrategyType.ByPrice, "0"),
        new KeyValuePair<FuturesTriggerOrderStrategyType, string>(FuturesTriggerOrderStrategyType.ByPriceGap, "1"),
    };
}