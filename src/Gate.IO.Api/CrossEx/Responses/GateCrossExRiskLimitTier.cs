namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx risk limit tier
/// </summary>
public record GateCrossExRiskLimitTier
{
    [JsonProperty("min_risk_limit_value")]
    public decimal MinimumRiskLimitValue { get; set; }

    [JsonProperty("max_risk_limit_value")]
    public decimal MaximumRiskLimitValue { get; set; }

    [JsonProperty("quick_cal_amount")]
    public decimal QuickCalculationAmount { get; set; }

    [JsonProperty("leverage_max")]
    public decimal MaximumLeverage { get; set; }

    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    [JsonProperty("tier")]
    public int Tier { get; set; }
}
