namespace Gate.IO.Api.Spot;

/// <summary>
/// Filters for DELETE /spot/orders. Omitted filters broaden the cancellation scope.
/// </summary>
public record GateSpotCancelOrdersRequest
{
    /// <summary>
    /// Currency pair. Omit to cancel matching open orders across all pairs.
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Buy or sell side. Omit to include both sides.
    /// </summary>
    public GateSpotOrderSide? Side { get; set; }

    /// <summary>
    /// Account type. Omit to include all eligible accounts; specify Unified for a unified account.
    /// </summary>
    public GateSpotAccountType? Account { get; set; }

    /// <summary>
    /// Actual quote currency to cancel in a unified market. Omit to include all quotes matching the other filters.
    /// </summary>
    public string TradeQuote { get; set; }

    /// <summary>
    /// ACK, RESULT or FULL processing mode. Omit for the server default (FULL).
    /// </summary>
    public GateSpotActionMode? ActionMode { get; set; }
}
