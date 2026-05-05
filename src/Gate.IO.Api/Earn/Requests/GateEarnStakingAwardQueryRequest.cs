namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking award query request
/// </summary>
public record GateEarnStakingAwardQueryRequest
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
    /// Page number
    /// </summary>
    public int? Page { get; set; }
}
