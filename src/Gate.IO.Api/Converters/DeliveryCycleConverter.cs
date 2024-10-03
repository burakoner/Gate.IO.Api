namespace Gate.IO.Api.Converters;

internal class DeliveryCycleConverter : BaseConverter<FuturesDeliveryCycle>
{
    public DeliveryCycleConverter() : this(true) { }
    public DeliveryCycleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesDeliveryCycle, string>> Mapping => new()
    {
        new KeyValuePair<FuturesDeliveryCycle, string>(FuturesDeliveryCycle.Weekly, "WEEKLY"),
        new KeyValuePair<FuturesDeliveryCycle, string>(FuturesDeliveryCycle.BiWeekly, "BI-WEEKLY"),
        new KeyValuePair<FuturesDeliveryCycle, string>(FuturesDeliveryCycle.Quarterly, "QUARTERLY"),
        new KeyValuePair<FuturesDeliveryCycle, string>(FuturesDeliveryCycle.BiQuarterly, "BI-QUARTERLY"),
    };
}