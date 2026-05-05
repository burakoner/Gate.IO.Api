namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account asset
/// </summary>
public record GateCrossExAccountAsset
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Balance.
    /// </summary>
    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL.
    /// </summary>
    [JsonProperty("upnl")]
    public decimal? UnrealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Equity.
    /// </summary>
    [JsonProperty("equity")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// Gets or sets the Futures Initial Margin.
    /// </summary>
    [JsonProperty("futures_initial_margin")]
    public decimal? FuturesInitialMargin { get; set; }

    /// <summary>
    /// Gets or sets the Futures Maintenance Margin.
    /// </summary>
    [JsonProperty("futures_maintenance_margin")]
    public decimal? FuturesMaintenanceMargin { get; set; }

    /// <summary>
    /// Gets or sets the Borrowing Initial Margin.
    /// </summary>
    [JsonProperty("borrowing_initial_margin")]
    public decimal? BorrowingInitialMargin { get; set; }

    /// <summary>
    /// Gets or sets the Borrowing Maintenance Margin.
    /// </summary>
    [JsonProperty("borrowing_maintenance_margin")]
    public decimal? BorrowingMaintenanceMargin { get; set; }

    /// <summary>
    /// Gets or sets the Available Balance.
    /// </summary>
    [JsonProperty("available_balance")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Gets or sets the Liability.
    /// </summary>
    [JsonProperty("liability")]
    public decimal? Liability { get; set; }
}
