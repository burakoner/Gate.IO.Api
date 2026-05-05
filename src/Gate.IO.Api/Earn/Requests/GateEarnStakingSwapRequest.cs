namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking swap request
/// </summary>
public record GateEarnStakingSwapRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    public GateEarnStakingOperationType Side { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// DeFi-type mining protocol identifier
    /// </summary>
    public long? ProductId { get; set; }
}
