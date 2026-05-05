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
