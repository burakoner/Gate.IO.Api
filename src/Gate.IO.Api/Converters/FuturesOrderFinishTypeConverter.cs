namespace Gate.IO.Api.Converters;

internal class FuturesOrderFinishTypeConverter : BaseConverter<FuturesOrderFinishType>
{
    public FuturesOrderFinishTypeConverter() : this(true) { }
    public FuturesOrderFinishTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesOrderFinishType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.Filled, "filled"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.Cancelled, "cancelled"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.Liquidated, "liquidated"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.IOC, "ioc"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.AutoDeleveraged, "auto_deleveraged"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.ReduceOnly, "reduce_only"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.PositionClosed, "position_closed"),
        new KeyValuePair<FuturesOrderFinishType, string>(FuturesOrderFinishType.ReduceOut, "reduce_out"),
    };
}