namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesLiquidate
{
    /// <summary>
    /// Liquidation time
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Position size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Liquidation order price
    /// </summary>
    [JsonProperty("order_price")]
    public decimal OrderPrice { get; set; }

    /// <summary>
    /// Liquidation order maker size
    /// </summary>
    [JsonProperty("left")]
    public long Left { get; set; }

    /// <summary>
    /// Liquidation order average taker price
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal FillPrice { get; set; }
}