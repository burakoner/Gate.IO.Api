namespace Gate.IO.Api.Converters;

internal class SpotOrderQueryStatusConverter : BaseConverter<SpotOrderQueryStatus>
{
    public SpotOrderQueryStatusConverter() : this(true) { }
    public SpotOrderQueryStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderQueryStatus, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderQueryStatus, string>(SpotOrderQueryStatus.Open, "open"),
        new KeyValuePair<SpotOrderQueryStatus, string>(SpotOrderQueryStatus.Finished, "finished"),
    };
}