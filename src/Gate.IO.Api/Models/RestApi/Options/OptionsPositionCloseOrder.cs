namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsPositionCloseOrder
{
    /// <summary>
    /// Close order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Close order price （quote currency)
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Is the close order from liquidation
    /// </summary>
    [JsonProperty("is_liq")]
    public bool IsLiq { get; set; }
}