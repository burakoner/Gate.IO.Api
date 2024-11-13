namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsUserTrade
/// </summary>
public class GateOptionsUserTrade
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
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order ID related
    /// </summary>
    [JsonProperty("order_id")]
    public int OrderId { get; set; }

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
    [JsonProperty("role")]
    public GateOptionsTraderRole Role { get; set; }
}