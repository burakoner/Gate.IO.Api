namespace Gate.IO.Api.Spot;

/// <summary>
/// Open orders
/// </summary>
public class GateSpotOpenOrders
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Total number of orders
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    [JsonProperty("orders")]
    public List<GateSpotOrder> Orders { get; set; } = [];
}