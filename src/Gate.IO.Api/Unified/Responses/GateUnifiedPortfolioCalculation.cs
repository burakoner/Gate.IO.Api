namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Portfolio Calculator Response
/// </summary>
public record GateUnifiedPortfolioCalculation
{
    /// <summary>
    /// Total maintenance margin, including only portfolio margin calculation results for positions in risk units, excluding borrowing margin. If borrowing exists, conventional borrowing margin requirements will still apply
    /// </summary>
    [JsonProperty("maintain_margin_total")]
    public decimal TotalMaintenanceMargin { get; set; }

    /// <summary>
    /// Total initial margin, calculated as the maximum of the following three combinations: position, position + positive delta orders, position + negative delta orders
    /// </summary>
    [JsonProperty("initial_margin_total")]
    public decimal TotalInitialMargin { get; set; }

    /// <summary>
    /// Calculation time
    /// </summary>
    [JsonProperty("calculate_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CalculationTime { get; set; }

    /// <summary>
    /// Risk unit
    /// </summary>
    [JsonProperty("risk_unit")]
    public List<GateUnifiedPortfolioCalculationRistUnit> RistUnits { get; set; } = [];
}

/// <summary>
/// Gate Unified Portfolio Calculator Response -> Risk Unit
/// </summary>
public record GateUnifiedPortfolioCalculationRistUnit
{
    /// <summary>
    /// Risk unit name
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Spot hedge usage
    /// </summary>
    [JsonProperty("spot_in_use")]
    public string SpotInUse { get; set; }

    /// <summary>
    /// Margin result
    /// </summary>
    [JsonProperty("margin_result")]
    public List<GateUnifiedPortfolioCalculationRistUnitMarginResult> MarginResults { get; set; } = [];

    /// <summary>
    /// Maintenance margin
    /// </summary>
    [JsonProperty("maintain_margin")]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Initial margin
    /// </summary>
    [JsonProperty("initial_margin")]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Total Delta of risk unit
    /// </summary>
    [JsonProperty("delta")]
    public decimal? Delta { get; set; }

    /// <summary>
    /// Total Gamma of risk unit
    /// </summary>
    [JsonProperty("gamma")]
    public decimal? Gamma { get; set; }

    /// <summary>
    /// Total Theta of risk unit
    /// </summary>
    [JsonProperty("theta")]
    public decimal? Theta { get; set; }

    /// <summary>
    /// Total Vega of risk unit
    /// </summary>
    [JsonProperty("vega")]
    public decimal? Vega { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Response -> Risk Unit -> Margin Result
/// </summary>
public record GateUnifiedPortfolioCalculationRistUnitMarginResult
{
    /// <summary>
    /// Position combination type
    /// original_position - Original position
    /// long_delta_original_position - Positive delta + Original position
    /// short_delta_original_position - Negative delta + Original position
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Results of 33 stress scenarios for MR1
    /// </summary>
    [JsonProperty("profit_loss_ranges")]
    public List<GateUnifiedPortfolioCalculationRistUnitMarginResultProfitLossRange> ProfitLossRanges { get; set; } = [];

    /// <summary>
    /// Results of 33 stress scenarios for MR1
    /// </summary>
    [JsonProperty("max_loss")]
    public GateUnifiedPortfolioCalculationRistUnitMarginResultProfitLossRange MaximumLoss { get; set; }

    /// <summary>
    /// Stress testing
    /// </summary>
    [JsonProperty("mr1")]
    public decimal? MR1 { get; set; }

    /// <summary>
    /// Basis spread risk
    /// </summary>
    [JsonProperty("mr2")]
    public decimal? MR2 { get; set; }

    /// <summary>
    /// Volatility spread risk
    /// </summary>
    [JsonProperty("mr3")]
    public decimal? MR3 { get; set; }

    /// <summary>
    /// Option short risk
    /// </summary>
    [JsonProperty("mr4")]
    public decimal? MR4 { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Response -> Risk Unit -> Margin Result -> Profit Loss Range
/// </summary>
public record GateUnifiedPortfolioCalculationRistUnitMarginResultProfitLossRange
{
    /// <summary>
    /// Percentage change in price
    /// </summary>
    [JsonProperty("price_percentage")]
    public decimal PricePercentage { get; set; }

    /// <summary>
    /// Percentage change in implied volatility
    /// </summary>
    [JsonProperty("implied_volatility_percentage")]
    public decimal ImpliedVolatilityPercentage { get; set; }

    /// <summary>
    /// PnL
    /// </summary>
    [JsonProperty("profit_loss")]
    public decimal ProfitLoss { get; set; }
}