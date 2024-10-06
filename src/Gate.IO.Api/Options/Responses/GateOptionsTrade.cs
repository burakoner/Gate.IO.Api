namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsTrade
/// </summary>
public class GateOptionsTrade
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Trading time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trading time, with milliseconds set to 3 decimal places.
    /// </summary>
    [JsonProperty("create_time_ms")]
    public double CreateTimeMs { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Trading size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Trading price (quote currency)
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Whether internal trade. Internal trade refers to the takeover of liquidation orders by the insurance fund and ADL users. Since it is not a normal matching on the market depth, the transaction price may deviate, and it will not be recorded in the K-line. If it is not an internal trade, this field will not be returned.
    /// </summary>
    [JsonProperty("is_internal")]
    public bool IsInternal { get; set; }
}