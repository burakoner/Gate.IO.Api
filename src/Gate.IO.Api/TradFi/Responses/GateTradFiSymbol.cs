namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol
/// </summary>
public record GateTradFiSymbol
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    [JsonProperty("category_id")]
    public long CategoryId { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradingStatus Status { get; set; }

    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    [JsonProperty("icon_link")]
    public string IconLink { get; set; }

    [JsonProperty("close_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CloseTime { get; set; }

    [JsonProperty("open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? OpenTime { get; set; }

    [JsonProperty("next_open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? NextOpenTime { get; set; }

    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    [JsonProperty("settlement_currency_symbol")]
    public string SettlementCurrencySymbol { get; set; }

    [JsonProperty("price_precision")]
    public int PricePrecision { get; set; }
}
