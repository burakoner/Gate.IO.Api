namespace Gate.IO.Api.Converters;

internal class FuturesOrderAutoSizeConverter : BaseConverter<FuturesOrderAutoSize>
{
    public FuturesOrderAutoSizeConverter() : this(true) { }
    public FuturesOrderAutoSizeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesOrderAutoSize, string>> Mapping => new()
    {
        new KeyValuePair<FuturesOrderAutoSize, string>(FuturesOrderAutoSize.CloseLong, "close_long"),
        new KeyValuePair<FuturesOrderAutoSize, string>(FuturesOrderAutoSize.CloseShort, "close_short"),
    };
}