namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsContract
{
    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("tag")]
    public OptionsContractPeriod Period { get; set; }

    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("expiration_time")]
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// &#x60;true&#x60; means call options, while &#x60;false&#x60; is put options
    /// </summary>
    [JsonProperty("is_call")]
    public bool IsCall { get; set; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
    
    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    /// <summary>
    /// Last trading price
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Current mark price (quote currency)
    /// </summary>
    [JsonProperty("mark_price")]
    public string MarkPrice { get; set; }

    [JsonProperty("mark_price_up")]
    public string MarkPriceUp { get; set; }

    [JsonProperty("mark_price_down")]
    public string MarkPriceDown { get; set; }

    /// <summary>
    /// Minimum mark price increment
    /// </summary>
    [JsonProperty("mark_price_round")]
    public string MarkPriceRound { get; set; }

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

    [JsonProperty("position_limit")]
    public long PositionLimit { get; set; }

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
    /// Multiplier used in converting from invoicing to settlement currency
    /// </summary>
    [JsonProperty("multiplier")]
    public decimal Multiplier { get; set; }

    /// <summary>
    /// Minimum order price increment
    /// </summary>
    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    /// <summary>
    /// deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate
    /// </summary>
    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }

    /// <summary>
    /// Maker fee rate, where negative means rebate
    /// </summary>
    [JsonProperty("maker_fee_rate")]
    public string MakerFeeRate { get; set; }

    /// <summary>
    /// Taker fee rate
    /// </summary>
    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }
    
    [JsonProperty("price_limit_fee_rate")]
    public decimal PriceLimitFeeRate { get; set; }

    /// <summary>
    /// Referral fee rate discount
    /// </summary>
    [JsonProperty("ref_discount_rate")]
    public decimal RefDiscountRate { get; set; }

    /// <summary>
    /// Referrer commission rate
    /// </summary>
    [JsonProperty("ref_rebate_rate")]
    public decimal RefRebateRate { get; set; }
    
    [JsonProperty("settle_fee_rate")]
    public decimal SettleFeeRate { get; set; }
    
    [JsonProperty("settle_limit_fee_rate")]
    public decimal SettleLimitFeeRate { get; set; }

    /// <summary>
    /// Minimum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_min")]
    public long OrderSizeMin { get; set; }

    /// <summary>
    /// Maximum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_max")]
    public long OrderSizeMax { get; set; }

    /// <summary>
    /// Maximum number of open orders
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    [JsonProperty("ask1_price")]
    public decimal BestAskPrice { get; set; }

    [JsonProperty("ask1_size")]
    public long BestAskSize { get; set; }

    [JsonProperty("bid1_price")]
    public decimal BestBidPrice { get; set; }

    [JsonProperty("bid1_size")]
    public long BestBidSize { get; set; }
}
