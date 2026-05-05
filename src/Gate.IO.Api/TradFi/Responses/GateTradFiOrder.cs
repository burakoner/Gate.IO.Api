namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi active order
/// </summary>
public record GateTradFiOrder
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
    /// Gets or sets the Description.
    /// </summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    [JsonProperty("price_type"), JsonConverter(typeof(MapConverter))]
    public GateTradFiOrderPriceType PriceType { get; set; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    [JsonProperty("state")]
    public int State { get; set; }

    /// <summary>
    /// Gets or sets the State Description.
    /// </summary>
    [JsonProperty("state_desc")]
    public string StateDescription { get; set; }

    /// <summary>
    /// Gets or sets the Finished.
    /// </summary>
    [JsonProperty("finished")]
    public int Finished { get; set; }

    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public GateTradFiOrderSide Side { get; set; }

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

    /// <summary>
    /// Gets or sets the Setup Time.
    /// </summary>
    [JsonProperty("time_setup")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime SetupTime { get; set; }
}
