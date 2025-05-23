namespace Gate.IO.Api.Unified;

/// <summary>
/// Leverage config
/// </summary>
public record GateUnifiedLeverageConfig
{
    /// <summary>
    /// Current leverage
    /// </summary>
    [JsonProperty("current_leverage")]
    public decimal CurrentLeverage { get; set; }

    /// <summary>
    /// Min leverage
    /// </summary>
    [JsonProperty("min_leverage")]
    public decimal MinLeverage { get; set; }

    /// <summary>
    /// Max leverage
    /// </summary>
    [JsonProperty("max_leverage")]
    public decimal MaxLeverage { get; set; }

    /// <summary>
    /// Debit
    /// </summary>
    [JsonProperty("debit")]
    public decimal Debit { get; set; }

    /// <summary>
    /// Available margin
    /// </summary>
    [JsonProperty("available_margin")]
    public decimal AvailableMargin { get; set; }

    /// <summary>
    /// Borrowable
    /// </summary>
    [JsonProperty("borrowable")]
    public decimal Borrowable { get; set; }

    /// <summary>
    /// Except leverage borrowable
    /// </summary>
    [JsonProperty("except_leverage_borrowable")]
    public decimal ExceptLeverageBorrowable { get; set; }
}
