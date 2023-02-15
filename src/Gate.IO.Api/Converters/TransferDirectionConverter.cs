namespace Gate.IO.Api.Converters;

internal class TransferDirectionConverter : BaseConverter<TransferDirection>
{
    public TransferDirectionConverter() : this(true) { }
    public TransferDirectionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<TransferDirection, string>> Mapping => new()
    {
        new KeyValuePair<TransferDirection, string>(TransferDirection.From, "from"),
        new KeyValuePair<TransferDirection, string>(TransferDirection.To, "to"),
    };
}