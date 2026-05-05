namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi user
/// </summary>
public record GateTradFiUser
{
    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public GateTradFiAccountStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Mt5 UID.
    /// </summary>
    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }
}
