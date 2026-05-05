namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy creation result
/// </summary>
public record GateBotCreateResult
{
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("jump_url")]
    public string JumpUrl { get; set; }
}
