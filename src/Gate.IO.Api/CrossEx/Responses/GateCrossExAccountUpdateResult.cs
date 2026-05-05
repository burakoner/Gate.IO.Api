namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account update result
/// </summary>
public record GateCrossExAccountUpdateResult
{
    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    /// <summary>
    /// Gets or sets the Account Mode.
    /// </summary>
    [JsonProperty("account_mode")]
    public string AccountMode { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }
}
