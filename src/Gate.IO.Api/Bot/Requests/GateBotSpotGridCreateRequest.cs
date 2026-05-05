namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot grid creation request
/// </summary>
public record GateBotSpotGridCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotSpotGridCreateParameters CreateParameters { get; set; }
}
