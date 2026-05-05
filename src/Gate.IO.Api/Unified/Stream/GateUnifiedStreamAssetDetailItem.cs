namespace Gate.IO.Api.Unified;

/// <summary>
/// Represents a single currency detail inside a Unified asset detail stream update.
/// </summary>
public record GateUnifiedStreamAssetDetailItem
{
    /// <summary>
    /// Available amount.
    /// </summary>
    [JsonProperty("a")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount.
    /// </summary>
    [JsonProperty("f")]
    public decimal Freeze { get; set; }

    /// <summary>
    /// Equity.
    /// </summary>
    [JsonProperty("e")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Total liabilities.
    /// </summary>
    [JsonProperty("tl")]
    public decimal TotalLiabilities { get; set; }

    /// <summary>
    /// Balance.
    /// </summary>
    [JsonProperty("b")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Cross margin balance. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("cb")]
    public decimal? CrossBalance { get; set; }

    /// <summary>
    /// Cross margin collateral balance. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("mb")]
    public decimal? MarginBalance { get; set; }

    /// <summary>
    /// Total initial margin for cross margin. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("im")]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Initial margin rate for cross margin, in percent. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRate { get; set; }

    /// <summary>
    /// Total maintenance margin for cross margin. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("mm")]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Maintenance margin rate for cross margin, in percent. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRate { get; set; }

    /// <summary>
    /// Total available margin balance. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("am")]
    public decimal? AvailableMargin { get; set; }

    /// <summary>
    /// Isolated margin available for opening positions. This field is pushed for USDT in single-currency margin mode.
    /// </summary>
    [JsonProperty("iam")]
    public decimal? IsolatedAvailableMargin { get; set; }
}
