namespace Gate.IO.Api.Converters;

internal class FuturesMarkTypeConverter : BaseConverter<FuturesMarkType>
{
    public FuturesMarkTypeConverter() : this(true) { }
    public FuturesMarkTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesMarkType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesMarkType, string>(FuturesMarkType.Internal, "internal"),
        new KeyValuePair<FuturesMarkType, string>(FuturesMarkType.Index, "index"),
    };
}