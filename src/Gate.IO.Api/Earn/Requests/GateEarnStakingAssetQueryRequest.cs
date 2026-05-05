namespace Gate.IO.Api.Earn;

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
