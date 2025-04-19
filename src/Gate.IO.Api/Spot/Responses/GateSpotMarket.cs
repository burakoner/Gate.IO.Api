namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot currency pair
/// </summary>
public record GateSpotMarket
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("id")]
    public string Symbol { get; set; }

    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("base")]
    public string Base { get; set; }

    /// <summary>
    /// Transaction currency name
    /// </summary>
    [JsonProperty("base_name")]
    public string BaseName { get; set; }

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quote")]
    public string Quote { get; set; }

    /// <summary>
    /// Name of the denominated currency
    /// </summary>
    [JsonProperty("quote_name")]
    public string QuoteName { get; set; }

    /// <summary>
    /// Trading fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Minimum amount of base currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("min_base_amount")]
    public decimal? MinBaseAmount { get; set; }

    /// <summary>
    /// Minimum amount of quote currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("min_quote_amount")]
    public decimal? MinQuoteAmount { get; set; }
    
    /// <summary>
    /// Maximum amount of base currency to trade, null means no limit
    /// </summary>
    [JsonProperty("max_base_amount")]
    public decimal? MaxBaseAmount { get; set; }

    /// <summary>
    /// Maximum amount of quote currency to trade, null means no limit
    /// </summary>
    [JsonProperty("max_quote_amount")]
    public decimal? MaxQuoteAmount { get; set; }

    /// <summary>
    /// Amount scale
    /// </summary>
    [JsonProperty("amount_precision")]
    public int AmountPrecision { get; set; }

    /// <summary>
    /// Price scale
    /// </summary>
    [JsonProperty("precision")]
    public int PricePrecision { get; set; }

    /// <summary>
    /// Trading Status
    /// </summary>
    [JsonProperty("trade_status")]
    public GateSpotMarketStatus Status { get; set; }

    /// <summary>
    /// Sell start unix timestamp in seconds
    /// </summary>
    [JsonProperty("sell_start")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime SellStart { get; set; }

    /// <summary>
    /// Buy start unix timestamp in seconds
    /// </summary>
    [JsonProperty("buy_start")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime BuyStart { get; set; }

    /// <summary>
    /// Trading pair type, normal: normal, premarket: pre-market
    /// </summary>
    [JsonProperty("type")]
    public GateSpotMarketType Type { get; set; }
}
