namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Market
/// </summary>
public  class GateMarginMarket
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
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Minimum amount of base currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("min_base_amount")]
    public decimal? MinimumBaseAmount { get; set; }

    /// <summary>
    /// Minimum amount of quote currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("min_quote_amount")]
    public decimal? MinimumQuoteAmount { get; set; }

    /// <summary>
    /// Maximum amount of quote currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("max_quote_amount")]
    public decimal? MaximumQuoteAmount { get; set; }

    /// <summary>
    /// Trading Status
    /// </summary>
    [JsonProperty("status")]
    public GateMarginMarketStatus Status { get; set; }
}
