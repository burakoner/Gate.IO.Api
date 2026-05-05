namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn early redemption request
/// </summary>
public record GateEarnFixedTermPreRedeemRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }
}
