namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol
/// </summary>
public record GateTradFiSymbol
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the Category ID.
    /// </summary>
    [JsonProperty("category_id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradingStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the Trade Mode.
    /// </summary>
    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    /// <summary>
    /// Gets or sets the Icon Link.
    /// </summary>
    [JsonProperty("icon_link")]
    public string IconLink { get; set; }

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
    /// Gets or sets the Settlement Currency.
    /// </summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Gets or sets the Settlement Currency Symbol.
    /// </summary>
    [JsonProperty("settlement_currency_symbol")]
    public string SettlementCurrencySymbol { get; set; }

    /// <summary>
    /// Gets or sets the Price Precision.
    /// </summary>
    [JsonProperty("price_precision")]
    public int PricePrecision { get; set; }
}
