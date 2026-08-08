namespace Gate.IO.Api.Stock;

/// <summary>
/// Active stock order
/// </summary>
public record GateStockOrder
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
    /// <summary>Gets or sets the trading status.</summary>
    [JsonProperty("trade_status"), JsonConverter(typeof(MapConverter))]
    public GateStockTradingStatus TradingStatus { get; set; }
    /// <summary>Gets or sets the trading permission mode.</summary>
    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateStockTradeMode TradeMode { get; set; }
    /// <summary>Gets or sets the price type.</summary>
    [JsonProperty("price_type"), JsonConverter(typeof(MapConverter))]
    public GateStockOrderPriceType PriceType { get; set; }
    /// <summary>Gets or sets the side.</summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateStockOrderSide Side { get; set; }
    /// <summary>Gets or sets the undocumented numeric order status.</summary>
    [JsonProperty("status")]
    public int Status { get; set; }
    /// <summary>Gets or sets the requested volume.</summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }
    /// <summary>Gets or sets the filled volume.</summary>
    [JsonProperty("fill_volume")]
    public decimal FilledVolume { get; set; }
    /// <summary>Gets or sets the order price.</summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
    /// <summary>Gets or sets the order creation time.</summary>
    [JsonProperty("time_setup"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
    /// <summary>Gets or sets the order update time.</summary>
    [JsonProperty("time_update"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }
    /// <summary>Gets or sets the maximum order volume.</summary>
    [JsonProperty("max_order_volume")]
    public decimal MaximumOrderVolume { get; set; }
    /// <summary>Gets or sets the order volume step.</summary>
    [JsonProperty("step_order_volume")]
    public decimal OrderVolumeStep { get; set; }
    /// <summary>Gets or sets the minimum order volume.</summary>
    [JsonProperty("min_order_volume")]
    public decimal MinimumOrderVolume { get; set; }
    /// <summary>Gets or sets price precision.</summary>
    [JsonProperty("price_precision")]
    public int PricePrecision { get; set; }
    /// <summary>Gets or sets general price protection.</summary>
    [JsonProperty("price_protection")]
    public decimal PriceProtection { get; set; }
    /// <summary>Gets or sets sell-side price protection.</summary>
    [JsonProperty("sell_price_protection")]
    public decimal SellPriceProtection { get; set; }
    /// <summary>Gets or sets buy-side price protection.</summary>
    [JsonProperty("buy_price_protection")]
    public decimal BuyPriceProtection { get; set; }
    /// <summary>Gets or sets the commission rate.</summary>
    [JsonProperty("commission_rate")]
    public decimal CommissionRate { get; set; }
    /// <summary>Gets or sets the slippage rate.</summary>
    [JsonProperty("slippage_rate")]
    public decimal SlippageRate { get; set; }
}
