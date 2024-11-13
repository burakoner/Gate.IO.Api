namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesUserTrade
/// </summary>
public class GateFuturesUserTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

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
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Trading size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

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
    public long CloseSize { get; set; }

    /// <summary>
    /// Trading price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Trade role. Available values are taker and maker
    /// </summary>
    [JsonProperty("role")]
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
