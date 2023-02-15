namespace Gate.IO.Api.Converters;

internal class SpotMarketStatusConverter : BaseConverter<SpotMarketStatus>
{
    public SpotMarketStatusConverter() : this(true) { }
    public SpotMarketStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotMarketStatus, string>> Mapping => new()
    {
        new KeyValuePair<SpotMarketStatus, string>(SpotMarketStatus.Untradable, "untradable"),
        new KeyValuePair<SpotMarketStatus, string>(SpotMarketStatus.Buyable, "buyable"),
        new KeyValuePair<SpotMarketStatus, string>(SpotMarketStatus.Sellable, "sellable"),
        new KeyValuePair<SpotMarketStatus, string>(SpotMarketStatus.Tradable, "tradable"),
    };
}
