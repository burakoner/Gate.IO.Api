namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock position
/// </summary>
public record GateStockPosition
{
    /// <summary>Gets or sets the symbol.</summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    /// <summary>Gets or sets the exchange.</summary>
    [JsonProperty("exchange"), JsonConverter(typeof(MapConverter))]
    public GateStockExchange Exchange { get; set; }
    /// <summary>Gets or sets the quote currency.</summary>
    [JsonProperty("quote_currency")]
    public string QuoteCurrency { get; set; }
    /// <summary>Gets or sets quote currency precision.</summary>
    [JsonProperty("quote_currency_precision")]
    public int QuoteCurrencyPrecision { get; set; }
    /// <summary>Gets or sets the foreign-exchange rate.</summary>
    [JsonProperty("fx_rate")]
    public decimal FxRate { get; set; }
    /// <summary>Gets or sets the trading status.</summary>
    [JsonProperty("trade_status"), JsonConverter(typeof(MapConverter))]
    public GateStockTradingStatus TradingStatus { get; set; }
    /// <summary>Gets or sets the symbol description.</summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }
    /// <summary>Gets or sets position profit and loss.</summary>
    [JsonProperty("position_pnl")]
    public decimal PositionPnl { get; set; }
    /// <summary>Gets or sets today's profit and loss.</summary>
    [JsonProperty("today_pnl")]
    public decimal TodayPnl { get; set; }
    /// <summary>Gets or sets the profit and loss rate.</summary>
    [JsonProperty("pnl_rate")]
    public decimal PnlRate { get; set; }
    /// <summary>Gets or sets today's sell amount.</summary>
    [JsonProperty("today_sell_amount")]
    public decimal TodaySellAmount { get; set; }
    /// <summary>Gets or sets today's buy amount.</summary>
    [JsonProperty("today_buy_amount")]
    public decimal TodayBuyAmount { get; set; }
    /// <summary>Gets or sets today's sell volume.</summary>
    [JsonProperty("today_sell_volume")]
    public decimal TodaySellVolume { get; set; }
    /// <summary>Gets or sets today's buy volume.</summary>
    [JsonProperty("today_buy_volume")]
    public decimal TodayBuyVolume { get; set; }
    /// <summary>Gets or sets yesterday's volume.</summary>
    [JsonProperty("yesterday_volume")]
    public decimal YesterdayVolume { get; set; }
    /// <summary>Gets or sets the position volume.</summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }
    /// <summary>Gets or sets the available volume.</summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }
    /// <summary>Gets or sets the pending transfer-out quantity.</summary>
    [JsonProperty("transfer_out_pending_qty")]
    public decimal TransferOutPendingQuantity { get; set; }
    /// <summary>Gets or sets the average cost price.</summary>
    [JsonProperty("avg_cost_price")]
    public decimal AverageCostPrice { get; set; }
    /// <summary>Gets or sets the diluted cost price.</summary>
    [JsonProperty("diluted_cost_price")]
    public decimal DilutedCostPrice { get; set; }
    /// <summary>Gets or sets the last regular price.</summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }
    /// <summary>Gets or sets the latest extended-hours price.</summary>
    [JsonProperty("extended_last_price")]
    public decimal? ExtendedLastPrice { get; set; }
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
