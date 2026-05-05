namespace Gate.IO.Api.Bot;

/// <summary>
/// Margin grid creation request
/// </summary>
public record GateBotMarginGridCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotMarginGridCreateParameters CreateParameters { get; set; }
}
