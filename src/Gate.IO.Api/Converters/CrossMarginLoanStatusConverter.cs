namespace Gate.IO.Api.Converters;

internal class CrossMarginLoanStatusConverter : BaseConverter<CrossMarginLoanStatus>
{
    public CrossMarginLoanStatusConverter() : this(true) { }
    public CrossMarginLoanStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CrossMarginLoanStatus, string>> Mapping => new()
    {
        new KeyValuePair<CrossMarginLoanStatus, string>(CrossMarginLoanStatus.Failed, "1"),
        new KeyValuePair<CrossMarginLoanStatus, string>(CrossMarginLoanStatus.Borrowed, "2"),
        new KeyValuePair<CrossMarginLoanStatus, string>(CrossMarginLoanStatus.Repaid, "3"),
    };
}