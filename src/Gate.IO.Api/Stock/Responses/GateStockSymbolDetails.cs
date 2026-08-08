namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading symbol details
/// </summary>
public record GateStockSymbolDetails
{
    /// <summary>Gets or sets the symbol.</summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    /// <summary>Gets or sets the exchange.</summary>
    [JsonProperty("exchange"), JsonConverter(typeof(MapConverter))]
    public GateStockExchange Exchange { get; set; }
    /// <summary>Gets or sets the exchange description.</summary>
    [JsonProperty("exchange_desc")]
    public string ExchangeDescription { get; set; }
    /// <summary>Gets or sets the quote currency.</summary>
    [JsonProperty("quote_currency")]
    public string QuoteCurrency { get; set; }
    /// <summary>Gets or sets quote currency precision.</summary>
    [JsonProperty("quote_currency_precision")]
    public int QuoteCurrencyPrecision { get; set; }
    /// <summary>Gets or sets the foreign-exchange rate.</summary>
    [JsonProperty("fx_rate")]
    public decimal FxRate { get; set; }
    /// <summary>Gets or sets the symbol description.</summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }
    /// <summary>Gets or sets the symbol category.</summary>
    [JsonProperty("category")]
    public string Category { get; set; }
    /// <summary>Gets or sets the settlement currency.</summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }
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
    /// <summary>Gets or sets volume precision.</summary>
    [JsonProperty("volume_precision")]
    public int VolumePrecision { get; set; }
    /// <summary>Gets or sets whether this is an IPO symbol.</summary>
    [JsonProperty("is_ipo")]
    public bool IsIpo { get; set; }
    /// <summary>Gets or sets the IPO price.</summary>
    [JsonProperty("ipo_price")]
    public decimal? IpoPrice { get; set; }
    /// <summary>Gets or sets general price protection.</summary>
    [JsonProperty("price_protection")]
    public decimal PriceProtection { get; set; }
    /// <summary>Gets or sets sell-side price protection.</summary>
    [JsonProperty("sell_price_protection")]
    public decimal SellPriceProtection { get; set; }
    /// <summary>Gets or sets buy-side price protection.</summary>
    [JsonProperty("buy_price_protection")]
    public decimal BuyPriceProtection { get; set; }
    /// <summary>Gets or sets the slippage rate.</summary>
    [JsonProperty("slippage_rate")]
    public decimal SlippageRate { get; set; }
    /// <summary>Gets or sets the commission rate.</summary>
    [JsonProperty("commission_rate")]
    public decimal CommissionRate { get; set; }
    /// <summary>Gets or sets the trading status.</summary>
    [JsonProperty("trade_status"), JsonConverter(typeof(MapConverter))]
    public GateStockTradingStatus TradingStatus { get; set; }
    /// <summary>Gets or sets the example-only status value when Gate returns it.</summary>
    /// <remarks>This field appears in the official response example but is absent from the published schema and current production payloads.</remarks>
    [JsonProperty("status")]
    public string Status { get; set; }
    /// <summary>Gets or sets the trading permission mode.</summary>
    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateStockTradeMode TradeMode { get; set; }
    /// <summary>Gets or sets the order fill timing.</summary>
    [JsonProperty("order_fill_timing"), JsonConverter(typeof(MapConverter))]
    public GateStockOrderFillTiming OrderFillTiming { get; set; }
    /// <summary>Gets or sets localized descriptions.</summary>
    [JsonProperty("symbol_descs")]
    public List<GateStockSymbolDescription> Descriptions { get; set; } = [];
    /// <summary>Gets or sets the icon link.</summary>
    [JsonProperty("icon_link")]
    public string IconLink { get; set; }
}
