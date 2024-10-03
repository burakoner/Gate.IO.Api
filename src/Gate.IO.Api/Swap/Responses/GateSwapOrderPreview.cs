namespace Gate.IO.Api.Swap;

/// <summary>
/// GateSwapOrderPreview
/// </summary>
public class GateSwapOrderPreview
{
    /// <summary>
    /// Preview result ID
    /// </summary>
    [JsonProperty("preview_id")]
    public long PreviewId { get; set; }

    /// <summary>
    /// Currency to sell which can be retrieved from supported currency list API GET /flash_swap/currencies
    /// </summary>
    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    /// <summary>
    /// Amount to sell
    /// </summary>
    [JsonProperty("sell_amount")]
    public decimal? SellAmount { get; set; }

    /// <summary>
    /// Currency to buy which can be retrieved from supported currency list API GET /flash_swap/currencies
    /// </summary>
    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    /// <summary>
    /// Amount to buy
    /// </summary>
    [JsonProperty("buy_amount")]
    public decimal? BuyAmount { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
}