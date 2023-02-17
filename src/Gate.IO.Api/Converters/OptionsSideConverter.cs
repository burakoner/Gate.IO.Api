namespace Gate.IO.Api.Converters;

internal class OptionsSideConverter : BaseConverter<OptionsSide>
{
    public OptionsSideConverter() : this(true) { }
    public OptionsSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsSide, string>> Mapping => new()
    {
        new KeyValuePair<OptionsSide, string>(OptionsSide.Long, "long"),
        new KeyValuePair<OptionsSide, string>(OptionsSide.Short, "short"),
    };
}