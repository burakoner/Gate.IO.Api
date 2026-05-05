namespace Gate.IO.Api.Swap;

/// <summary>
/// Flash swap order preview request
/// </summary>
public record GateSwapPreviewRequest
{
    /// <summary>
    /// The name of the asset being sold
    /// </summary>
    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    /// <summary>
    /// Amount to sell
    /// </summary>
    [JsonProperty("sell_amount")]
    public decimal? SellAmount { get; set; }

    /// <summary>
    /// The name of the asset being purchased
    /// </summary>
    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    /// <summary>
    /// Amount to buy
    /// </summary>
    [JsonProperty("buy_amount")]
    public decimal? BuyAmount { get; set; }
}
