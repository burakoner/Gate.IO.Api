namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin transferable amount query request
/// </summary>
public record GateMarginTransferableAmountRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }
}
