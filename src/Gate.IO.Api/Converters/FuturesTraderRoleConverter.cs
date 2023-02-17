namespace Gate.IO.Api.Converters;

internal class FuturesTraderRoleConverter : BaseConverter<FuturesTraderRole>
{
    public FuturesTraderRoleConverter() : this(true) { }
    public FuturesTraderRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesTraderRole, string>> Mapping => new()
    {
        new KeyValuePair< FuturesTraderRole, string>(FuturesTraderRole.Taker, "taker"),
        new KeyValuePair< FuturesTraderRole, string>(FuturesTraderRole.Maker, "maker"),
    };
}