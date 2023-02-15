namespace Gate.IO.Api.Converters;

internal class MarginLoanRepayModeConverter : BaseConverter<MarginLoanRepayMode>
{
    public MarginLoanRepayModeConverter() : this(true) { }
    public MarginLoanRepayModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarginLoanRepayMode, string>> Mapping => new()
    {
        new KeyValuePair<MarginLoanRepayMode, string>(MarginLoanRepayMode.All, "all"),
        new KeyValuePair<MarginLoanRepayMode, string>(MarginLoanRepayMode.Partial, "partial"),
    };
}