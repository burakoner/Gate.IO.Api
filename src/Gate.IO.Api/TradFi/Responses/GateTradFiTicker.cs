namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi ticker
/// </summary>
public record GateTradFiTicker
{
    /// <summary>
    /// Gets or sets the Highest Price.
    /// </summary>
    [JsonProperty("highest_price")]
    public decimal HighestPrice { get; set; }

    /// <summary>
    /// Gets or sets the Lowest Price.
    /// </summary>
    [JsonProperty("lowest_price")]
    public decimal LowestPrice { get; set; }

    /// <summary>
    /// Gets or sets the Price Change.
    /// </summary>
    [JsonProperty("price_change")]
    public decimal PriceChange { get; set; }

    /// <summary>
    /// Gets or sets the Price Change Amount.
    /// </summary>
    [JsonProperty("price_change_amount")]
    public decimal PriceChangeAmount { get; set; }

    /// <summary>
    /// Gets or sets the Today Open Price.
    /// </summary>
    [JsonProperty("today_open_price")]
    public decimal TodayOpenPrice { get; set; }

    /// <summary>
    /// Gets or sets the Last Today Close Price.
    /// </summary>
    [JsonProperty("last_today_close_price")]
    public decimal LastTodayClosePrice { get; set; }

    /// <summary>
    /// Gets or sets the Last Price.
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Gets or sets the Bid Price.
    /// </summary>
    [JsonProperty("bid_price")]
    public decimal BidPrice { get; set; }

    /// <summary>
    /// Gets or sets the Ask Price.
    /// </summary>
    [JsonProperty("ask_price")]
    public decimal AskPrice { get; set; }

    /// <summary>
    /// Gets or sets the Is Favorite.
    /// </summary>
    [JsonProperty("favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradingStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the Close Time.
    /// </summary>
    [JsonProperty("close_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CloseTime { get; set; }

    /// <summary>
    /// Gets or sets the Open Time.
    /// </summary>
    [JsonProperty("open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? OpenTime { get; set; }

    /// <summary>
    /// Gets or sets the Next Open Time.
    /// </summary>
    [JsonProperty("next_open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? NextOpenTime { get; set; }

    /// <summary>
    /// Gets or sets the Trade Mode.
    /// </summary>
    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    /// <summary>
    /// Gets or sets the Category Name.
    /// </summary>
    [JsonProperty("category_name")]
    public string CategoryName { get; set; }
}
