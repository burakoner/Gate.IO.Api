namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order update result
/// </summary>
public record GateTradFiOrderUpdateResult
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    [JsonProperty("state")]
    public int State { get; set; }

    /// <summary>
    /// Gets or sets the Volume.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss Price.
    /// </summary>
    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }
}
