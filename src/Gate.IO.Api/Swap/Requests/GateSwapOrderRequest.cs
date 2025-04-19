namespace Gate.IO.Api.Swap;

/// <summary>
/// GateSwapOrderRequest
/// </summary>
public record GateSwapOrderRequest
{
    /// <summary>
    /// Preview result ID
    /// </summary>
    [JsonProperty("preview_id")]
    public long? PreviewId { get; set; }

    /// <summary>
    /// The name of the asset being sold, as obtained from the "GET /flash_swap/currency_pairs" API, which retrieves a list of supported flash swap currency pairs.
    /// </summary>
    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    /// <summary>
    /// Amount to sell (based on the preview result)
    /// </summary>
    [JsonProperty("sell_amount")]
    public decimal? SellAmount { get; set; }

    /// <summary>
    /// The name of the asset being purchased, as obtained from the "GET /flash_swap/currency_pairs" API, which provides a list of supported flash swap currency pairs.
    /// </summary>
    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    /// <summary>
    /// Amount to buy (based on the preview result)
    /// </summary>
    [JsonProperty("buy_amount")]
    public decimal? BuyAmount { get; set; }
}