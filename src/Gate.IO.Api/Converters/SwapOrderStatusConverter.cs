namespace Gate.IO.Api.Converters;

internal class SwapOrderStatusConverter : BaseConverter<SwapOrderStatus>
{
    public SwapOrderStatusConverter() : this(true) { }
    public SwapOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SwapOrderStatus, string>> Mapping => new()
    {
        new KeyValuePair<SwapOrderStatus, string>(SwapOrderStatus.Success, "1"),
        new KeyValuePair<SwapOrderStatus, string>(SwapOrderStatus.Failure, "2"),
    };
}