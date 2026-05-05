namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx risk limit tier
/// </summary>
public record GateCrossExRiskLimitTier
{
    /// <summary>
    /// Gets or sets the Minimum Risk Limit Value.
    /// </summary>
    [JsonProperty("min_risk_limit_value")]
    public decimal MinimumRiskLimitValue { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Risk Limit Value.
    /// </summary>
    [JsonProperty("max_risk_limit_value")]
    public decimal MaximumRiskLimitValue { get; set; }

    /// <summary>
    /// Gets or sets the Quick Calculation Amount.
    /// </summary>
    [JsonProperty("quick_cal_amount")]
    public decimal QuickCalculationAmount { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Leverage.
    /// </summary>
    [JsonProperty("leverage_max")]
    public decimal MaximumLeverage { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Rate.
    /// </summary>
    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    /// <summary>
    /// Gets or sets the Tier.
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }
}
