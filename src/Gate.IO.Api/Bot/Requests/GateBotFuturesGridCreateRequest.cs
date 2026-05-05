namespace Gate.IO.Api.Bot;

/// <summary>
/// Futures grid creation request
/// </summary>
public record GateBotFuturesGridCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotFuturesGridCreateParameters CreateParameters { get; set; }
}
