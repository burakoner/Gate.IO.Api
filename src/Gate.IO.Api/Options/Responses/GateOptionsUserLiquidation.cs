namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsUserLiquidation
/// </summary>
public record GateOptionsUserLiquidation
{
    /// <summary>
    /// Position close time
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
    
    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }
    
    /// <summary>
    /// Position side, long or short
    /// </summary>
    [JsonProperty("side")]
    public GateOptionsSide Side { get; set; }

    /// <summary>
    /// PNL
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }
    
    /// <summary>
    /// Text of close order
    /// </summary>
    [JsonProperty("text")]
    public string Comment { get; set; }
    /// <summary>
    /// settlement size
    /// </summary>
    [JsonProperty("settle_size")]
    public decimal SettleSize { get; set; }
}