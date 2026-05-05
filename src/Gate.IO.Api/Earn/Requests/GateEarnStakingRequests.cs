namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking coin query request
/// </summary>
public record GateEarnStakingCoinQueryRequest
{
    /// <summary>
    /// Currency type
    /// </summary>
    public GateEarnStakingCoinType? CoinType { get; set; }
}

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

/// <summary>
/// Staking asset query request
/// </summary>
public record GateEarnStakingAssetQueryRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Coin { get; set; }
}
