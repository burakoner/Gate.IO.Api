namespace Gate.IO.Api.Converters;

internal class FuturesTriggerOrderPriceTypeConverter : BaseConverter<FuturesTriggerOrderPriceType>
{
    public FuturesTriggerOrderPriceTypeConverter() : this(true) { }
    public FuturesTriggerOrderPriceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesTriggerOrderPriceType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesTriggerOrderPriceType, string>(FuturesTriggerOrderPriceType.DealPrice, "0"),
        new KeyValuePair<FuturesTriggerOrderPriceType, string>(FuturesTriggerOrderPriceType.MarkPrice, "1"),
        new KeyValuePair<FuturesTriggerOrderPriceType, string>(FuturesTriggerOrderPriceType.IndexPrice, "2"),
    };
}