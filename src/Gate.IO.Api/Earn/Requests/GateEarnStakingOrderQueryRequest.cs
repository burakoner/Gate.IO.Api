namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking order query request
/// </summary>
public record GateEarnStakingOrderQueryRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long? ProductId { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    public GateEarnStakingOperationType? Type { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }
}
