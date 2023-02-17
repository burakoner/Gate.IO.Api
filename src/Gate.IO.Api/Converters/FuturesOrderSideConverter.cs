namespace Gate.IO.Api.Converters;

internal class FuturesOrderSideConverter : BaseConverter<FuturesOrderSide>
{
    public FuturesOrderSideConverter() : this(true) { }
    public FuturesOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesOrderSide, string>> Mapping => new()
    {
        new KeyValuePair<FuturesOrderSide, string>(FuturesOrderSide.Ask, "ask"),
        new KeyValuePair<FuturesOrderSide, string>(FuturesOrderSide.Bid, "bid"),
    };
}