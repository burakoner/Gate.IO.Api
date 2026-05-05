namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy stop result
/// </summary>
public record GateBotStopResult
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
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Result Message.
    /// </summary>
    [JsonProperty("result_message")]
    public string ResultMessage { get; set; }
}
