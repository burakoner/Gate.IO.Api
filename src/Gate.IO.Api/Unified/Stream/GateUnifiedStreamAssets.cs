namespace Gate.IO.Api.Unified;

/// <summary>
/// Represents a Unified asset overview stream update.
/// </summary>
public record GateUnifiedStreamAssets
{
    /// <summary>
    /// Gate user ID.
    /// </summary>
    [JsonProperty("u")]
    public long UserId { get; set; }

    /// <summary>
    /// Data refresh time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RefreshTime { get; set; }

    /// <summary>
    /// Total initial margin rate, in percent.
    /// </summary>
    [JsonProperty("r")]
    public decimal TotalInitialMarginRate { get; set; }

    /// <summary>
    /// Total maintenance margin rate, in percent.
    /// </summary>
    [JsonProperty("R")]
    public decimal TotalMaintenanceMarginRate { get; set; }

    /// <summary>
    /// Total margin balance.
    /// </summary>
    [JsonProperty("b")]
    public decimal TotalMarginBalance { get; set; }

    /// <summary>
    /// Portfolio margin total equity.
    /// </summary>
    [JsonProperty("e")]
    public decimal UnifiedMarginTotalEquity { get; set; }

    /// <summary>
    /// Portfolio margin total liabilities.
    /// </summary>
    [JsonProperty("l")]
    public decimal UnifiedMarginTotalLiabilities { get; set; }

    /// <summary>
    /// Portfolio margin total.
    /// </summary>
    [JsonProperty("T")]
    public decimal UnifiedMarginTotal { get; set; }

    /// <summary>
    /// Total available margin.
    /// </summary>
    [JsonProperty("a")]
    public decimal TotalAvailableMargin { get; set; }
}
