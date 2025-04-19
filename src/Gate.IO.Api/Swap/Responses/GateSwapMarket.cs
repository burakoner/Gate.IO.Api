namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate Swap Market
/// </summary>
public record GateSwapMarket
{
    /// <summary>
    /// The currency pair, BTC_USDT represents selling Bitcoin (BTC) and buying Tether (USDT).
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// The currency to be sold
    /// </summary>
    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    /// <summary>
    /// The currency to be bought
    /// </summary>
    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    /// <summary>
    /// The minimum quantity required for selling
    /// </summary>
    [JsonProperty("sell_min_amount")]
    public decimal SellMinimumAmount { get; set; }

    /// <summary>
    /// The maximum quantity allowed for selling
    /// </summary>
    [JsonProperty("sell_max_amount")]
    public decimal SellMaximumAmount { get; set; }

    /// <summary>
    /// The minimum quantity required for buying
    /// </summary>
    [JsonProperty("buy_min_amount")]
    public decimal BuyMinimumAmount { get; set; }

    /// <summary>
    /// The maximum quantity allowed for buying
    /// </summary>
    [JsonProperty("buy_max_amount")]
    public decimal BuyMaximumAmount { get; set; }
}
