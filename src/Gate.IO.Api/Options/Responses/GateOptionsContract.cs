namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsContract
/// </summary>
public class GateOptionsContract
{
    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// tag
    /// </summary>
    [JsonProperty("tag")]
    public GateOptionsContractPeriod Period { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonProperty("expiration_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ExpirationTime { get; set; }

    /// <summary>
    /// &#x60;true&#x60; means call options, while &#x60;false&#x60; is put options
    /// </summary>
    [JsonProperty("is_call")]
    public bool IsCall { get; set; }

    /// <summary>
    /// Multiplier used in converting from invoicing to settlement currency
    /// </summary>
    [JsonProperty("multiplier")]
    public decimal Multiplier { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Underlying price (quote currency)
    /// </summary>
    [JsonProperty("underlying_price")]
    public decimal UnderlyingPrice { get; set; }

    /// <summary>
    /// Last trading price
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Current mark price (quote currency)
    /// </summary>
    [JsonProperty("mark_price")]
    public DeserializeError MarkPrice { get; set; }

    /// <summary>
    /// Current index price (quote currency)
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Maker fee rate, where negative means rebate
    /// </summary>
    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    /// <summary>
    /// Taker fee rate
    /// </summary>
    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    /// <summary>
    /// Minimum order price increment
    /// </summary>
    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    /// <summary>
    /// Minimum mark price increment
    /// </summary>
    [JsonProperty("mark_price_round")]
    public decimal MarkPriceRound { get; set; }

    /// <summary>
    /// Minimum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_min")]
    public long OrderSizeMinimum { get; set; }

    /// <summary>
    /// Maximum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_max")]
    public long OrderSizeMaximum { get; set; }

    /// <summary>
    /// deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate
    /// </summary>
    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }
    
    /// <summary>
    /// Referral fee rate discount
    /// </summary>
    [JsonProperty("ref_discount_rate")]
    public decimal ReferralDiscountRate { get; set; }
    
    /// <summary>
    /// Referrer commission rate
    /// </summary>
    [JsonProperty("ref_rebate_rate")]
    public decimal ReferrerRebateRate { get; set; }
    
    /// <summary>
    /// Current orderbook ID
    /// </summary>
    [JsonProperty("orderbook_id")]
    public long OrderbookId { get; set; }
    
    /// <summary>
    /// Current trade ID
    /// </summary>
    [JsonProperty("trade_id")]
    public long TradeId { get; set; }
    
    /// <summary>
    /// Historical accumulated trade size
    /// </summary>
    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }
    
    /// <summary>
    /// Current total long position size
    /// </summary>
    [JsonProperty("position_size")]
    public long PositionSize { get; set; }
    
    /// <summary>
    /// Maximum number of open orders
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }
}
