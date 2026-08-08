namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock account assets
/// </summary>
public record GateStockAssets
{
    /// <summary>Gets or sets total equity.</summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }
    /// <summary>Gets or sets the account balance.</summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }
    /// <summary>Gets or sets the available balance.</summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }
    /// <summary>Gets or sets the position market value.</summary>
    [JsonProperty("position_market_value")]
    public decimal PositionMarketValue { get; set; }
    /// <summary>Gets or sets position profit and loss.</summary>
    [JsonProperty("position_pnl")]
    public decimal PositionPnl { get; set; }
    /// <summary>Gets or sets today's profit and loss.</summary>
    [JsonProperty("today_pnl")]
    public decimal TodayPnl { get; set; }
    /// <summary>Gets or sets whether the stock user exists.</summary>
    [JsonProperty("user_exists")]
    public bool UserExists { get; set; }
}
