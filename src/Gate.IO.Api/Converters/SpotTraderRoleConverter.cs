namespace Gate.IO.Api.Converters;

internal class SpotTraderRoleConverter : BaseConverter<SpotTraderRole>
{
    public SpotTraderRoleConverter() : this(true) { }
    public SpotTraderRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotTraderRole, string>> Mapping => new()
    {
        new KeyValuePair<SpotTraderRole, string>(SpotTraderRole.Taker, "taker"),
        new KeyValuePair<SpotTraderRole, string>(SpotTraderRole.Maker, "maker"),
    };
}