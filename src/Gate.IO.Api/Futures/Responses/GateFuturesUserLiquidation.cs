namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesUserLiquidation
/// </summary>
public record GateFuturesUserLiquidation
{
    /// <summary>
    /// Liquidation time
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }
    
    /// <summary>
    /// Position leverage. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
    
    /// <summary>
    /// Position size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }
    
    /// <summary>
    /// Position margin. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }
    
    /// <summary>
    /// Average entry price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }
    
    /// <summary>
    /// Liquidation price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("liq_price")]
    public decimal LiquidationPrice { get; set; }
    
    /// <summary>
    /// Mark price. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }
    
    /// <summary>
    /// Liquidation order ID. Not returned in public endpoints.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
    
    /// <summary>
    /// Liquidation order price
    /// </summary>
    [JsonProperty("order_price")]
    public decimal OrderPrice { get; set; }
    
    /// <summary>
    /// Liquidation order average taker price
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal FillPrice { get; set; }

    /// <summary>
    /// Liquidation order maker size
    /// </summary>
    [JsonProperty("left")]
    public long Left { get; set; }
}