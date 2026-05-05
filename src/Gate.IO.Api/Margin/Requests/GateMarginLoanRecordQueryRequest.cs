namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin loan record query request
/// </summary>
public record GateMarginLoanRecordQueryRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Borrow or repay
    /// </summary>
    public GateMarginUniOrderType? Type { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}
