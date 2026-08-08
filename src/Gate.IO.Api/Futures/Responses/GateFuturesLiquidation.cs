namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesLiquidation
/// </summary>
public record GateFuturesLiquidation
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
    /// User position size
    /// </summary>
    [JsonProperty("size")]
    public decimal Size { get; set; }

    /// <summary>
    /// Number of forced liquidation orders
    /// </summary>
    [JsonProperty("order_size")]
    public decimal OrderSize { get; set; }

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
    /// Reserved field with no current business significance
    /// </summary>
    [JsonProperty("left")]
    public string Left { get; set; }
}
