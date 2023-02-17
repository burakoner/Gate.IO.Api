namespace Gate.IO.Api.Converters;

internal class OptionsTraderRoleConverter : BaseConverter<OptionsTraderRole>
{
    public OptionsTraderRoleConverter() : this(true) { }
    public OptionsTraderRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsTraderRole, string>> Mapping => new()
    {
        new KeyValuePair<OptionsTraderRole, string>(OptionsTraderRole.Taker, "taker"),
        new KeyValuePair<OptionsTraderRole, string>(OptionsTraderRole.Maker, "maker"),
    };
}