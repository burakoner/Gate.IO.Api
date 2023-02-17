namespace Gate.IO.Api.Converters;

internal class DeliveryCycleConverter : BaseConverter<DeliveryCycle>
{
    public DeliveryCycleConverter() : this(true) { }
    public DeliveryCycleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<DeliveryCycle, string>> Mapping => new()
    {
        new KeyValuePair<DeliveryCycle, string>(DeliveryCycle.Weekly, "WEEKLY"),
        new KeyValuePair<DeliveryCycle, string>(DeliveryCycle.BiWeekly, "BI-WEEKLY"),
        new KeyValuePair<DeliveryCycle, string>(DeliveryCycle.Quarterly, "QUARTERLY"),
        new KeyValuePair<DeliveryCycle, string>(DeliveryCycle.BiQuarterly, "BI-QUARTERLY"),
    };
}