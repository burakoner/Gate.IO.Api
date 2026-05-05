namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy stop result
/// </summary>
public record GateBotStopResult
{
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("result_message")]
    public string ResultMessage { get; set; }
}
