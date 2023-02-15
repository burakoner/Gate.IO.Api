namespace Gate.IO.Api.Converters;

internal class MarginMarketStatusConverter : BaseConverter<MarginMarketStatus>
{
    public MarginMarketStatusConverter() : this(true) { }
    public MarginMarketStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarginMarketStatus, string>> Mapping => new()
    {
        new KeyValuePair<MarginMarketStatus, string>(MarginMarketStatus.Disabled, "0"),
        new KeyValuePair<MarginMarketStatus, string>(MarginMarketStatus.Enabled, "1"),
    };
}