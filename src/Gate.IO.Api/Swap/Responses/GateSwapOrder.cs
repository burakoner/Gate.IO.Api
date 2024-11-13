namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate Swap Order
/// </summary>
public class GateSwapOrder
{
    /// <summary>
    /// Flash swap order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Creation time of order (in milliseconds)
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Currency to sell
    /// </summary>
    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    /// <summary>
    /// Amount to sell
    /// </summary>
    [JsonProperty("sell_amount")]
    public decimal SellAmount { get; set; }

    /// <summary>
    /// Currency to buy
    /// </summary>
    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    /// <summary>
    /// Amount to buy
    /// </summary>
    [JsonProperty("buy_amount")]
    public decimal BuyAmount { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Flash swap order status
    /// </summary>
    [JsonProperty("status")]
    public GateSwapOrderStatus Status { get; set; }
}