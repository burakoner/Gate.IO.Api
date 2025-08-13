namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures Risk Limit Tier
/// </summary>
public record GateFuturesRiskLimitTier : GateFuturesRiskLimitTable
{
    /// <summary>
    /// Markets, visible only during market pagination requests
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }
}
