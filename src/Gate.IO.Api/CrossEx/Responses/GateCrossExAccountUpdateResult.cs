namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account update result
/// </summary>
public record GateCrossExAccountUpdateResult
{
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    [JsonProperty("account_mode")]
    public string AccountMode { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }
}
