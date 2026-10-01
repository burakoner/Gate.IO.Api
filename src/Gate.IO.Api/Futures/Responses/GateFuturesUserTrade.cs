namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesUserTrade
/// </summary>
public record GateFuturesUserTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id"), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long Id { get; set; }

    /// <summary>Fill ID returned by my_trades_timerange (distinct JSON field from my_trades.id).</summary>
    [JsonProperty("trade_id"), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? TradeId { get; set; }

    /// <summary>Trade value reported by my_trades. Not guaranteed on the time-range endpoint.</summary>
    [JsonProperty("trade_value")]
    public decimal? TradeValue { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order ID related
    /// </summary>
    [JsonProperty("order_id"), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long OrderId { get; set; }

    /// <summary>
    /// Trading size
    /// </summary>
    [JsonProperty("size")]
    public decimal Size { get; set; }

    /// <summary>
    /// Number of closed positions:
    /// close_size=0 &amp;&amp; size&gt;0 Open long position
    /// close_size=0 &amp;&amp; size＜0 Open short position
    /// close_size&gt;0 &amp;&amp; size&gt;0 &amp;&amp; size &lt;= close_size Close short postion
    /// close_size&gt;0 &amp;&amp; size&gt;0 &amp;&amp; size &gt; close_size Close short position and open long position
    /// close_size&lt;0 &amp;&amp; size&lt;0 &amp;&amp; size &gt;= close_size Close long postion
    /// close_size&lt;0 &amp;&amp; size&lt;0 &amp;&amp; size &lt; close_size Close long position and open short position
    /// </summary>
    [JsonProperty("close_size")]
    public decimal CloseSize { get; set; }

    /// <summary>
    /// Trading price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Trade role. Available values are taker and maker
    /// </summary>
    [JsonProperty("role"), JsonConverter(typeof(MapConverter))]
    public GateFuturesTradeRole Role { get; set; }

    /// <summary>
    /// User defined information
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Fee deducted
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Points used to deduct fee
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal PointFee { get; set; }
}
