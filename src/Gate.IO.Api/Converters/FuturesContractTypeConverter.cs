namespace Gate.IO.Api.Converters;

internal class FuturesContractTypeConverter : BaseConverter<FuturesContractType>
{
    public FuturesContractTypeConverter() : this(true) { }
    public FuturesContractTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesContractType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesContractType, string>(FuturesContractType.Inverse, "inverse"),
        new KeyValuePair<FuturesContractType, string>(FuturesContractType.Direct, "direct"),
    };
}