namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi account assets
/// </summary>
public record GateTradFiAccountAssets
{
    /// <summary>
    /// Gets or sets the Equity.
    /// </summary>
    [JsonProperty("equity")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Gets or sets the Margin Level.
    /// </summary>
    [JsonProperty("margin_level")]
    public decimal MarginLevel { get; set; }

    /// <summary>
    /// Gets or sets the Balance.
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Gets or sets the Margin.
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Gets or sets the Margin Free.
    /// </summary>
    [JsonProperty("margin_free")]
    public decimal MarginFree { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL.
    /// </summary>
    [JsonProperty("unrealized_pnl")]
    public decimal UnrealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Mt5 UID.
    /// </summary>
    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }
}
