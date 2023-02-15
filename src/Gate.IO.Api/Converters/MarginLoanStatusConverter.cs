namespace Gate.IO.Api.Converters;

internal class MarginLoanStatusConverter : BaseConverter<MarginLoanStatus>
{
    public MarginLoanStatusConverter() : this(true) { }
    public MarginLoanStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarginLoanStatus, string>> Mapping => new()
    {
        new KeyValuePair<MarginLoanStatus, string>(MarginLoanStatus.Open, "open"),
        new KeyValuePair<MarginLoanStatus, string>(MarginLoanStatus.Loaned, "loaned"),
        new KeyValuePair<MarginLoanStatus, string>(MarginLoanStatus.Finished, "finished"),
        new KeyValuePair<MarginLoanStatus, string>(MarginLoanStatus.AutoRepaid, "auto_repaid"),
    };
}