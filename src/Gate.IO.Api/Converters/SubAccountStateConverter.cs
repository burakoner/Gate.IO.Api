namespace Gate.IO.Api.Converters;

internal class SubAccountStateConverter : BaseConverter<SubAccountState>
{
    public SubAccountStateConverter() : this(true) { }
    public SubAccountStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SubAccountState, string>> Mapping => new()
    {
        new KeyValuePair<SubAccountState, string>(SubAccountState.Normal, "normal"),
        new KeyValuePair<SubAccountState, string>(SubAccountState.Locked, "locked"),
    };
}