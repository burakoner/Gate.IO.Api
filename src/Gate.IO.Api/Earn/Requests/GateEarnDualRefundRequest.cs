namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment early redemption request
/// </summary>
public record GateEarnDualRefundRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Request ID returned by order-refund-preview
    /// </summary>
    public string RequestId { get; set; }
}
