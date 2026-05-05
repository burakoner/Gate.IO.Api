namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account asset
/// </summary>
public record GateCrossExAccountAsset
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    [JsonProperty("upnl")]
    public decimal? UnrealizedPnl { get; set; }

    [JsonProperty("equity")]
    public decimal? Equity { get; set; }

    [JsonProperty("futures_initial_margin")]
    public decimal? FuturesInitialMargin { get; set; }

    [JsonProperty("futures_maintenance_margin")]
    public decimal? FuturesMaintenanceMargin { get; set; }

    [JsonProperty("borrowing_initial_margin")]
    public decimal? BorrowingInitialMargin { get; set; }

    [JsonProperty("borrowing_maintenance_margin")]
    public decimal? BorrowingMaintenanceMargin { get; set; }

    [JsonProperty("available_balance")]
    public decimal? AvailableBalance { get; set; }

    [JsonProperty("liability")]
    public decimal? Liability { get; set; }
}
