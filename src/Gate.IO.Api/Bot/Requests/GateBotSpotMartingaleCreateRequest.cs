namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot martingale creation request
/// </summary>
public record GateBotSpotMartingaleCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotSpotMartingaleCreateParameters CreateParameters { get; set; }
}
