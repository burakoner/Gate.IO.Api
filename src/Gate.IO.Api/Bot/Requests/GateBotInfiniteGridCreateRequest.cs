namespace Gate.IO.Api.Bot;

/// <summary>
/// Infinite grid creation request
/// </summary>
public record GateBotInfiniteGridCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotInfiniteGridCreateParameters CreateParameters { get; set; }
}
