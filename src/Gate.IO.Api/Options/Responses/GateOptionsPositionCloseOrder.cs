namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsPositionCloseOrder
/// </summary>
public record GateOptionsPositionCloseOrder
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

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }
}