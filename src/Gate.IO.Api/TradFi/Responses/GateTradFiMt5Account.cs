namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi MT5 account information
/// </summary>
public record GateTradFiMt5Account
{
    /// <summary>
    /// Gets or sets the Mt5 UID.
    /// </summary>
    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Stop Out Level.
    /// </summary>
    [JsonProperty("stop_out_level")]
    public string StopOutLevel { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public GateTradFiAccountStatus Status { get; set; }
}
