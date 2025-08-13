namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures Risk Limit Table
/// </summary>
public record GateFuturesRiskLimitTable
{
    /// <summary>
    /// Tier
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Position risk limit
    /// </summary>
    [JsonProperty("risk_limit")]
    public decimal RiskLimit { get; set; }

    /// <summary>
    /// Initial margin rate
    /// </summary>
    [JsonProperty("initial_rate")]
    public decimal InitialRate { get; set; }

    /// <summary>
    /// Maintenance margin rate
    /// </summary>
    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    /// <summary>
    /// Maximum leverage
    /// </summary>
    [JsonProperty("leverage_max")]
    public decimal LeverageMax { get; set; }

    /// <summary>
    /// Maintenance margin quick calculation deduction amount
    /// </summary>
    [JsonProperty("deduction")]
    public decimal Deduction { get; set; }
}
