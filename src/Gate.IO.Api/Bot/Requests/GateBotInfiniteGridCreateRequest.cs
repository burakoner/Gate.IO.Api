namespace Gate.IO.Api.Bot;

/// <summary>
/// Infinite grid creation request
/// </summary>
public record GateBotInfiniteGridCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotInfiniteGridCreateParameters CreateParameters { get; set; }
}
