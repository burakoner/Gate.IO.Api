namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin borrowable amount query request
/// </summary>
public record GateMarginBorrowableRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }
}
