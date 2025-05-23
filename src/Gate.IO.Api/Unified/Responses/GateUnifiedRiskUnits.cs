namespace Gate.IO.Api.Unified;

/// <summary>
/// Risk units
/// </summary>
public record GateUnifiedRiskUnits
{
    /// <summary>
    /// User id
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Spot hedge status
    /// </summary>
    [JsonProperty("spot_hedge")]
    public bool SpotHedge { get; set; }

    /// <summary>
    /// Risk units
    /// </summary>
    [JsonProperty("risk_units")]
    public List<GateIoRiskUnitsDetails> RiskUnits { get; set; } = [];
}

/// <summary>
/// Risk unit details
/// </summary>
public record GateIoRiskUnitsDetails
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Spot hedging usage
    /// </summary>
    [JsonProperty("spot_in_use")]
    public decimal SpotInUse { get; set; }

    /// <summary>
    /// Maintenance margin
    /// </summary>
    [JsonProperty("maintain_margin")]
    public decimal MaintenanceMargin { get; set; }

    /// <summary>
    /// Initial margin
    /// </summary>
    [JsonProperty("initial_margin")]
    public decimal InitialMargin { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }
}
