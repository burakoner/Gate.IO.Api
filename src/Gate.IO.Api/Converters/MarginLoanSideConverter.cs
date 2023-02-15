namespace Gate.IO.Api.Converters;

internal class MarginLoanSideConverter : BaseConverter<MarginLoanSide>
{
    public MarginLoanSideConverter() : this(true) { }
    public MarginLoanSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarginLoanSide, string>> Mapping => new()
    {
        new KeyValuePair<MarginLoanSide, string>(MarginLoanSide.Lend, "lend"),
        new KeyValuePair<MarginLoanSide, string>(MarginLoanSide.Borrow, "borrow"),
    };
}