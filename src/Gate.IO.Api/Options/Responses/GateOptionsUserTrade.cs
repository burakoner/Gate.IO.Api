namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsUserTrade
/// </summary>
public record GateOptionsUserTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long TradeId { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trading time in milliseconds
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long? CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Underlying name
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Order ID related
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    [JsonProperty("order")]
    internal long StreamOrderId
    {
        get => OrderId;
        set => OrderId = value;
    }

    /// <summary>
    /// Trading size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Trading price (quote currency)
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Underlying price (quote currency)
    /// </summary>
    [JsonProperty("underlying_price")]
    public decimal UnderlyingPrice { get; set; }

    /// <summary>
    /// Trade role. Available values are &#x60;taker&#x60; and &#x60;maker&#x60;
    /// </summary>
    [JsonProperty("role"), JsonConverter(typeof(MapConverter))]
    public GateOptionsTraderRole Role { get; set; }

    /// <summary>
    /// Fee deducted
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// User defined information
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }
}
