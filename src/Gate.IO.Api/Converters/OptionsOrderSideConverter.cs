namespace Gate.IO.Api.Converters;

internal class OptionsOrderSideConverter : BaseConverter<OptionsOrderSide>
{
    public OptionsOrderSideConverter() : this(true) { }
    public OptionsOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsOrderSide, string>> Mapping => new()
    {
        new KeyValuePair<OptionsOrderSide, string>(OptionsOrderSide.Ask, "ask"),
        new KeyValuePair<OptionsOrderSide, string>(OptionsOrderSide.Bid, "bid"),
    };
}