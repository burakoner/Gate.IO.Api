namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot currency pair
/// </summary>
public class SpotMarket
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
    /// Quote currency
    /// </summary>
    [JsonProperty("quote")]
    public string Quote { get; set; }

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
    /// Amount scale
    /// </summary>
    [JsonProperty("amount_precision")]
    public int AmountPrecision { get; set; }

    /// <summary>
    /// Price scale
    /// </summary>
    [JsonProperty("precision")]
    public int Precision { get; set; }

    /// <summary>
    /// Trading Status
    /// </summary>
    [JsonProperty("trade_status")]
    public GateSpotMarketStatus Status { get; set; }

    /// <summary>
    /// Sell start unix timestamp in seconds
    /// </summary>
    [JsonProperty("sell_start")]
    public DateTime SellStart { get; set; }

    /// <summary>
    /// Buy start unix timestamp in seconds
    /// </summary>
    [JsonProperty("buy_start")]
    public DateTime BuyStart { get; set; }
}
