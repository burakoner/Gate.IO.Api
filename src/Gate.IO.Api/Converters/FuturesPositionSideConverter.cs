namespace Gate.IO.Api.Converters;

internal class FuturesPositionSideConverter : BaseConverter<FuturesPositionSide>
{
    public FuturesPositionSideConverter() : this(true) { }
    public FuturesPositionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesPositionSide, string>> Mapping => new()
    {
        new KeyValuePair<FuturesPositionSide, string>(FuturesPositionSide.Long, "long"),
        new KeyValuePair<FuturesPositionSide, string>(FuturesPositionSide.Short, "short"),
    };
}