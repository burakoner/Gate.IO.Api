namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending or redemption request
/// </summary>
public record GateEarnUniLendRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Amount to deposit into lending pool or redeem
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    public GateEarnUniLendOperationType Type { get; set; }

    /// <summary>
    /// Minimum interest rate. Required for lending operations.
    /// </summary>
    public decimal? MinimumRate { get; set; }
}
