namespace Gate.IO.Api.Stock;

/// <summary>
/// Historical stock order
/// </summary>
public record GateStockOrderHistory
{
    /// <summary>Gets or sets the order identifier.</summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }
    /// <summary>Gets or sets the symbol.</summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    /// <summary>Gets or sets the exchange.</summary>
    [JsonProperty("exchange"), JsonConverter(typeof(MapConverter))]
    public GateStockExchange Exchange { get; set; }
    /// <summary>Gets or sets the quote currency.</summary>
    [JsonProperty("quote_currency")]
    public string QuoteCurrency { get; set; }
    /// <summary>Gets or sets the foreign-exchange rate.</summary>
    [JsonProperty("fx_rate")]
    public decimal FxRate { get; set; }
    /// <summary>Gets or sets the symbol description.</summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }
    /// <summary>Gets or sets the price type.</summary>
    [JsonProperty("price_type"), JsonConverter(typeof(MapConverter))]
    public GateStockOrderPriceType PriceType { get; set; }
    /// <summary>Gets or sets the undocumented numeric order status.</summary>
    [JsonProperty("status")]
    public int Status { get; set; }
    /// <summary>Gets or sets the status description.</summary>
    [JsonProperty("status_desc")]
    public string StatusDescription { get; set; }
    /// <summary>Gets or sets detailed status information.</summary>
    [JsonProperty("status_detail")]
    public GateStockOrderStatusDetail StatusDetail { get; set; }
    /// <summary>Gets or sets the undocumented numeric finish reason.</summary>
    [JsonProperty("finish_as")]
    public int FinishAs { get; set; }
    /// <summary>Gets or sets the side.</summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateStockOrderSide Side { get; set; }
    /// <summary>Gets or sets the time in force.</summary>
    [JsonProperty("time_in_force"), JsonConverter(typeof(MapConverter))]
    public GateStockTimeInForce TimeInForce { get; set; }
    /// <summary>Gets or sets the requested volume.</summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }
    /// <summary>Gets or sets the filled volume.</summary>
    [JsonProperty("fill_volume")]
    public decimal FilledVolume { get; set; }
    /// <summary>Gets or sets the order price.</summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
    /// <summary>Gets or sets the average fill price.</summary>
    [JsonProperty("avg_fill_price")]
    public decimal? AverageFillPrice { get; set; }
    /// <summary>Gets or sets the charged commission.</summary>
    [JsonProperty("commission")]
    public decimal Commission { get; set; }
    /// <summary>Gets or sets the order creation time.</summary>
    [JsonProperty("time_setup"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
    /// <summary>Gets or sets the completion time.</summary>
    [JsonProperty("time_done"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CompletionTime { get; set; }
}

/// <summary>
/// Stock order status detail
/// </summary>
public record GateStockOrderStatusDetail
{
    /// <summary>Gets or sets the title.</summary>
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>Gets or sets the message.</summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}
