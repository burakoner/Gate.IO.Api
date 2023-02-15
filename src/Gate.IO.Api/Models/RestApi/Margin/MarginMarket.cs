namespace Gate.IO.Api.Models.RestApi.Margin;

public  class MarginMarket
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
    public int Leverage { get; set; }

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
    /// Maximum amount of quote currency to trade, &#x60;null&#x60; means no limit
    /// </summary>
    [JsonProperty("max_quote_amount")]
    public decimal? MaxQuoteAmount { get; set; }

    /// <summary>
    /// Trading Status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(MarginMarketStatusConverter))]
    public MarginMarketStatus Status { get; set; }
}
