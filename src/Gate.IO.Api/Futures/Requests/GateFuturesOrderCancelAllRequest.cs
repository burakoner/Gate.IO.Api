namespace Gate.IO.Api.Futures;

/// <summary>
/// Filters used when cancelling all matching open Futures orders.
/// </summary>
public record GateFuturesOrderCancelAllRequest
{
    /// <summary>
    /// Futures contract. All contracts are included when omitted.
    /// </summary>
    public string Contract { get; set; }

    /// <summary>
    /// Controls how much order data is returned.
    /// </summary>
    public GateFuturesActionMode? ActionMode { get; set; }

    /// <summary>
    /// Limits cancellation to bids or asks.
    /// </summary>
    public GateFuturesOrderSide? Side { get; set; }

    /// <summary>
    /// Excludes reduce-only orders when enabled.
    /// </summary>
    public bool? ExcludeReduceOnly { get; set; }

    /// <summary>
    /// Remark attached to the cancellation request.
    /// </summary>
    public string Text { get; set; }
}
