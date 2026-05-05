namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi ticker
/// </summary>
public record GateTradFiTicker
{
    [JsonProperty("highest_price")]
    public decimal HighestPrice { get; set; }

    [JsonProperty("lowest_price")]
    public decimal LowestPrice { get; set; }

    [JsonProperty("price_change")]
    public decimal PriceChange { get; set; }

    [JsonProperty("price_change_amount")]
    public decimal PriceChangeAmount { get; set; }

    [JsonProperty("today_open_price")]
    public decimal TodayOpenPrice { get; set; }

    [JsonProperty("last_today_close_price")]
    public decimal LastTodayClosePrice { get; set; }

    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    [JsonProperty("bid_price")]
    public decimal BidPrice { get; set; }

    [JsonProperty("ask_price")]
    public decimal AskPrice { get; set; }

    [JsonProperty("favorite")]
    public bool IsFavorite { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradingStatus Status { get; set; }

    [JsonProperty("close_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CloseTime { get; set; }

    [JsonProperty("open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? OpenTime { get; set; }

    [JsonProperty("next_open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? NextOpenTime { get; set; }

    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    [JsonProperty("category_name")]
    public string CategoryName { get; set; }
}
