namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy creation result
/// </summary>
public record GateBotCreateResult
{
    /// <summary>
    /// Gets or sets the Strategy ID.
    /// </summary>
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Jump URL.
    /// </summary>
    [JsonProperty("jump_url")]
    public string JumpUrl { get; set; }
}
